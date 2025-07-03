//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using UnityEngine;

namespace ZeroFramework.Goap
{
    public abstract class GoapConfigInitializerBase : MonoBehaviour
    {
        public abstract void InitConfig(IGoapConfig config);
    }
}