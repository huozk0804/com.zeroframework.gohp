//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using ZeroFramework.Goap.Agent;

namespace ZeroFramework.Goap
{
    public interface IGoapInjector
    {
        void Inject(IAction action);
        void Inject(IGoal goal);
        void Inject(ISensor sensor);
    }
}