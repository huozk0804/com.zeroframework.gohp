//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;

namespace ZeroFramework.Goap
{
    public interface IAgentCollection
    {
        HashSet<IMonoGoapActionProvider> All();
        void Add(IMonoGoapActionProvider actionProvider);
        void Remove(IMonoGoapActionProvider actionProvider);
        void Enqueue(IMonoGoapActionProvider actionProvider);
        IMonoGoapActionProvider[] GetQueue();
        int GetQueueCount();
    }
}