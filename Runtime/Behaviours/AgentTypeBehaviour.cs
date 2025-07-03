//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using UnityEngine;

namespace ZeroFramework.Goap
{
    [DefaultExecutionOrder(-99)]
    public class AgentTypeBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected AgentTypeScriptable config;

        public IAgentType AgentType { get; private set; }
        public AgentTypeScriptable Config => config;

        protected virtual void Awake()
        {
            var newConfig = config.Create();
            //var agentType = new AgentTypeFactory(Zero.goap.Config).Create(newConfig);
            //Zero.goap.Register(agentType);
            //AgentType = agentType;
        }
    }
}
