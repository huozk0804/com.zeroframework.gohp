//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using ZeroFramework.Goap.Agent;

namespace ZeroFramework.Goap
{
    public class GlobalWorldData : WorldDataBase, IGlobalWorldData
    {
        protected override bool IsLocal => false;

        public override (bool Exists, int Value) GetWorldValue(Type worldKey)
        {
            if (!States.TryGetValue(worldKey, out var state))
                return (false, 0);

            return (true, state.Value);
        }

        public override ITarget GetTargetValue(Type targetKey)
        {
            if (!Targets.TryGetValue(targetKey, out var target))
                return null;

            return target.Value;
        }

        public override IWorldDataState<ITarget> GetTargetState(Type targetKey)
        {
            if (!Targets.TryGetValue(targetKey, out var state))
                return null;

            return state;
        }

        public override IWorldDataState<int> GetWorldState(Type worldKey)
        {
            if (!States.TryGetValue(worldKey, out var state))
                return null;

            return state;
        }
    }
}