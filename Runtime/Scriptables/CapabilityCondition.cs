//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace Keystone.Goap
{
    [Serializable]
    public class CapabilityCondition
    {
        public ClassRef worldKey = new();
        public Comparison comparison;
        public int amount;

        public CapabilityCondition() { }

        public CapabilityCondition(string data)
        {
            var split = data.Split(' ');
            worldKey.Name = split[0];
            comparison = split[1].FromName();
            amount = int.Parse(split[2]);
        }

        public override string ToString() => $"{worldKey.Name} {comparison.ToName()} {amount}";
    }
}
