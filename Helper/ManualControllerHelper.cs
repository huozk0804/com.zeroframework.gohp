//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public class ManualControllerHelper : GoapControllerBase
    {
        private IGoap _goap;

        public override void Initialize(IGoap goap)
        {
            _goap = goap;
            _goap.Events.OnAgentResolve += OnAgentResolve;
        }

        private void OnDisable()
        {
            _goap.Events.OnAgentResolve -= OnAgentResolve;
        }
        
        private void OnAgentResolve(IGoapActionProvider actionProvider)
        {
            var runner = GetRunner(actionProvider);

            runner.Run(new[] { actionProvider as IMonoGoapActionProvider });
            runner.Complete();
        }

        private IAgentTypeJobRunner GetRunner(IGoapActionProvider actionProvider)
        {
            return _goap.AgentTypeRunners[actionProvider.AgentType];
        }
    }
}