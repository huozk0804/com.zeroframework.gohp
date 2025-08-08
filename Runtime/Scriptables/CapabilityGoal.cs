//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Keystone.Goap
{
    [Serializable]
    public class CapabilityGoal
    {
        public ClassRef goal = new();

        public float baseCost = 1;
        public List<CapabilityCondition> conditions = new();
    }
}