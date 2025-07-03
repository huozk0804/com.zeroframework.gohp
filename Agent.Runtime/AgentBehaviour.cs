//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using UnityEngine;

namespace ZeroFramework.Goap.Agent
{
    public class AgentBehaviour : MonoBehaviour, IMonoAgent
    {
        [field: SerializeField] public ActionProviderBase ActionProviderBase { get; set; }

        [SerializeField] private bool isPaused = false;

        public bool IsPaused
        {
            get => isPaused;
            set
            {
                if (value == isPaused)
                    return;

                isPaused = value;

                if (value)
                {
                    Events.Pause();
                    return;
                }

                Events.Resume();
            }
        }

        private IActionProvider _actionProvider;

        public IActionProvider ActionProvider
        {
            get => _actionProvider;
            set
            {
                if (_actionProvider == value)
                    return;

                _actionProvider = value;
                _actionProvider.Receiver = this;
            }
        }

        [field: SerializeField] public LoggerConfig LoggerConfig { get; set; } = new();

        public AgentState State { get; private set; } = AgentState.NoAction;
        public AgentMoveState MoveState { get; private set; } = AgentMoveState.Idle;

        public IActionState ActionState { get; } = new ActionState();

        public IAgentEvents Events { get; } = new AgentEvents();
        public IDataReferenceInjector Injector { get; private set; }
        public IAgentDistanceObserver DistanceObserver { get; set; } = new VectorDistanceObserver();
        public ILogger<IMonoAgent> Logger { get; set; } = new AgentLogger();

        public IAgentTimers Timers { get; } = new AgentTimers();

        public ITarget CurrentTarget { get; private set; }

        public Transform Transform => transform;

        private ActionRunner _actionRunner;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            if (ActionProvider == null)
                throw new AgentException(
                    $"There is no ActionProvider assigned to the agent '{name}'! Please assign one in the inspector or through code in the Awake method.");
        }

        private void OnEnable()
        {
            if (ActionState.PreviousAction != null)
                ActionProvider?.ResolveAction();
        }

        private void OnDisable()
        {
            StopAction(false);
        }

        public void Initialize()
        {
            Injector = new DataReferenceInjector(this);
            _actionRunner = new ActionRunner(this,
                new AgentProxy(SetState, SetMoveState, (state) => ActionState.RunState = state,
                    IsInRange));
            Logger.Initialize(LoggerConfig, this);

            if (ActionProviderBase != null)
                ActionProvider = ActionProviderBase;
        }

        private void Update()
        {
            Run();
        }

        /// <summary>
        ///     Runs the current action. This is called in the Update method.
        /// </summary>
        public void Run()
        {
            if (IsPaused)
                return;

            if (ActionState.Action == null)
                return;

            UpdateTarget();

            _actionRunner.Run();
        }

        private void UpdateTarget()
        {
            if (CurrentTarget == ActionState.Data?.Target)
                return;

            CurrentTarget = ActionState.Data?.Target;

            if (CurrentTarget == null)
            {
                Events.TargetLost();
                return;
            }

            Events.TargetChanged(CurrentTarget, IsInRange());
        }

        private void SetState(AgentState state)
        {
            if (State == state)
                return;

            State = state;

            if (state is AgentState.PerformingAction or AgentState.MovingWhilePerformingAction)
            {
                Timers.Action.Touch();
            }
        }

        private void SetMoveState(AgentMoveState state)
        {
            if (MoveState == state)
                return;

            MoveState = state;

            switch (state)
            {
                case AgentMoveState.InRange:
                    Events.TargetInRange(CurrentTarget);
                    break;
                case AgentMoveState.NotInRange:
                    Events.TargetNotInRange(CurrentTarget);
                    break;
            }
        }

        private bool IsInRange()
        {
            var distance = DistanceObserver.GetDistance(this, ActionState.Data?.Target, Injector);

            return ActionState.Action.IsInRange(this, distance, ActionState.Data, Injector);
        }

        /// <summary>
        ///     Sets the action for the agent. This is mostly used by the action provider.
        /// </summary>
        /// <param name="actionProvider">The action provider.</param>
        /// <param name="action">The action to set.</param>
        /// <param name="target">The target of the action.</param>
        public void SetAction(IActionProvider actionProvider, IAction action, ITarget target)
        {
            ActionProvider = actionProvider;

            if (ActionState.Action != null)
            {
                StopAction(false);
            }

            var data = action.GetData();
            Injector.Inject(data);
            data.Target = target;

            ActionState.SetAction(action, data);
            Timers.Action.Touch();

            SetState(AgentState.StartingAction);

            action.Start(this, data);

            Events.ActionStart(action);
        }

        /// <summary>
        ///     Stops the current action.
        /// </summary>
        /// <param name="resolveAction">Whether to resolve for a new action after stopping.</param>
        public void StopAction(bool resolveAction = true)
        {
            var action = ActionState.Action;

            action?.Stop(this, ActionState.Data);
            ResetAction();

            Events.ActionStop(action);

            if (resolveAction)
                ResolveAction();
        }

        /// <summary>
        ///     Completes the current action.
        /// </summary>
        /// <param name="resolveAction">Whether to resolve for a new action after completing.</param>
        public void CompleteAction(bool resolveAction = true)
        {
            var action = ActionState.Action;

            action?.Complete(this, ActionState.Data);
            ResetAction();

            Events.ActionComplete(action);

            if (resolveAction)
                ResolveAction();
        }

        /// <summary>
        ///     Will trigger try and resolve a new action from the action provider.
        /// </summary>
        public void ResolveAction()
        {
            if (ActionProvider == null)
                throw new AgentException("No action provider found!");

            ActionProvider.ResolveAction();
        }

        private void ResetAction()
        {
            ActionState.Reset();
            SetState(AgentState.NoAction);
            SetMoveState(AgentMoveState.Idle);
            UpdateTarget();
        }

        private void OnDestroy()
        {
            Logger.Dispose();
        }
    }
}