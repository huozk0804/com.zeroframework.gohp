//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace Keystone.Goap
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class GoapIdAttribute : Attribute
    {
        public readonly string Id;

        public GoapIdAttribute(string id)
        {
            Id = id;
        }
    }
}
