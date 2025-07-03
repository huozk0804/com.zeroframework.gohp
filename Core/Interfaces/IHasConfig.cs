//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public interface IHasConfig<TConfig> where TConfig : IConfig
    {
        public TConfig Config { get; }
        public void SetConfig(TConfig config);
    }
}