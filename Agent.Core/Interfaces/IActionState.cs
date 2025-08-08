//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap.Agent
{
    public interface IActionState
    {
        bool HasPerformed { get; }
        IAction Action { get; }
        public IAction PreviousAction { get; }
        IActionRunState RunState { get; set; }
        IActionData Data { get; }

        void SetAction(IAction action, IActionData data);
        void Reset();
    }
}