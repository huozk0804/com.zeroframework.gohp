//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------


namespace ZeroFramework.Goap.Agent
{
    public interface IActionProvider
    {
        IActionReceiver Receiver { get; set; }
        void ResolveAction();
        bool IsDisabled(IAction action);
        void Enable(IAction action);
        void Disable(IAction action, IActionDisabler disabler);
    }
}