//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    public interface IAgentTypeJobRunner
    {
        void Run(IMonoGoapActionProvider[] queue);
        void Complete();
        void Dispose();
        IGraph GetGraph();
    }
}