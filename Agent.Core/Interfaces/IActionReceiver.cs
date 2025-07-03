//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using UnityEngine;

namespace ZeroFramework.Goap.Agent
{
    public interface IActionReceiver
    {
        IDataReferenceInjector Injector { get; }
        IActionState ActionState { get; }
        IAgentTimers Timers { get; }
        ILogger<IMonoAgent> Logger { get; }
        IAgentEvents Events { get; }
        Transform Transform { get; }
        IActionProvider ActionProvider { get; }
        bool IsPaused { get; set; }
        void SetAction(IActionProvider actionProvider, IAction action, ITarget target);
        void StopAction(bool resolveAction = true);
    }
}
