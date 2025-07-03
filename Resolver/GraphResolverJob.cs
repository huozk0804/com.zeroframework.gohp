//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace ZeroFramework.Goap
{
    [BurstCompile]
    public struct NodeData
    {
        public int Index;

        // Cost of when using this node as a parent
        public float P;

        // Cost when performing this node
        public float G;

        // Heuristic
        public float H;
        public int ParentIndex;
        public float3 Position;

        public float F => G + H;
    }

    [BurstCompile]
    public struct RunData
    {
        public NativeArray<int> StartIndex;

        public float3 AgentPosition;

        // Index = NodeIndex
        public NativeArray<bool> IsEnabled;

        public NativeArray<bool> IsExecutable;

        // Index = ConditionIndex
        public NativeArray<bool> ConditionsMet;
        public NativeArray<float3> Positions;
        public NativeArray<float> Costs;
        public float DistanceMultiplier;
    }

    [BurstCompile]
    public struct NodeSorter : IComparer<NodeData>
    {
        public int Compare(NodeData x, NodeData y)
        {
            return x.F.CompareTo(y.F);
        }
    }

    [BurstCompile]
    public struct GraphResolverJob : IJob
    {
        // Graph specific
        // Dictionary<ActionIndex, ConditionIndex[]>
        [ReadOnly]
        public NativeParallelMultiHashMap<int, int> NodeConditions;

        // Dictionary<ConditionIndex, NodeIndex[]>
        [ReadOnly]
        public NativeParallelMultiHashMap<int, int> ConditionConnections;

        // Resolve specific
        [ReadOnly]
        public RunData RunData;

        // Results
        public NativeList<NodeData> Result;
        public NativeList<NodeData> PickedGoal;

        public static readonly float3 InvalidPosition = new(float.MaxValue, float.MaxValue, float.MaxValue);

        [BurstCompile]
        public void Execute()
        {
            var nodeCount = NodeConditions.Count();
            var runData = RunData;

            var openSet = new NativeHashMap<int, NodeData>(nodeCount, Allocator.Temp);
            var closedSet = new NativeHashMap<int, NodeData>(nodeCount, Allocator.Temp);

            // Add each start node's (goal) connections to the open set
            foreach (var i in runData.StartIndex)
            {
                var nodeData = new NodeData
                {
                    Index = i,
                    G = RunData.Costs[i],
                    P = RunData.Costs[i],
                    H = int.MaxValue,
                    ParentIndex = -1,
                    Position = InvalidPosition,
                };

                // We're assuming the start node is always a goal, and as such not executable
                closedSet.TryAdd(nodeData.Index, nodeData);

                AddConnections(RunData, ref openSet, ref closedSet, nodeData);
            }

            while (!openSet.IsEmpty)
            {
                var openList = openSet.GetValueArray(Allocator.Temp);
                openList.Sort(new NodeSorter());

                var currentNode = openList[0];

                if (runData.IsExecutable[currentNode.Index])
                {
                    RetracePath(currentNode, closedSet, Result);
                    break;
                }

                closedSet.TryAdd(currentNode.Index, currentNode);
                openSet.Remove(currentNode.Index);

                // If this node has a condition that is false and has no connections, it is unresolvable
                if (HasUnresolvableCondition(currentNode.Index))
                {
                    continue;
                }

                AddConnections(RunData, ref openSet, ref closedSet, currentNode);

                openList.Dispose();
            }

            openSet.Dispose();
            closedSet.Dispose();
        }

        private void AddConnections(
            RunData runData, ref NativeHashMap<int, NodeData> openSet,
            ref NativeHashMap<int, NodeData> closedSet, NodeData currentNode
        )
        {
            foreach (var conditionIndex in NodeConditions.GetValuesForKey(currentNode.Index))
            {
                if (runData.ConditionsMet[conditionIndex])
                {
                    continue;
                }

                foreach (var neighborIndex in ConditionConnections.GetValuesForKey(conditionIndex))
                {
                    if (closedSet.ContainsKey(neighborIndex))
                    {
                        continue;
                    }

                    if (!runData.IsEnabled[neighborIndex])
                    {
                        continue;
                    }

                    var neighborPosition = GetPosition(currentNode, neighborIndex);

                    // The cost with distance from the current node to the neighbour node
                    var newParentG = GetNewCost(currentNode, neighborIndex, neighborPosition);
                    // The cost with distance from the agent to the neighbour node
                    var newG = newParentG + GetDistanceCost(runData.AgentPosition, neighborPosition);
                    NodeData neighbor;

                    // Current neighbour is not in the open set
                    if (!openSet.TryGetValue(neighborIndex, out neighbor))
                    {
                        neighbor = new NodeData
                        {
                            Index = neighborIndex,
                            P = newParentG,
                            G = newG,
                            H = GetHeuristic(neighborIndex),
                            ParentIndex = currentNode.Index,
                            Position = neighborPosition,
                        };
                        openSet.Add(neighborIndex, neighbor);
                        continue;
                    }

                    // This neighbour has a lower cost
                    if (newG < neighbor.G)
                    {
                        neighbor.G = newG;
                        neighbor.P = newParentG;
                        neighbor.ParentIndex = currentNode.Index;
                        neighbor.Position = neighborPosition;

                        openSet.Remove(neighborIndex);
                        openSet.Add(neighborIndex, neighbor);
                    }
                }
            }
        }

        private float GetNewCost(NodeData currentNode, int neighborIndex, float3 neighborPosition)
        {
            return currentNode.P + RunData.Costs[neighborIndex] +
                   GetDistanceCost(currentNode, neighborPosition);
        }

        private float GetHeuristic(int neighborIndex)
        {
            return UnmetConditionCost(neighborIndex);
        }

        private float GetDistanceCost(NodeData previousNode, float3 currentPosition)
        {
            return GetDistanceCost(previousNode.Position, currentPosition);
        }

        private float GetDistanceCost(float3 previousPosition, float3 currentPosition)
        {
            if (previousPosition.Equals(InvalidPosition) || currentPosition.Equals(InvalidPosition))
            {
                return 0f;
            }

            return math.distance(previousPosition, currentPosition) * RunData.DistanceMultiplier;
        }

        private float3 GetPosition(NodeData currentNode, int currentIndex)
        {
            var pos = RunData.Positions[currentIndex];

            if (pos.Equals(InvalidPosition))
                return currentNode.Position;

            return pos;
        }

        private void RetracePath(NodeData startNode, NativeHashMap<int, NodeData> closedSet, NativeList<NodeData> path)
        {
            var currentNode = startNode;
            while (currentNode.ParentIndex != -1)
            {
                path.Add(currentNode);
                currentNode = closedSet[currentNode.ParentIndex];
            }

            PickedGoal.Add(currentNode);
        }

        private bool HasUnresolvableCondition(int currentIndex)
        {
            foreach (var conditionIndex in NodeConditions.GetValuesForKey(currentIndex))
            {
                if (RunData.ConditionsMet[conditionIndex])
                {
                    continue;
                }

                if (!ConditionConnections.GetValuesForKey(conditionIndex).MoveNext())
                {
                    return true;
                }
            }

            return false;
        }

        private float UnmetConditionCost(int currentIndex)
        {
            var cost = 0f;
            foreach (var conditionIndex in NodeConditions.GetValuesForKey(currentIndex))
            {
                if (!RunData.ConditionsMet[conditionIndex])
                {
                    cost += GetCheapestCostForCondition(conditionIndex);
                }
            }

            return cost;
        }

        private float GetCheapestCostForCondition(int conditionIndex)
        {
            var cost = float.MaxValue;
            foreach (var nodeIndex in ConditionConnections.GetValuesForKey(conditionIndex))
            {
                if (RunData.Costs[nodeIndex] < cost)
                    cost = RunData.Costs[nodeIndex];
            }

            return cost;
        }
    }
}
