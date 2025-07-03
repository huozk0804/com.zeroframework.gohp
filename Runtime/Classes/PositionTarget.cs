//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using ZeroFramework.Goap.Agent;
using UnityEngine;

namespace ZeroFramework.Goap
{
    public class PositionTarget : ITarget
    {
        public Vector3 Position { get; private set; }

        public PositionTarget(Vector3 position)
        {
            Position = position;
        }

        public PositionTarget SetPosition(Vector3 position)
        {
            Position = position;
            return this;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
