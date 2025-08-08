//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    public class Effect : IEffect
    {
        public IWorldKey WorldKey { get; set; }
        public bool Increase { get; set; }
    }
}