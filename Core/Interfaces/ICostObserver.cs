//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public interface ICostObserver
    {
        float GetCost(IConnectable current, IConnectable[] path);
        void SetWorldData(IWorldData worldData);
    }
}
