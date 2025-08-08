//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    // Backwards compatability for v2 actions
    public abstract class GoapActionBase<TActionData> : GoapActionBase<TActionData, EmptyActionProperties>
        where TActionData : IActionData, new() { }

    public abstract class GoapActionBase<TActionData, TActionProperties> : AgentActionBase<TActionData, TActionProperties>, IGoapAction
        where TActionData : IActionData, new()
        where TActionProperties : class, IActionProperties, new()
    {
        public IActionConfig Config { get; private set; }

        public Guid Guid { get; } = Guid.NewGuid();
        public IEffect[] Effects => Config.Effects;
        public ICondition[] Conditions => Config.Conditions;

        public override TActionProperties Properties => Config.Properties as TActionProperties;

        /// <summary>
        ///     Sets the configuration for the action.
        /// </summary>
        /// <param name="config">The action configuration.</param>
        public void SetConfig(IActionConfig config)
        {
            Config = config;
        }

        /// <summary>
        ///     Returns the cost of the action.
        /// </summary>
        /// <param name="agent">The action receiver.</param>
        /// <param name="references">The component references.</param>
        /// <param name="target">The target of the action.</param>
        /// <returns>The cost of the action.</returns>
        public virtual float GetCost(IActionReceiver agent, IComponentReference references, ITarget target)
        {
            return Config.BaseCost;
        }

        /// <summary>
        ///     Gets the move mode for the action.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <returns>The move mode for the action.</returns>
        public ActionMoveMode GetMoveMode(IMonoAgent agent)
        {
            return Config.MoveMode;
        }

        /// <summary>
        ///     Gets the stopping distance for the action.
        /// </summary>
        /// <returns>The stopping distance for the action.</returns>
        public virtual float GetStoppingDistance()
        {
            return Config.StoppingDistance;
        }

        /// <summary>
        ///     Determines whether the agent is in range for the action.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="distance">The distance to the target.</param>
        /// <param name="data">The action data.</param>
        /// <param name="references">The component references.</param>
        /// <returns>True if the agent is in range, otherwise false.</returns>
        public bool IsInRange(IMonoAgent agent, float distance, IActionData data, IComponentReference references)
        {
            return IsInRange(agent, distance, (TActionData)data, references);   
        }

        /// <summary>
        ///     Determines whether the agent is in range for the action.
        /// </summary>
        /// <param name="agent">The agent.</param>
        /// <param name="distance">The distance to the target.</param>
        /// <param name="data">The action data.</param>
        /// <param name="references">The component references.</param>
        /// <returns>True if the agent is in range, otherwise false.</returns>
        public virtual bool IsInRange(IMonoAgent agent, float distance, TActionData data, IComponentReference references)
        {
            return distance <= GetStoppingDistance();
        }
        
        /// <summary>
        ///     Determines whether the action is valid. Don't override this method, use IsValid(IActionReceiver agent, TActionData
        ///     data) instead.
        /// </summary>
        /// <param name="agent">The action receiver.</param>
        /// <param name="data">The action data.</param>
        /// <returns>True if the action is valid, otherwise false.</returns>
        public bool IsValid(IActionReceiver agent, IActionData data)
        {
            if (agent.ActionProvider is not GoapActionProvider goapAgent)
                return false;

            if (Config.ValidateConditions && !goapAgent.AgentType.AllConditionsMet(goapAgent, this))
            {
                agent.Logger.Warning($"Conditions not met: {Config.Name}");
                return false;
            }

            if (Config.ValidateTarget && Config.RequiresTarget)
            {
                if (data.Target == null)
                {
                    agent.Logger.Warning($"No target found for: {Config.Name}");
                    return false;
                }

                if (!data.Target.IsValid())
                {
                    agent.Logger.Warning($"Target became invalid: {Config.Name}");
                    return false;
                }
            }

            return IsValid(agent, (TActionData) data);
        }

        /// <summary>
        ///     Determines whether the action is valid with the specified action data. This is the method you should overwrite (in
        ///     most instances).
        /// </summary>
        /// <param name="agent">The action receiver.</param>
        /// <param name="data">The action data.</param>
        /// <returns>True if the action is valid, otherwise false.</returns>
        public virtual bool IsValid(IActionReceiver agent, TActionData data)
        {
            return true;
        }

        /// <summary>
        ///     Determines whether the action is executable.
        /// </summary>
        /// <param name="agent">The action receiver.</param>
        /// <param name="conditionsMet">Whether the conditions are met.</param>
        /// <returns>True if the action is executable, otherwise false.</returns>
        public bool IsExecutable(IActionReceiver agent, bool conditionsMet)
        {
            var goapAgent = agent.Injector.GetCachedComponent<GoapActionProvider>();

            if (goapAgent == null)
                return false;

            if (!conditionsMet)
                return false;

            if (Config.RequiresTarget && goapAgent.WorldData.GetTarget(this) == null)
                return false;

            return true;
        }

        /// <summary>
        ///     Determines whether the action is enabled. This is used by the planner.
        /// </summary>
        /// <param name="agent">The action receiver.</param>
        /// <returns>True if the action is enabled, otherwise false.</returns>
        public bool IsEnabled(IActionReceiver agent)
        {
            return IsEnabled(agent, agent.Injector);
        }

        /// <summary>
        ///     Determines whether the action is enabled. This is used by the planner.
        /// </summary>
        /// <param name="receiver">The action receiver.</param>
        /// <param name="references">The component references.</param>
        /// <returns>True if the action is enabled, otherwise false.</returns>
        public virtual bool IsEnabled(IActionReceiver receiver, IComponentReference references)
        {
            return !receiver.ActionProvider.IsDisabled(this);
        }

        /// <summary>
        ///     Enables the action.
        /// </summary>
        public void Enable(IActionReceiver receiver)
        {
            receiver.ActionProvider.Enable(this);
        }

        /// <summary>
        ///     Disables the action.
        /// </summary>
        /// <param name="receiver">The action receiver</param>
        /// <param name="disabler">The action disabler.</param>
        public void Disable(IActionReceiver receiver, IActionDisabler disabler)
        {
            receiver.ActionProvider.Disable(this, disabler);
        }
    }
}
