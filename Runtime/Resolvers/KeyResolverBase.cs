//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public abstract class KeyResolverBase : IKeyResolver
    {
        protected IWorldData WorldData { get; private set; }

        public void SetWorldData(IWorldData globalWorldData)
        {
            WorldData = globalWorldData;
        }

        public abstract string GetKey(IEffect key);
        public abstract string GetKey(ICondition key);
        public abstract bool AreConflicting(IEffect effect, ICondition condition);
    }
}
