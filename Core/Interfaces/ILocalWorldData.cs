//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public interface ILocalWorldData : IWorldData
    {
        IGlobalWorldData GlobalData { get; }

        void SetParent(IGlobalWorldData globalData);
    }
}
