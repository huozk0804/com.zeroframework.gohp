//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap.Agent
{
    /// <summary>
    /// 动作委托 - 用于处理动作相关事件
    /// </summary>
    public delegate void ActionDelegate(IAction action);

    /// <summary>
    /// 代理动作委托 - 用于处理代理与动作相关事件
    /// </summary>
    public delegate void AgentActionDelegate(IAgent agent, IAction action);

    /// <summary>
    /// 目标委托 - 用于处理目标相关事件
    /// </summary>
    public delegate void TargetDelegate(ITarget target);

    /// <summary>
    /// 目标距离委托 - 用于处理目标距离相关事件
    /// </summary>
    public delegate void TargetRangeDelegate(ITarget target, bool inRange);

    /// <summary>
    /// 代理委托 - 用于处理代理相关事件
    /// </summary>
    public delegate void AgentDelegate(IAgent agent);

    /// <summary>
    /// 空委托 - 用于无参数事件
    /// </summary>
    public delegate void EmptyDelegate();
}