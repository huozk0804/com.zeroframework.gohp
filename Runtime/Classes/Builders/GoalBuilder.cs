//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ZeroFramework.Goap
{
    public class GoalBuilder<T> : GoalBuilder
        where T : IGoal
    {
        public GoalBuilder(WorldKeyBuilder worldKeyBuilder) : base(typeof(T), worldKeyBuilder) { }

        /// <summary>
        ///     Sets the base cost for the goal.
        /// </summary>
        /// <param name="baseCost">The base cost.</param>
        /// <returns>The current instance of <see cref="GoalBuilder{T}" />.</returns>
        public GoalBuilder<T> SetBaseCost(float baseCost)
        {
            config.BaseCost = baseCost;
            return this;
        }

        /// <summary>
        ///     Adds a condition to the goal.
        /// </summary>
        /// <typeparam name="TWorldKey">The type of the world key.</typeparam>
        /// <param name="comparison">The comparison type.</param>
        /// <param name="amount">The amount for the condition.</param>
        /// <returns>The current instance of <see cref="GoalBuilder{T}" />.</returns>
        public GoalBuilder<T> AddCondition<TWorldKey>(Comparison comparison, int amount)
            where TWorldKey : IWorldKey
        {
            conditions.Add(new Condition(worldKeyBuilder.GetKey<TWorldKey>(), comparison, amount));
            return this;
        }

        /// <summary>
        ///     Sets the callback for the goal. This will be called when the goal is created.
        /// </summary>
        /// <param name="callback">The callback action.</param>
        /// <returns>The current instance of <see cref="GoalBuilder{T}" />.</returns>
        public GoalBuilder<T> SetCallback(Action<T> callback)
        {
            config.Callback = (obj) => callback((T) obj);
            return this;
        }
    }

    public class GoalBuilder
    {
        protected readonly GoalConfig config;
        protected readonly List<ICondition> conditions = new();
        protected readonly WorldKeyBuilder worldKeyBuilder;

        public GoalBuilder(Type type, WorldKeyBuilder worldKeyBuilder)
        {
            this.worldKeyBuilder = worldKeyBuilder;
            config = new GoalConfig(type)
            {
                BaseCost = 1,
                ClassType = type.AssemblyQualifiedName,
            };
        }

        /// <summary>
        ///     Builds the goal configuration.
        /// </summary>
        /// <returns>The built <see cref="IGoalConfig" />.</returns>
        public IGoalConfig Build()
        {
            config.Conditions = conditions;
            return config;
        }

        public static GoalBuilder<TGoal> Create<TGoal>(WorldKeyBuilder worldKeyBuilder)
            where TGoal : IGoal
        {
            return new GoalBuilder<TGoal>(worldKeyBuilder);
        }
    }
}
