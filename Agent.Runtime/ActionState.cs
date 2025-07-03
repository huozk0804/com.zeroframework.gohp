//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap.Agent
{
    public class ActionState : IActionState
    {
        public bool HasPerformed => RunState != null;
        public IAction Action { get; private set; }
        public IAction PreviousAction { get; private set; }
        public IActionRunState RunState { get; set; }
        public IActionData Data { get; private set; }

        public void SetAction(IAction action, IActionData data)
        {
            Action = action;
            RunState = null;
            Data = data;
        }

        public void Reset()
        {
            PreviousAction = Action;
            Action = null;
            RunState = null;
            Data = null;
        }
    }
}
