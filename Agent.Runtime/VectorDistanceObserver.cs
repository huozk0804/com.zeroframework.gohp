//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using UnityEngine;

namespace ZeroFramework.Goap.Agent
{
    public class VectorDistanceObserver : IAgentDistanceObserver
    {
        public float GetDistance(IMonoAgent agent, ITarget target, IComponentReference reference)
        {
            if (agent.transform == null)
                return 0f;

            if (target == null)
                return 0f;

            if (!target.IsValid())
                return 0f;

            return Vector3.Distance(agent.transform.position, target.Position);
        }
    }
}
