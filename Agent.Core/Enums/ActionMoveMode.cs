//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------


namespace Keystone.Goap.Agent
{
    /// <summary>
    /// 动作移动模式枚举 - 定义动作执行时的移动方式
    /// </summary>
    public enum ActionMoveMode
    {
        /// <summary>
        /// 执行前先移动
        /// </summary>
        MoveBeforePerforming,

        /// <summary>
        /// 移动中执行
        /// </summary>
        PerformWhileMoving,
    }
}