//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using UnityEngine;

namespace ZeroFramework.Goap.Agent
{
    /// <summary>
    /// 目标接口 - 定义目标的基本属性和方法
    /// </summary>
    public interface ITarget
    {
        /// <summary>
        /// 目标位置
        /// </summary>
        public Vector3 Position { get; }
        /// <summary>
        /// 判断目标是否有效
        /// </summary>
        public bool IsValid();
    }
}