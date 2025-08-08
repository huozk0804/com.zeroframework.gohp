//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    public interface IGraphResolver
    {
        IResolveHandle StartResolve(RunData runData);
        IEnabledBuilder GetEnabledBuilder();
        IExecutableBuilder GetExecutableBuilder();
        IPositionBuilder GetPositionBuilder();
        ICostBuilder GetCostBuilder();
        IGraph GetGraph();
        int GetIndex(IConnectable action);
        IGoapAction GetAction(int index);
        void Dispose();
        IConditionBuilder GetConditionBuilder();
    }
}