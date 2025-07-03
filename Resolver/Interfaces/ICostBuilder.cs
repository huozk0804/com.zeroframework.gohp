//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public interface ICostBuilder
    {
        ICostBuilder SetCost(IConnectable action, float cost);
        float[] Build();
        void Clear();
    }
}