//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using UnityEngine;

namespace ZeroFramework.Goap.Agent
{
    public class ForTimeActionDisabler : IActionDisabler
    {
        private readonly float _enableAt;

        public ForTimeActionDisabler(float time)
        {
            this._enableAt = Time.time + time;
        }

        public bool IsDisabled(IAgent agent)
        {
            return Time.time < this._enableAt;
        }
    }
}