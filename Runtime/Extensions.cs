//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Keystone.Goap.Agent;
using UnityEngine;

namespace Keystone.Goap
{
    public static class Extensions
    {
        public static bool IsNull(this object obj)
        {
            if (obj is not MonoBehaviour mono)
                return obj == null;

            return mono == null;
        }

        public static string ToName(this Comparison comparison)
        {
            return comparison switch
            {
                Comparison.SmallerThan => "<",
                Comparison.SmallerThanOrEqual => "<=",
                Comparison.GreaterThan => ">",
                Comparison.GreaterThanOrEqual => ">=",
                _ => throw new NotImplementedException(),
            };
        }

        public static Comparison FromName(this string comparison)
        {
            return comparison switch
            {
                "<" => Comparison.SmallerThan,
                "<=" => Comparison.SmallerThanOrEqual,
                ">" => Comparison.GreaterThan,
                ">=" => Comparison.GreaterThanOrEqual,
                _ => throw new NotImplementedException(),
            };
        }

        public static string ToName(this EffectType type)
        {
            return type switch
            {
                EffectType.Increase => "++",
                EffectType.Decrease => "--",
                _ => throw new NotImplementedException(),
            };
        }

        public static ClassRefStatus GetStatus(this IClassRef classRef, Script[] scripts)
        {
            var (status, match) = classRef.GetMatch(scripts);

            return status;
        }

        public static Script GetScript(this IClassRef classRef, Script[] scripts)
        {
            var (status, match) = classRef.GetMatch(scripts);

            return match;
        }

        public static (ClassRefStatus status, Script script) GetMatch(this IClassRef classRef, Script[] scripts)
        {
            if (string.IsNullOrEmpty(classRef.Name) && string.IsNullOrEmpty(classRef.Id))
            {
                return (ClassRefStatus.Empty, null);
            }

            // Full match
            if (scripts.Any(x => x.Id == classRef.Id && x.Type.Name == classRef.Name))
            {
                return (ClassRefStatus.Full, scripts.First(x => x.Id == classRef.Id && x.Type.Name == classRef.Name));
            }

            // Id Match
            if (scripts.Any(x => x.Id == classRef.Id))
            {
                return (ClassRefStatus.Id, scripts.First(x => x.Id == classRef.Id));
            }

            // Name Match
            if (scripts.Any(x => x.Type.Name == classRef.Name))
            {
                return (ClassRefStatus.Name, scripts.First(x => x.Type.Name == classRef.Name));
            }

            return (ClassRefStatus.None, null);
        }

        public static T GetInstance<T>(this Script script) where T : class
        {
            if (script?.Type == null)
                return null;

            var instance = Activator.CreateInstance(script.Type);

            if (instance is TargetKeyBase targetKey)
            {
                targetKey.Name = script.Type.Name;
            }

            if (instance is WorldKeyBase worldKey)
            {
                worldKey.Name = script.Type.Name;
            }

            return instance as T;
        }

        public static string GetFullName([CanBeNull] this Script script)
        {
            return script?.Type.AssemblyQualifiedName ?? "UNDEFINED";
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static IWorldKey[] GetWorldKeys(this IAgentTypeConfig agentTypeConfig)
        {
            return agentTypeConfig.Actions
                .SelectMany((action) =>
                {
                    return action.Conditions
                        .Where(x => x.WorldKey != null)
                        .Select(y => y.WorldKey);
                })
                .Distinct()
                .ToArray();
        }

        public static ITargetKey[] GetTargetKeys(this IAgentTypeConfig agentTypeConfig)
        {
            return agentTypeConfig.Actions
                .Where(x => x.Target != null)
                .Select(x => x.Target)
                .Distinct()
                .ToArray();
        }

        public static Type GetPropertiesType(this Type type)
        {
            var baseType = type;

            while (baseType != null)
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(GoapActionBase<,>))
                    return baseType.GetGenericArguments()[1];

                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(GoapActionBase<>))
                    return typeof(EmptyActionProperties);

                baseType = baseType.BaseType;
            }

            return null;
        }

        public static (INode[] RootNodes, INode[] ChildNodes) ToNodes(this IEnumerable<IConnectable> actions)
        {
            var mappedNodes = actions.Select(ToNode).ToArray();

            return (
                mappedNodes.Where(x => x.IsRootNode).ToArray(),
                mappedNodes.Where(x => !x.IsRootNode).ToArray()
            );
        }

        private static INode ToNode(IConnectable action)
        {
            return new Node
            {
                Action = action,
                Conditions = action.Conditions?.Select(y => new NodeCondition
                {
                    Condition = y,
                }).Cast<INodeCondition>().ToList() ?? new List<INodeCondition>(),
                Effects = action.Effects?.Select(y => new NodeEffect
                {
                    Effect = y,
                }).Cast<INodeEffect>().ToList() ?? new List<INodeEffect>(),
            };
        }

        public static string GetGenericTypeName(this Type type)
        {
            var typeName = type.Name;

            if (type.IsGenericType)
            {
                var genericArguments = type.GetGenericArguments();
                var genericTypeName = typeName.Substring(0, typeName.IndexOf('`'));
                var typeArgumentNames = string.Join(",", genericArguments.Select(a => a.GetGenericTypeName()));
                typeName = $"{genericTypeName}<{typeArgumentNames}>";
            }

            return typeName;
        }

        /// <summary>
        /// 获取有效的目标位置，如果目标无效则返回null
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <returns>有效的位置或null</returns>
        public static Vector3? GetValidPosition(this ITarget target)
        {
            // 如果目标为null，返回null
            if (target == null)
                return null;

            // 如果目标无效，返回null
            if (!target.IsValid())
                return null;

            // 返回目标的位置
            return target.Position;
        }
    }
}