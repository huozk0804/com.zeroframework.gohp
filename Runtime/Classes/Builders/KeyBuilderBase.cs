//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ZeroFramework.Goap
{
    public abstract class KeyBuilderBase<TInterface>
    {
        private readonly Dictionary<Type, TInterface> _keys = new();

        public TInterface GetKey<TKey>()
            where TKey : TInterface
        {
            var type = typeof(TKey);

            if (_keys.TryGetValue(type, out var key))
            {
                return key;
            }

            key = (TInterface) Activator.CreateInstance(type);

            InjectData(key);
            _keys.Add(type, key);

            return key;
        }

        protected abstract void InjectData(TInterface key);
    }
}
