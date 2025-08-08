//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    public class WorldKeyBuilder : KeyBuilderBase<IWorldKey>
    {
        protected override void InjectData(IWorldKey key)
        {
            if (key is WorldKeyBase worldKey)
                worldKey.Name = key.GetType().GetGenericTypeName();
        }
    }
}