//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    /// <summary>
    /// 类引用状态枚举 - 用于表示类引用的不同状态
    /// </summary>
    public enum ClassRefStatus
    {
        /// <summary>
        /// 空引用状态
        /// </summary>
        Empty,

        /// <summary>
        /// 无引用状态
        /// </summary>
        None,

        /// <summary>
        /// 仅名称引用状态
        /// </summary>
        Name,

        /// <summary>
        /// 仅ID引用状态
        /// </summary>
        Id,

        /// <summary>
        /// 完整引用状态
        /// </summary>
        Full,
    }
}
