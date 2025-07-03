//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using UnityEngine;

namespace ZeroFramework.Goap
{
    [Serializable]
    public class ClassRef : IClassRef
    {
        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        public string Id { get; set; }
    }
}
