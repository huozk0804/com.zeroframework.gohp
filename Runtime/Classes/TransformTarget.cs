//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using Keystone.Goap.Agent;
using UnityEngine;

namespace Keystone.Goap
{
    public class TransformTarget : ITarget
    {
        public Transform Transform { get; private set; }

        public Vector3 Position
        {
            get
            {
                if (Transform == null)
                    return Vector3.zero;

                return Transform.position;
            }
        }

        public TransformTarget(Transform transform)
        {
            Transform = transform;
        }

        public TransformTarget SetTransform(Transform transform)
        {
            Transform = transform;
            return this;
        }

        public bool IsValid()
        {
            return Transform != null;
        }
    }
}
