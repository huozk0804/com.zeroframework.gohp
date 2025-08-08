//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap.Agent
{
    /// <summary>
    /// 代理接口 - 定义代理的属性和行为
    /// </summary>
    public interface IAgent : IActionReceiver
    {
        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool IsPaused { get; set; }

        /// <summary>
        /// 当前代理状态
        /// </summary>
        AgentState State { get; }

        /// <summary>
        /// 当前移动状态
        /// </summary>
        AgentMoveState MoveState { get; }

        /// <summary>
        /// 距离观察者
        /// </summary>
        IAgentDistanceObserver DistanceObserver { get; }

        /// <summary>
        /// 当前目标
        /// </summary>
        ITarget CurrentTarget { get; }

        /// <summary>
        /// 初始化代理
        /// </summary>
        void Initialize();

        /// <summary>
        /// 运行代理
        /// </summary>
        void Run();

        /// <summary>
        /// 完成当前动作
        /// </summary>
        /// <param name="resolveAction">是否解析动作</param>
        void CompleteAction(bool resolveAction = true);

        /// <summary>
        /// 解析当前动作
        /// </summary>
        void ResolveAction();
    }
}