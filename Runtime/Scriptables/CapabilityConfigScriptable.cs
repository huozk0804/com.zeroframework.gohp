//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Keystone.Goap
{
    [CreateAssetMenu(menuName = "Goap/CapabilityConfig")]
    public class CapabilityConfigScriptable : ScriptableCapabilityFactoryBase
    {
        public List<CapabilityGoal> goals = new();
        public List<CapabilityAction> actions = new();
        public List<CapabilityWorldSensor> worldSensors = new();
        public List<CapabilityTargetSensor> targetSensors = new();
        public List<CapabilityMultiSensor> multiSensors = new();

        [SerializeField]
        public GeneratorScriptable generatorScriptable;

        public override ICapabilityConfig Create()
        {
            var generator = GetGenerator();

            return new CapabilityConfig(name)
            {
                Goals = GetGoals(generator),
                Actions = GetActions(generator),
                WorldSensors = GetWorldSensors(generator),
                TargetSensors = GetTargetSensors(generator),
                MultiSensors = GetMultiSensors(generator),
            };
        }

        public List<IActionConfig> GetActions(GeneratorScriptable generator)
        {
            if (generator == null)
                return new List<IActionConfig>();

            var actionClasses = generator.GetActions();
            var targetClasses = generator.GetTargetKeys();

            return actions.Select(x => new ActionConfig
            {
                Name = x.action.Name,
                ClassType = x.action.GetScript(actionClasses).GetFullName(),
                BaseCost = x.baseCost,
                Target = x.target.GetScript(targetClasses).GetInstance<ITargetKey>(),
                StoppingDistance = x.stoppingDistance,
                ValidateTarget = x.validateTarget,
                RequiresTarget = x.requiresTarget,
                ValidateConditions = x.validateConditions,
                Conditions = x.conditions.Select(y => new Condition
                {
                    WorldKey = y.worldKey.GetScript(generator.GetWorldKeys()).GetInstance<IWorldKey>(),
                    Comparison = y.comparison,
                    Amount = y.amount,
                }).Cast<ICondition>().ToArray(),
                Effects = x.effects.Select(y => new Effect
                {
                    WorldKey = y.worldKey.GetScript(generator.GetWorldKeys()).GetInstance<IWorldKey>(),
                    Increase = y.effect == EffectType.Increase,
                }).Cast<IEffect>().ToArray(),
                MoveMode = x.moveMode,
                Properties = x.properties,
            }).Cast<IActionConfig>().ToList();
        }

        public List<IGoalConfig> GetGoals(GeneratorScriptable generator)
        {
            if (generator == null)
                return new List<IGoalConfig>();

            var goalClasses = generator.GetGoals();

            return goals.Select(x => new GoalConfig
            {
                Name = x.goal.Name,
                ClassType = x.goal.GetScript(goalClasses).GetFullName(),
                Conditions = x.conditions.Select(y => new Condition
                {
                    WorldKey = y.worldKey.GetScript(generator.GetWorldKeys()).GetInstance<IWorldKey>(),
                    Comparison = y.comparison,
                    Amount = y.amount,
                }).Cast<ICondition>().ToList(),
            }).Cast<IGoalConfig>().ToList();
        }

        public List<IWorldSensorConfig> GetWorldSensors(GeneratorScriptable generator)
        {
            if (generator == null)
                return new List<IWorldSensorConfig>();

            var sensorClasses = generator.GetWorldSensors();

            return worldSensors.Select(x => new WorldSensorConfig
            {
                Name = x.sensor.Name,
                ClassType = x.sensor.GetScript(sensorClasses).GetFullName(),
                Key = x.worldKey.GetScript(generator.GetWorldKeys()).GetInstance<IWorldKey>(),
            }).Cast<IWorldSensorConfig>().ToList();
        }

        public List<ITargetSensorConfig> GetTargetSensors(GeneratorScriptable generator)
        {
            if (generator == null)
                return new List<ITargetSensorConfig>();

            var sensorClasses = generator.GetTargetSensors();

            return targetSensors.Select(x => new TargetSensorConfig
            {
                Name = x.sensor.Name,
                ClassType = x.sensor.GetScript(sensorClasses).GetFullName(),
                Key = x.targetKey.GetScript(generator.GetTargetKeys()).GetInstance<ITargetKey>(),
            }).Cast<ITargetSensorConfig>().ToList();
        }

        public List<IMultiSensorConfig> GetMultiSensors(GeneratorScriptable generator)
        {
            if (generator == null)
                return new List<IMultiSensorConfig>();

            var sensorClasses = generator.GetMultiSensors();

            return multiSensors.Select(x => new MultiSensorConfig
            {
                Name = x.sensor.Name,
                ClassType = x.sensor.GetScript(sensorClasses).GetFullName(),
            }).Cast<IMultiSensorConfig>().ToList();
        }

        public GeneratorScriptable GetGenerator()
        {
#if UNITY_EDITOR
            generatorScriptable = ClassScanner.GetGenerator(this);
            EditorUtility.SetDirty(this);
#endif
            return generatorScriptable;
        }
    }
}
