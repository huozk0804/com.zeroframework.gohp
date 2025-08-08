//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace Keystone.Goap
{
    [Serializable]
    public class CapabilityEffect
    {
        public ClassRef worldKey = new();
        public EffectType effect;

        public override string ToString() => $"{worldKey.Name}{effect.ToName()}";
    }
}