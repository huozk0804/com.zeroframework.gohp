//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using UnityEngine;

namespace ZeroFramework.Goap
{
    public class ReactiveControllerBehaviour : MonoBehaviour, IGoapController
    {
        private readonly ReactiveController _controller = new();

        public void Initialize(IGoap goap)
        {
            _controller.Initialize(goap);
        }

        private void OnDisable()
        {
            _controller.Disable();
        }

        public void OnUpdate()
        {
            _controller.OnUpdate();
        }

        public void OnLateUpdate()
        {
            _controller.OnLateUpdate();
        }
    }
}
