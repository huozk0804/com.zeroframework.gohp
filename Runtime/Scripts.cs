//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace Keystone.Goap
{
    [Serializable]
    public class Scripts
    {
        public Script[] goals;
        public Script[] actions;
        public Script[] worldSensors;
        public Script[] worldKeys;
        public Script[] targetSensors;
        public Script[] targetKeys;
        public Script[] multiSensors;
    }
}