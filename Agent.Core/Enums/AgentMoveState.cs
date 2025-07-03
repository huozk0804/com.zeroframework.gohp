//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------


namespace ZeroFramework.Goap.Agent
{
    /// <summary>
    /// 代理移动状态枚举 - 表示代理的移动状态
    /// </summary>
    public enum AgentMoveState
    {
        /// <summary>
        /// 空闲
        /// </summary>
        Idle,

        /// <summary>
        /// 在范围内
        /// </summary>
        InRange,

        /// <summary>
        /// 不在范围内
        /// </summary>
        NotInRange,
    }
}