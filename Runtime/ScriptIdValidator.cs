//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZeroFramework.Goap
{
    public class ScriptReferenceValidator
    {
        public IClassReferenceIssue[] CheckAll(CapabilityConfigScriptable capabilityConfig)
        {
            var generator = capabilityConfig.GetGenerator();
            var classes = generator.GetClasses();

            var issues = new List<IClassReferenceIssue>();

            capabilityConfig.goals.ForEach(goal =>
            {
                issues.Add(CheckReference(goal.goal, classes.goals, ClassRefType.Goal));

                goal.conditions.ForEach(condition =>
                {
                    issues.Add(CheckReference(condition.worldKey, classes.worldKeys, ClassRefType.WorldKey));
                });
            });

            capabilityConfig.actions.ForEach(action =>
            {
                issues.Add(CheckReference(action.action, classes.actions, ClassRefType.Action));
                issues.Add(CheckReference(action.target, classes.targetKeys, ClassRefType.TargetKey));

                action.conditions.ForEach(condition =>
                {
                    issues.Add(CheckReference(condition.worldKey, classes.worldKeys, ClassRefType.WorldKey));
                });

                action.effects.ForEach(effect =>
                {
                    issues.Add(CheckReference(effect.worldKey, classes.worldKeys, ClassRefType.WorldKey));
                });
            });

            capabilityConfig.worldSensors.ForEach(sensor =>
            {
                issues.Add(CheckReference(sensor.sensor, classes.worldSensors, ClassRefType.WorldSensor));
                issues.Add(CheckReference(sensor.worldKey, classes.worldKeys, ClassRefType.WorldKey));
            });

            capabilityConfig.targetSensors.ForEach(sensor =>
            {
                issues.Add(CheckReference(sensor.sensor, classes.targetSensors, ClassRefType.TargetSensor));
                issues.Add(CheckReference(sensor.targetKey, classes.targetKeys, ClassRefType.TargetKey));
            });

            capabilityConfig.multiSensors.ForEach(sensor =>
            {
                issues.Add(CheckReference(sensor.sensor, classes.multiSensors, ClassRefType.MultiSensor));
            });

            return issues.Where(x => x != null).ToArray();
        }

        private IClassReferenceIssue CheckReference(ClassRef reference, Script[] scripts, ClassRefType type)
        {
            var (status, match) = reference.GetMatch(scripts);

            switch (status)
            {
                case ClassRefStatus.Empty:
                    return new EmptyClassReferenceIssue(reference, type);
                case ClassRefStatus.None:
                    return new MissingClassReferenceIssue(reference, type);
                case ClassRefStatus.Name:
                    return new NameClassReferenceIssue(reference, match);
                case ClassRefStatus.Id:
                    return new IdClassReferenceIssue(reference, match);
            }

            return null;
        }
    }

    public interface IClassReferenceIssue
    {
        void Fix(GeneratorScriptable generator);
        string GetMessage();
    }

    public class EmptyClassReferenceIssue : IClassReferenceIssue
    {
        private readonly ClassRef _reference;
        private readonly ClassRefType _type;

        public EmptyClassReferenceIssue(ClassRef reference, ClassRefType type)
        {
            _reference = reference;
            _type = type;
        }

        public void Fix(GeneratorScriptable generator)
        {
            Debug.Log($"Unable to fix reference without name and id: {_type}");
        }

        public string GetMessage()
        {
            return $"Reference without name and id: {_type}";
        }
    }

    public class MissingClassReferenceIssue : IClassReferenceIssue
    {
        private readonly ClassRef _reference;
        private readonly ClassRefType _type;

        public MissingClassReferenceIssue(ClassRef reference, ClassRefType type)
        {
            _reference = reference;
            _type = type;
        }

        public void Fix(GeneratorScriptable generator)
        {
            var result = Generate(generator);

            if (result == null)
                return;

            _reference.Id = result.Id;
            _reference.Name = result.Name;

            Debug.Log($"Generated {result.Path}");
        }

        private Script Generate(GeneratorScriptable generator)
        {
#if UNITY_EDITOR
            switch (_type)
            {
                case ClassRefType.Action:
                    return generator.CreateAction(_reference.Name);
                case ClassRefType.Goal:
                    return generator.CreateGoal(_reference.Name);
                case ClassRefType.TargetKey:
                    return generator.CreateTargetKey(_reference.Name);
                case ClassRefType.WorldKey:
                    return generator.CreateWorldKey(_reference.Name);
                case ClassRefType.TargetSensor:
                    Debug.Log("TargetSensors can't be generated. Please create them manually.");
                    break;
                case ClassRefType.WorldSensor:
                    Debug.Log("WorldSensors can't be generated. Please create them manually.");
                    break;
                case ClassRefType.MultiSensor:
                    return generator.CreateMultiSensor(_reference.Name);
            }
#endif
            return null;
        }

        public string GetMessage()
        {
            return $"Class does not exist: {_reference.Name} ({_type})";
        }
    }

    public class NameClassReferenceIssue : IClassReferenceIssue
    {
        private readonly ClassRef _reference;
        private readonly Script _script;

        public NameClassReferenceIssue(ClassRef reference, Script script)
        {
            _reference = reference;
            _script = script;
        }

        public void Fix(GeneratorScriptable generator)
        {
            _reference.Id = _script.Id;
            Debug.Log($"Fixed {GetMessage()}");
        }

        public string GetMessage()
        {
            return $"Reference matched by name, but not by id: {_reference.Name} ({_script.Type.Name})";
        }
    }

    public class IdClassReferenceIssue : IClassReferenceIssue
    {
        private readonly ClassRef _reference;
        private readonly Script _script;

        public IdClassReferenceIssue(ClassRef reference, Script script)
        {
            _reference = reference;
            _script = script;
        }

        public void Fix(GeneratorScriptable generator)
        {
            _reference.Name = _script.Type.Name;
            Debug.Log($"Fixed {GetMessage()}");
        }

        public string GetMessage()
        {
            return $"Reference matched by id, but not by name: {_reference.Id} ({_script.Type.Name})";
        }
    }

    public enum ClassRefType
    {
        Goal,
        Action,
        TargetKey,
        WorldKey,
        TargetSensor,
        WorldSensor,
        MultiSensor,
    }
}