//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public class Condition : ICondition
    {
        public IWorldKey WorldKey { get; set; }
        public Comparison Comparison { get; set; }
        public int Amount { get; set; }

        public Condition()
        {
        }

        public Condition(IWorldKey worldKey, Comparison comparison, int amount)
        {
            WorldKey = worldKey;
            Comparison = comparison;
            Amount = amount;
        }
    }
}