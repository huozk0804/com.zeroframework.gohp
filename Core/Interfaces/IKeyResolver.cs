//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public interface IKeyResolver
    {
        string GetKey(ICondition condition);
        string GetKey(IEffect effect);
        bool AreConflicting(IEffect effect, ICondition condition);
        void SetWorldData(IWorldData globalWorldData);
    }
}
