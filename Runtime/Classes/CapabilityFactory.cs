//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using UnityEngine;

namespace Keystone.Goap
{
    public abstract class CapabilityFactoryBase
    {
        public abstract ICapabilityConfig Create();
    }

    public abstract class MonoCapabilityFactoryBase : MonoBehaviour
    {
        public abstract ICapabilityConfig Create();
    }

    public abstract class ScriptableCapabilityFactoryBase : ScriptableObject
    {
        public abstract ICapabilityConfig Create();
    }
}
