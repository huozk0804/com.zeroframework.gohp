//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    /// <summary>
    /// 目标请求委托 - 用于处理目标请求事件
    /// </summary>
    public delegate void GoalRequestDelegate(IGoalRequest goal);

    /// <summary>
    /// 目标委托 - 用于处理目标事件
    /// </summary>
    public delegate void GoalDelegate(IGoal goal);

    /// <summary>
    /// 代理目标委托 - 用于处理代理的目标事件
    /// </summary>
    public delegate void AgentGoalDelegate(IMonoGoapActionProvider actionProvider, IGoal goal);

    /// <summary>
    /// 代理目标请求委托 - 用于处理代理的目标请求事件
    /// </summary>
    public delegate void AgentGoalRequestDelegate(IMonoGoapActionProvider actionProvider, IGoalRequest request);

    /// <summary>
    /// 代理类型委托 - 用于处理代理类型事件
    /// </summary>
    public delegate void AgentTypeDelegate(IAgentType agentType);

    /// <summary>
    /// GOAP代理委托 - 用于处理GOAP代理事件
    /// </summary>
    public delegate void GoapAgentDelegate(IMonoGoapActionProvider actionProvider);

    /// <summary>
    /// GOAP动作委托 - 用于处理GOAP动作事件
    /// </summary>
    public delegate void GoapActionDelegate(IGoapAction action);

    /// <summary>
    /// GOAP代理动作委托 - 用于处理GOAP代理的动作事件
    /// </summary>
    public delegate void GoapAgentActionDelegate(IMonoGoapActionProvider actionProvider, IGoapAction action);
}
