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
    public abstract class WorldDataBase : IWorldData
    {
        protected abstract bool IsLocal { get; }
        public Dictionary<Type, IWorldDataState<int>> States { get; } = new();
        public Dictionary<Type, IWorldDataState<ITarget>> Targets { get; } = new();

        public ITarget GetTarget(IGoapAction action)
        {
            if (action == null)
                return null;

            if (action.Config.Target == null)
                return null;

            return GetTargetValue(action.Config.Target.GetType());
        }

        public bool IsTrue<TWorldKey>(Comparison comparison, int value)
        {
            return IsTrue(typeof(TWorldKey), comparison, value);
        }

        public bool IsTrue(IWorldKey worldKey, Comparison comparison, int value)
        {
            return IsTrue(worldKey.GetType(), comparison, value);
        }

        public bool IsTrue(Type worldKey, Comparison comparison, int value)
        {
            var (exists, state) = GetWorldValue(worldKey);

            if (!exists)
                return false;

            switch (comparison)
            {
                case Comparison.GreaterThan:
                    return state > value;
                case Comparison.GreaterThanOrEqual:
                    return state >= value;
                case Comparison.SmallerThan:
                    return state < value;
                case Comparison.SmallerThanOrEqual:
                    return state <= value;
            }

            return false;
        }

        public void SetState(IWorldKey key, int state)
        {
            SetState(key.GetType(), state);
        }

        public void SetState<TKey>(int state) where TKey : IWorldKey
        {
            SetState(typeof(TKey), state);
        }

        public void SetState(Type key, int state)
        {
            if (key == null)
                return;

            if (States.ContainsKey(key))
            {
                States[key].Value = state;
                States[key].Timer.Touch();
                return;
            }

            States.Add(key, new WorldDataState<int>
            {
                Key = key,
                Value = state,
                IsLocal = IsLocal,
            });
        }

        public void SetTarget(ITargetKey key, ITarget target)
        {
            SetTarget(key.GetType(), target);
        }

        public void SetTarget<TKey>(ITarget target) where TKey : ITargetKey
        {
            SetTarget(typeof(TKey), target);
        }

        private void SetTarget(Type key, ITarget target)
        {
            if (key == null)
                return;

            if (Targets.ContainsKey(key))
            {
                Targets[key].Value = target;
                Targets[key].Timer.Touch();
                return;
            }

            Targets.Add(key, new WorldDataState<ITarget>
            {
                Key = key,
                Value = target,
                IsLocal = IsLocal,
            });
        }

        public (bool Exists, int Value) GetWorldValue<TKey>(TKey worldKey) where TKey : IWorldKey => GetWorldValue(worldKey.GetType());

        public abstract (bool Exists, int Value) GetWorldValue(Type worldKey);
        public abstract ITarget GetTargetValue(Type targetKey);
        public abstract IWorldDataState<ITarget> GetTargetState(Type targetKey);
        public abstract IWorldDataState<int> GetWorldState(Type worldKey);
    }

    public class WorldDataState<T> : IWorldDataState<T>
    {
        public bool IsLocal { get; set; }
        public Type Key { get; set; }
        public T Value { get; set; }
        public ITimer Timer { get; } = new Agent.Timer();
    }
}