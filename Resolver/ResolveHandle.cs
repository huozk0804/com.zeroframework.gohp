//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;

namespace Keystone.Goap
{
    public class ResolveHandle : IResolveHandle
    {
        private readonly GraphResolver _graphResolver;
        private JobHandle _handle;
        private GraphResolverJob _job;
        private readonly List<IConnectable> _results = new();

        public ResolveHandle(GraphResolver graphResolver)
        {
            _graphResolver = graphResolver;
        }

        public ResolveHandle Start(NativeParallelMultiHashMap<int, int> nodeConditions,
            NativeParallelMultiHashMap<int, int> conditionConnections, RunData runData)
        {
            _job = new GraphResolverJob
            {
                NodeConditions = nodeConditions,
                ConditionConnections = conditionConnections,
                RunData = runData,
                Result = new NativeList<NodeData>(Allocator.TempJob),
                PickedGoal = new NativeList<NodeData>(Allocator.TempJob)
            };

            _handle = _job.Schedule();

            return this;
        }

        public JobResult Complete()
        {
            _handle.Complete();

            _results.Clear();

            foreach (var data in _job.Result)
            {
                _results.Add(_graphResolver.GetAction(data.Index));
            }

            IGoal goal = default;

            if (_job.PickedGoal.Length > 0)
                goal = _graphResolver.GetGoal(_job.PickedGoal[0].Index);

            _job.Result.Dispose();
            _job.PickedGoal.Dispose();

            _job.RunData.StartIndex.Dispose();
            _job.RunData.IsEnabled.Dispose();
            _job.RunData.IsExecutable.Dispose();
            _job.RunData.Positions.Dispose();
            _job.RunData.Costs.Dispose();
            _job.RunData.ConditionsMet.Dispose();

            _graphResolver.Release(this);

            return new JobResult
            {
                Actions = _results.ToArray(),
                Goal = goal,
            };
        }
    }

    public class JobResult
    {
        public IConnectable[] Actions { get; set; }
        public IGoal Goal { get; set; }
    }
}