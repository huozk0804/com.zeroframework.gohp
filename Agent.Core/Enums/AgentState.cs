//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------


namespace ZeroFramework.Goap.Agent
{
    /// <summary>
    /// 代理状态枚举 - 表示代理当前的行为状态
    /// </summary>
    public enum AgentState
    {
        /// <summary>
        /// 无动作
        /// </summary>
        NoAction,

        /// <summary>
        /// 正在开始动作
        /// </summary>
        StartingAction,

        /// <summary>
        /// 正在执行动作
        /// </summary>
        PerformingAction,

        /// <summary>
        /// 正在移动到目标
        /// </summary>
        MovingToTarget,

        /// <summary>
        /// 移动中执行动作
        /// </summary>
        MovingWhilePerformingAction,
    }
}