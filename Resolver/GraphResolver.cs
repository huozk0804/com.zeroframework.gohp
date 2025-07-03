//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Unity.Collections;

namespace ZeroFramework.Goap
{
    public class GraphResolver : IGraphResolver
    {
        private readonly List<INode> _indexList;
        private readonly List<IConnectable> _actionIndexList;

        private readonly List<INodeCondition> _conditionList;
        private readonly List<ICondition> _conditionIndexList;

        private NativeParallelMultiHashMap<int, int> _nodeConditions;
        private NativeParallelMultiHashMap<int, int> _conditionConnections;

        private readonly Graph _graph;
        private readonly Queue<ResolveHandle> _handles = new();

        public GraphResolver(IConnectable[] actions, IKeyResolver keyResolver)
        {
            _graph = new GraphBuilder(keyResolver).Build(actions);

            _indexList = _graph.AllNodes.ToList();
            _actionIndexList = _indexList.Select(x => x.Action).ToList();

            _conditionList = _indexList.SelectMany(x => x.Conditions).ToList();
            _conditionIndexList = _conditionList.Select(x => x.Condition).ToList();

            CreateNodeConditions();
            CreateConditionConnections();
        }

        private void CreateNodeConditions()
        {
            var map = new NativeParallelMultiHashMap<int, int>(_indexList.Count, Allocator.Persistent);

            for (var i = 0; i < _indexList.Count; i++)
            {
                var conditions = _indexList[i].Conditions
                    .Select(x => _conditionIndexList.IndexOf(x.Condition));

                foreach (var condition in conditions)
                {
                    map.Add(i, condition);
                }
            }

            _nodeConditions = map;
        }

        private void CreateConditionConnections()
        {
            var map = new NativeParallelMultiHashMap<int, int>(_conditionIndexList.Count, Allocator.Persistent);

            for (var i = 0; i < _conditionIndexList.Count; i++)
            {
                var connections = _conditionList[i].Connections
                    .Select(x => _indexList.IndexOf(x));

                foreach (var connection in connections)
                {
                    map.Add(i, connection);
                }
            }

            _conditionConnections = map;
        }

        public IResolveHandle StartResolve(RunData runData)
        {
            return GetResolveHandle().Start(_nodeConditions, _conditionConnections, runData);
        }

        public IEnabledBuilder GetEnabledBuilder()
        {
            return new EnabledBuilder(_actionIndexList);
        }

        public IExecutableBuilder GetExecutableBuilder()
        {
            return new ExecutableBuilder(_actionIndexList);
        }

        public IPositionBuilder GetPositionBuilder()
        {
            return new PositionBuilder(_actionIndexList);
        }

        public ICostBuilder GetCostBuilder()
        {
            return new CostBuilder(_actionIndexList);
        }

        public IConditionBuilder GetConditionBuilder()
        {
            return new ConditionBuilder(_conditionIndexList);
        }

        public IGraph GetGraph()
        {
            return _graph;
        }

        public int GetIndex(IConnectable action) => _actionIndexList.IndexOf(action);
        public IGoapAction GetAction(int index) => _actionIndexList[index] as IGoapAction;
        public IGoal GetGoal(int index) => _actionIndexList[index] as IGoal;

        public void Dispose()
        {
            _nodeConditions.Dispose();
            _conditionConnections.Dispose();
        }

        private ResolveHandle GetResolveHandle()
        {
            if (_handles.TryDequeue(out var handle))
                return handle;

            return new ResolveHandle(this);
        }

        public void Release(ResolveHandle handle)
        {
            _handles.Enqueue(handle);
        }
    }
}