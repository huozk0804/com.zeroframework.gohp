//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public class ActionBuilder<T> : ActionBuilder where T : IAction
    {
        public ActionBuilder(WorldKeyBuilder worldKeyBuilder, TargetKeyBuilder targetKeyBuilder) : base(typeof(T),
            worldKeyBuilder, targetKeyBuilder)
        {
        }

        /// <summary>
        ///     Sets the target key for the action.
        ///     为操作设置目标键。
        /// </summary>
        /// <typeparam name="TTargetKey">The type of the target key.</typeparam>
        /// <returns>The current instance of <see cref="ActionBuilder{T}" />.</returns>
        public ActionBuilder<T> SetTarget<TTargetKey>() where TTargetKey : ITargetKey
        {
            config.Target = targetKeyBuilder.GetKey<TTargetKey>();
            return this;
        }

        /// <summary>
        ///     Sets the base cost for the action.
        ///     设置操作的基本费用。
        /// </summary>
        /// <param name="baseCost">The base cost.</param>
        /// <returns>The current instance of <see cref="ActionBuilder{T}" />.</returns>
        public ActionBuilder<T> SetBaseCost(float baseCost)
        {
            config.BaseCost = baseCost;
            return this;
        }

        /// <summary>
        ///     Sets whether the target should be validated when running the action.
        ///     设置在运行操作时是否应该验证目标。
        /// </summary>
        /// <param name="validate">True if the target should be validated; otherwise, false.</param>
        /// <returns>The current instance of <see cref="ActionBuilder{T}" />.</returns>
        public ActionBuilder<T> SetValidateTarget(bool validate)
        {
            config.ValidateTarget = validate;
            return this;
        }

        /// <summary>
        ///     Sets whether the action requires a target.
        ///     设置操作是否需要目标。
        /// </summary>
        /// <param name="requiresTarget">True if the action requires a target; otherwise, false.</param>
        /// <returns>The current instance of <see cref="ActionBuilder{T}" />.</returns>
        public ActionBuilder<T> SetRequiresTarget(bool requiresTarget)
        {
            config.RequiresTarget = requiresTarget;
            return this;
        }

        /// <summary>
        ///     Sets whether the conditions should be validated when running the action.
        ///     设置在运行操作时是否应验证条件。
        /// </summary>
        /// <param name="validate">True if the conditions should be validated; otherwise, false.</param>
        /// <returns>The current instance of <see cref="ActionBuilder{T}" />.</returns>
        public ActionBuilder<T> SetValidateConditions(bool validate)
        {
            config.ValidateConditions = validate;
            return this;
        }

        /// <summary>
        ///     Sets the stopping distance for the action. This is the distance at which the action will stop moving towards the
        ///     target.
        ///     设置动作的停止距离。这是动作停止向目标移动的距离。
        /// </summary>
        /// <param name="inRange">The stopping distance.</param>
        /// <returns>The current instance of <see cref="ActionBuilder{T}" />.</returns>
        public ActionBuilder<T> SetStoppingDistance(float inRange)
        {
            config.StoppingDistance = inRange;
            return this;
        }

        /// <summary>
        ///     Sets the move mode for the action.
        ///     设置动作的移动模式。
        /// </summary>
        /// <param name="moveMode">The move mode.</param>
        /// <returns>The current instance of <see cref="ActionBuilder{T}" />.</returns>
        public ActionBuilder<T> SetMoveMode(ActionMoveMode moveMode)
        {
            config.MoveMode = moveMode;
            return this;
        }

        /// <summary>
        ///     Adds a condition to the action.
        ///     向动作添加条件。
        /// </summary>
        /// <typeparam name="TWorldKey">The type of the world key.</typeparam>
        /// <param name="comparison">The comparison type.</param>
        /// <param name="amount">The amount for the condition.</param>
        /// <returns>The current instance of <see cref="ActionBuilder{T}" />.</returns>
        public ActionBuilder<T> AddCondition<TWorldKey>(Comparison comparison, int amount)
            where TWorldKey : IWorldKey
        {
            conditions.Add(new Condition
            {
                WorldKey = worldKeyBuilder.GetKey<TWorldKey>(),
                Comparison = comparison,
                Amount = amount,
            });

            return this;
        }

        /// <summary>
        ///     Adds an effect to the action.
        ///     为动作添加效果。
        /// </summary>
        /// <typeparam name="TWorldKey">The type of the world key.</typeparam>
        /// <param name="type">The effect type.</param>
        /// <returns>The current instance of <see cref="ActionBuilder{T}" />.</returns>
        public ActionBuilder<T> AddEffect<TWorldKey>(EffectType type)
            where TWorldKey : IWorldKey
        {
            effects.Add(new Effect
            {
                WorldKey = worldKeyBuilder.GetKey<TWorldKey>(),
                Increase = type == EffectType.Increase,
            });

            return this;
        }

        /// <summary>
        ///     Sets the properties for the action.
        ///     设置动作的属性。
        /// </summary>
        /// <param name="properties">The action properties.</param>
        /// <returns>The current instance of <see cref="ActionBuilder{T}" />.</returns>
        public ActionBuilder<T> SetProperties(IActionProperties properties)
        {
            ValidateProperties(properties);

            config.Properties = properties;
            return this;
        }

        /// <summary>
        ///     Sets the callback for when the action is created. This can be used to set up the action with custom data.
        ///     设置动作创建时的回调。这可用于设置带有自定义数据的操作。
        /// </summary>
        /// <param name="callback">The callback action.</param>
        /// <returns>The current instance of <see cref="ActionBuilder{T}" />.</returns>
        public ActionBuilder<T> SetCallback(Action<T> callback)
        {
            config.Callback = (obj) => callback((T)obj);
            return this;
        }
    }

    public class ActionBuilder
    {
        protected readonly ActionConfig config;
        protected readonly List<ICondition> conditions = new();
        protected readonly List<IEffect> effects = new();
        protected readonly WorldKeyBuilder worldKeyBuilder;
        protected readonly TargetKeyBuilder targetKeyBuilder;
        protected readonly Type actionType;

        public ActionBuilder(Type actionType, WorldKeyBuilder worldKeyBuilder, TargetKeyBuilder targetKeyBuilder)
        {
            this.actionType = actionType;
            this.worldKeyBuilder = worldKeyBuilder;
            this.targetKeyBuilder = targetKeyBuilder;

            var propType = this.actionType.GetPropertiesType();

            config = new ActionConfig
            {
                Name = actionType.Name,
                ClassType = actionType.AssemblyQualifiedName,
                BaseCost = 1,
                StoppingDistance = 0.5f,
                RequiresTarget = true,
                ValidateConditions = true,
                ValidateTarget = true,
                Properties = (IActionProperties)Activator.CreateInstance(propType),
            };
        }

        protected void ValidateProperties(IActionProperties properties)
        {
            var actionPropsType = actionType.GetPropertiesType();

            if (actionPropsType == properties.GetType())
                return;

            throw new ArgumentException(
                $"The provided properties do not match the expected type '{actionPropsType.Name}'.",
                nameof(properties));
        }

        public IActionConfig Build()
        {
            config.Conditions = conditions.ToArray();
            config.Effects = effects.ToArray();

            return config;
        }

        public static ActionBuilder<TAction> Create<TAction>(WorldKeyBuilder worldKeyBuilder,
            TargetKeyBuilder targetKeyBuilder)
            where TAction : IAction
        {
            return new ActionBuilder<TAction>(worldKeyBuilder, targetKeyBuilder);
        }
    }
}