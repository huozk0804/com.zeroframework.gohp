//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using Unity.Mathematics;
using UnityEngine;

namespace Keystone.Goap
{
    public interface IPositionBuilder
    {
        IPositionBuilder SetPosition(IConnectable action, Vector3? position);
        float3[] Build();
        void Clear();
    }
}