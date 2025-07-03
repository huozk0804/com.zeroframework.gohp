//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap.Agent
{
    public class ActionContext : IActionContext
    {
        public float DeltaTime { get; set; }
        public bool IsInRange { get; set; }
    }
}