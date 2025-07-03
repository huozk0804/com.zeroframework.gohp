//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using UnityEngine;

namespace ZeroFramework.Goap
{
    public class ProactiveControllerBehaviour : MonoBehaviour, IGoapController
    {
        private readonly ProactiveController _controller = new();

        [Tooltip("Only updates during Awake")]
        [SerializeField]
        private float resolveTime = 1f;

        public void Awake()
        {
            _controller.ResolveTime = resolveTime;
        }

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
