//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------


namespace Keystone.Goap.Agent
{
    /// <summary>
    /// 动作接口 - 定义动作的基本行为和属性
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// 获取动作的移动模式
        /// </summary>
        /// <param name="agent">代理对象</param>
        /// <returns>动作移动模式</returns>
        ActionMoveMode GetMoveMode(IMonoAgent agent);

        /// <summary>
        /// 获取停止距离
        /// </summary>
        /// <returns>停止距离</returns>
        float GetStoppingDistance();

        /// <summary>
        /// 判断代理是否在范围内
        /// </summary>
        /// <param name="agent">代理对象</param>
        /// <param name="distance">距离</param>
        /// <param name="data">动作数据</param>
        /// <param name="references">组件引用</param>
        /// <returns>是否在范围内</returns>
        bool IsInRange(IMonoAgent agent, float distance, IActionData data, IComponentReference references);

        /// <summary>
        /// 获取动作数据
        /// </summary>
        /// <returns>动作数据</returns>
        IActionData GetData();

        /// <summary>
        /// 当动作被创建时调用
        /// </summary>
        void Created();

        /// <summary>
        /// 检查动作是否有效
        /// </summary>
        /// <param name="agent">动作接收者</param>
        /// <param name="data">动作数据</param>
        /// <returns>是否有效</returns>
        bool IsValid(IActionReceiver agent, IActionData data);

        /// <summary>
        /// 当动作被分配/开始时调用
        /// </summary>
        /// <param name="agent">代理对象</param>
        /// <param name="data">动作数据</param>
        void Start(IMonoAgent agent, IActionData data);

        /// <summary>
        /// 第一次执行动作时调用
        /// </summary>
        /// <param name="agent">代理对象</param>
        /// <param name="data">动作数据</param>
        void BeforePerform(IMonoAgent agent, IActionData data);

        /// <summary>
        /// 动作执行时每帧调用
        /// </summary>
        /// <param name="agent">代理对象</param>
        /// <param name="data">动作数据</param>
        /// <param name="context">动作上下文</param>
        /// <returns>动作运行状态</returns>
        IActionRunState Perform(IMonoAgent agent, IActionData data, IActionContext context);

        /// <summary>
        /// 当动作被停止时调用（被强制或其他原因）
        /// </summary>
        /// <param name="agent">代理对象</param>
        /// <param name="data">动作数据</param>
        void Stop(IMonoAgent agent, IActionData data);

        /// <summary>
        /// 当动作完成时调用
        /// </summary>
        /// <param name="agent">代理对象</param>
        /// <param name="data">动作数据</param>
        void Complete(IMonoAgent agent, IActionData data);

        /// <summary>
        /// 判断动作是否可执行
        /// </summary>
        /// <param name="agent">动作接收者</param>
        /// <param name="conditionsMet">条件是否满足</param>
        /// <returns>是否可执行</returns>
        bool IsExecutable(IActionReceiver agent, bool conditionsMet);
    }
}
