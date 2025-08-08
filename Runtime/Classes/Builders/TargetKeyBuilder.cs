//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    public class TargetKeyBuilder : KeyBuilderBase<ITargetKey>
    {
        protected override void InjectData(ITargetKey key)
        {
            if (key is TargetKeyBase targetKey)
                targetKey.Name = key.GetType().GetGenericTypeName();
        }
    }
}