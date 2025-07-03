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
    public class Script
    {
        [field: SerializeField] public string Name { get; set; }

        [SerializeField] private string fullName;

        private Type _type;

        public Type Type
        {
            get
            {
                if (_type == null) _type = Type.GetType(fullName);
                return _type;
            }
            set
            {
                _type = value;
                fullName = value.AssemblyQualifiedName;
            }
        }

        [field: SerializeField] public string Path { get; set; }

        [field: SerializeField] public string Id { get; set; }
    }
}