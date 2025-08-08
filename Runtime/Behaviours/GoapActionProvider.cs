//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using Keystone.Goap.Agent;
using UnityEngine;

namespace Keystone.Goap
{
    public class GoapActionProvider : ActionProviderBase, IMonoGoapActionProvider
    {
        [field: SerializeField]
        public LoggerConfig LoggerConfig { get; set; } = new();

        [field: SerializeField]
        public AgentTypeBehaviour AgentTypeBehaviour { get; set; }

        [field: SerializeField]
        public float DistanceMultiplier { get; set; } = 1f;

        private IAgentType _agentType;
        
        public IAgentType AgentType
        {
            get => _agentType;
            set
            {
                if (_agentType != null)
                    _agentType.Unregister(this);

                _agentType = value;
                WorldData.SetParent(value.WorldData);
                GoalRequest = null;
                CurrentPlan = null;

                value.Register(this);

                Events.Bind(this, value.Events);
            }
        }

        public IGoalResult CurrentPlan { get; private set; } = new GoalResult();
        public IGoalRequest GoalRequest { get; private set; }
        private IGoalRequest _requestCache = new GoalRequest();

        public ILocalWorldData WorldData { get; } = new LocalWorldData();
        public IGoapAgentEvents Events { get; } = new GoapAgentEvents();
        public ILogger<IMonoGoapActionProvider> Logger { get; } = new GoapAgentLogger();

        public Vector3 Position => transform.position;

        private void Awake()
        {
            if (AgentTypeBehaviour != null)
                AgentType = AgentTypeBehaviour.AgentType;

            Logger.Initialize(LoggerConfig, this);
        }

        private void Start()
        {
            if (AgentType == null)
                throw new GoapException($"There is no AgentType assigned to the agent '{name}'! Please assign one in the inspector or through code in the Awake method.");
        }

        private void OnEnable()
        {
            if (AgentType == null)
                return;

            AgentType.Register(this);

            if (GoalRequest != null)
                ResolveAction();
        }

        private void OnDisable()
        {
            if (AgentType == null)
                return;

            AgentType.Unregister(this);
            Events.Unbind();
        }
        
        private IGoalRequest GetRequestCache()
        {
            if (_requestCache == null)
                _requestCache = new GoalRequest();

            _requestCache.Goals.Clear();
            _requestCache.Key = string.Empty;

            return _requestCache;
        }

        /// <summary>
        /// Request goals of the specified types.
        /// </summary>
        /// <param name="resolve"></param>
        /// <param name="goalTypes"></param>
        public void RequestGoal(Type[] goalTypes, bool resolve = true)
        {
            ValidateSetup();

            var request = GetRequestCache();
            request.Goals.Clear();
            foreach (var goalType in goalTypes)
            {
                request.Goals.Add(AgentType.ResolveGoal(goalType));
            }

            RequestGoal(request, resolve);
        }

        /// <summary>
        /// Request goals of the specified type.
        /// </summary>
        /// <param name="resolve"></param>
        /// <param name="goalType"></param>
        public void RequestGoal(Type goalType, bool resolve = true)
        {
            ValidateSetup();

            var request = GetRequestCache();
            request.Goals.Clear();
            request.Goals.Add(AgentType.ResolveGoal(goalType));

            RequestGoal(request, resolve);
        }

        /// <summary>
        ///     Requests a goal of type TGoal.
        /// </summary>
        /// <typeparam name="TGoal">The type of the goal.</typeparam>
        /// <param name="resolve">Whether to resolve the action after requesting the goal. Defaults to true.</param>
        public void RequestGoal<TGoal>(bool resolve = true)
            where TGoal : IGoal
        {
            RequestGoal(typeof(TGoal), resolve);
        }

        /// <summary>
        ///     Requests two goals of types TGoal1 and TGoal2.
        /// </summary>
        /// <typeparam name="TGoal1">The type of the first goal.</typeparam>
        /// <typeparam name="TGoal2">The type of the second goal.</typeparam>
        /// <param name="resolve">Whether to resolve the action after requesting the goals. Defaults to true.</param>
        public void RequestGoal<TGoal1, TGoal2>(bool resolve = true)
            where TGoal1 : IGoal
            where TGoal2 : IGoal
        {
            RequestGoal(new []
            {
                typeof(TGoal1),
                typeof(TGoal2)
            }, resolve);
        }

        /// <summary>
        ///     Requests three goals of types TGoal1, TGoal2, and TGoal3.
        /// </summary>
        /// <typeparam name="TGoal1">The type of the first goal.</typeparam>
        /// <typeparam name="TGoal2">The type of the second goal.</typeparam>
        /// <typeparam name="TGoal3">The type of the third goal.</typeparam>
        /// <param name="resolve">Whether to resolve the action after requesting the goals. Defaults to true.</param>
        public void RequestGoal<TGoal1, TGoal2, TGoal3>(bool resolve = true)
            where TGoal1 : IGoal
            where TGoal2 : IGoal
            where TGoal3 : IGoal
        {
            RequestGoal(new []
            {
                typeof(TGoal1),
                typeof(TGoal2),
                typeof(TGoal3)
            }, resolve);
        }

        /// <summary>
        ///     Requests four goals of types TGoal1, TGoal2, TGoal3, and TGoal4.
        /// </summary>
        /// <typeparam name="TGoal1">The type of the first goal.</typeparam>
        /// <typeparam name="TGoal2">The type of the second goal.</typeparam>
        /// <typeparam name="TGoal3">The type of the third goal.</typeparam>
        /// <typeparam name="TGoal4">The type of the fourth goal.</typeparam>
        /// <param name="resolve">Whether to resolve the action after requesting the goals. Defaults to true.</param>
        public void RequestGoal<TGoal1, TGoal2, TGoal3, TGoal4>(bool resolve = true)
            where TGoal1 : IGoal
            where TGoal2 : IGoal
            where TGoal3 : IGoal
            where TGoal4 : IGoal
        {
            RequestGoal(new []
            {
                typeof(TGoal1),
                typeof(TGoal2),
                typeof(TGoal3),
                typeof(TGoal4)
            }, resolve);
        }

        /// <summary>
        ///     Requests five goals of types TGoal1, TGoal2, TGoal3, TGoal4, and TGoal5.
        /// </summary>
        /// <typeparam name="TGoal1">The type of the first goal.</typeparam>
        /// <typeparam name="TGoal2">The type of the second goal.</typeparam>
        /// <typeparam name="TGoal3">The type of the third goal.</typeparam>
        /// <typeparam name="TGoal4">The type of the fourth goal.</typeparam>
        /// <typeparam name="TGoal5">The type of the fifth goal.</typeparam>
        /// <param name="resolve">Whether to resolve the action after requesting the goals. Defaults to true.</param>
        public void RequestGoal<TGoal1, TGoal2, TGoal3, TGoal4, TGoal5>(bool resolve = true)
            where TGoal1 : IGoal
            where TGoal2 : IGoal
            where TGoal3 : IGoal
            where TGoal4 : IGoal
            where TGoal5 : IGoal
        {
            RequestGoal(new []
            {
                typeof(TGoal1),
                typeof(TGoal2),
                typeof(TGoal3),
                typeof(TGoal4),
                typeof(TGoal5)
            }, resolve);
        }

        /// <summary>
        ///     Requests a specific goal.
        /// </summary>
        /// <param name="goal">The goal to request.</param>
        /// <param name="resolve">Whether to resolve the action after requesting the goal.</param>
        public void RequestGoal(IGoal goal, bool resolve)
        {
            ValidateSetup();

            var request = GetRequestCache();
            request.Goals.Clear();

            request.Goals.Add(goal);

            RequestGoal(request, resolve);
        }

        /// <summary>
        ///     Requests a goal based on the provided goal request. This will allow you to request multiple goals at once.
        /// </summary>
        /// <param name="request">The goal request.</param>
        /// <param name="resolve">Whether to resolve the action after requesting the goal.</param>
        public void RequestGoal(IGoalRequest request, bool resolve = true)
        {
            ValidateSetup();

            if (request == null)
                return;

            if (AreEqual(GoalRequest?.Goals ?? new List<IGoal>(), request.Goals))
                return;

            _requestCache = GoalRequest;
            GoalRequest = request;

            if (Receiver == null)
                return;

            if (resolve)
                ResolveAction();
        }

        public static bool AreEqual<T>(List<T> array1, List<T> array2)
            where T : class
        {
            if (array1.Count != array2.Count)
                return false;

            for (var i = 0; i < array1.Count; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        ///     Sets the action based on the provided goal result. This method is called by the resolver.
        /// </summary>
        /// <param name="result">The goal result.</param>
        public void SetAction(IGoalResult result)
        {
            if (result == null)
                return;

            if (Logger.ShouldLog())
                Logger.Log($"Setting action '{result.Action.GetType().GetGenericTypeName()}' for goal '{result.Goal.GetType().GetGenericTypeName()}'.");

            var currentGoal = CurrentPlan?.Goal;

            CurrentPlan = result;

            if (Receiver == null)
                return;

            Receiver.Timers.Goal.Touch();

            if (currentGoal != result.Goal)
                Events.GoalStart(result.Goal);

            Receiver.SetAction(this, result.Action, WorldData.GetTarget(result.Action));
        }

        [Obsolete("Use agent.StopAction() instead")]
        public void StopAction(bool resolveAction = true)
        {
            Receiver?.StopAction(resolveAction);
        }

        private IActionReceiver _receiver;

        public override IActionReceiver Receiver
        {
            get => _receiver;
            set
            {
                _receiver = value;
                Events.Bind(value);
            }
        }

        /// <summary>
        ///     Will try and resolve for a new action based on the current goal request.
        /// </summary>
        public override void ResolveAction()
        {
            ValidateSetup();

            Events.Resolve();
            Receiver.Timers.Resolve.Touch();
        }

        /// <summary>
        ///     Clears the current goal.
        /// </summary>
        public void ClearGoal()
        {
            CurrentPlan = null;
        }

        /// <summary>
        ///     Sets the distance multiplier for the agent. This is used by the resolver to calculate the cost of distance between
        ///     actions.
        /// </summary>
        /// <param name="multiplier">The distance multiplier.</param>
        public void SetDistanceMultiplier(float multiplier)
        {
            if (multiplier < 0f)
                throw new GoapException("The distance multiplier must be >= 0.");

            DistanceMultiplier = multiplier;
        }

        /// <summary>
        ///     Sets the distance multiplier for the agent based on speed, based on the assumption that speed is in units per
        ///     second and cost is similar to a second.
        ///     This is used by the resolver to calculate the cost of distance between actions.
        /// </summary>
        /// <param name="multiplier">The distance multiplier.</param>
        public void SetDistanceMultiplierSpeed(float speed)
        {
            if (speed <= 0f)
                throw new GoapException("The speed value must be greater than 0. To disable the distance multiplier, use SetDistanceMultiplier(0f).");

            DistanceMultiplier = 1f / speed;
        }

        private void ValidateSetup()
        {
            if (AgentType == null)
                throw new GoapException($"There is no AgentType assigned to the agent '{name}'! Please assign one in the inspector or through code in the Awake method.");

            if (Receiver == null)
                throw new GoapException($"There is no ActionReceiver assigned to the agent '{name}'! You're probably missing the ActionProvider on the AgentBehaviour.");
        }

        /// <summary>
        ///     Gets the actions of the specified type.
        /// </summary>
        /// <typeparam name="TAction">The type of the actions.</typeparam>
        /// <returns>A list of actions of the specified type.</returns>
        public List<TAction> GetActions<TAction>() where TAction : IGoapAction => AgentType.GetActions<TAction>();

        private void OnDestroy()
        {
            Logger.Dispose();
        }
    }
}
