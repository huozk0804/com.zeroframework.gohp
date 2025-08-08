//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public class LocalWorldData : WorldDataBase, ILocalWorldData
    {
        protected override bool IsLocal => true;
        public IGlobalWorldData GlobalData { get; private set; }

        public void SetParent(IGlobalWorldData globalData)
        {
            GlobalData = globalData;
        }

        public override (bool Exists, int Value) GetWorldValue(Type worldKey)
        {
            if (States.TryGetValue(worldKey, out var state))
                return (true, state.Value);

            return GlobalData.GetWorldValue(worldKey);
        }

        public override ITarget GetTargetValue(Type targetKey)
        {
            if (Targets.TryGetValue(targetKey, out var value))
                return value.Value;

            return GlobalData.GetTargetValue(targetKey);
        }

        public override IWorldDataState<ITarget> GetTargetState(Type targetKey)
        {
            if (Targets.TryGetValue(targetKey, out var value))
                return value;

            return GlobalData.GetTargetState(targetKey);
        }

        public override IWorldDataState<int> GetWorldState(Type worldKey)
        {
            if (States.TryGetValue(worldKey, out var state))
                return state;

            return GlobalData.GetWorldState(worldKey);
        }
    }
}