//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public class GoapConfig : IGoapConfig
    {
        public IConditionObserver ConditionObserver { get; set; }
        public IKeyResolver KeyResolver { get; set; }
        public IGoapInjector GoapInjector { get; set; }

        public static GoapConfig Default => new()
        {
            ConditionObserver = new ConditionObserver(),
            KeyResolver = new KeyResolver(),
            GoapInjector = new DefaultGoapInjector(),
        };
    }
}
