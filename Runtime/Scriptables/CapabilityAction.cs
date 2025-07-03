//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using ZeroFramework.Goap.Agent;

namespace ZeroFramework.Goap
{
    [Serializable]
    public class CapabilityAction
    {
        public ClassRef action = new();
        public ClassRef target = new();

        [SerializeReference]
        public IActionProperties properties;

        public float baseCost = 1;
        public float stoppingDistance = 0.1f;
        public bool validateTarget = true;
        public bool requiresTarget = true;
        public bool validateConditions = true;
        public ActionMoveMode moveMode;
        public List<CapabilityCondition> conditions = new();
        public List<CapabilityEffect> effects = new();
    }
}
