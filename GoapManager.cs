//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine.Serialization;

namespace ZeroFramework.Goap
{
    public sealed class GoapManager : GameFrameworkModule, IGoapManager
    {
        private Goap _goap;
        private GoapControllerBase _controllerHelper;
        private bool _isInitialized;

        protected override int Priority => 1;

        public float RunTime => _goap.RunTime;
        public float CompleteTime => _goap.CompleteTime;
        public int RunCount { get; private set; }

        public GoapConfigInitializerBase configInitializer;

        [FormerlySerializedAs("goapSetConfigFactories")]
        public List<AgentTypeFactoryBase> agentTypeConfigFactories = new();

        public GoapManager()
        {
            Initialize();
        }

        protected override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        protected override void Shutdown()
        {
            _goap.Dispose();
        }

        private void Initialize()
        {
            if (_isInitialized)
                return;

            //_controllerHelper = Helper.CreateHelper(GameFrameworkConfig.Instance.goapControllerHelperTypeName,
            //    GameFrameworkConfig.Instance.goapCustomControllerCustomHelper);
            //if (_controllerHelper == null)
            //{
            //    Log.Error("No IGoapController found on GameObject of GoapBehaviour.");
            //    return;
            //}

            _goap = new Goap(_controllerHelper);

            if (configInitializer != null)
                configInitializer.InitConfig(Config);

            _controllerHelper.Initialize(_goap);
            CreateAgentTypes();
            _isInitialized = true;
        }

        private void CreateAgentTypes()
        {
            var agentTypeFactory = new AgentTypeFactory(Config);

            foreach (var factory in agentTypeConfigFactories)
            {
                if (factory == null)
                    continue;

                Register(agentTypeFactory.Create(factory.Create()));
            }
        }

        public IAgentType GetAgentType(string id)
        {
            Initialize();
            return _goap.GetAgentType(id);
        }

        public void Register(IAgentType agentType) => _goap.Register(agentType);

        public void Register(IAgentTypeConfig agentTypeConfig) =>
            _goap.Register(new AgentTypeFactory(Config).Create(agentTypeConfig));

        public IGoapEvents Events => _goap.Events;
        public Dictionary<IAgentType, IAgentTypeJobRunner> AgentTypeRunners => _goap.AgentTypeRunners;
        public IGoapController Controller => _goap.Controller;
        public IGraph GetGraph(IAgentType agentType) => _goap.GetGraph(agentType);
        public bool Knows(IAgentType agentType) => _goap.Knows(agentType);
        public List<IMonoGoapActionProvider> Agents => _goap.Agents;
        public IAgentType[] AgentTypes => _goap.AgentTypes;
        public IGoapConfig Config => _goap.Config;
    }
}