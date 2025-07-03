//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

namespace ZeroFramework.Goap.Agent
{
    public class DataReferenceInjector : IDataReferenceInjector
    {
        private readonly IMonoAgent _agent;
        private readonly Dictionary<Type, object> _references = new();
        private readonly Dictionary<PropertyInfo, object> _cachedReferences = new();

        private static readonly Dictionary<Type, DataReferenceCache> CachedDataReferenceCaches = new();

        public DataReferenceInjector(IMonoAgent agent)
        {
            _agent = agent;
        }

        public void Inject(IActionData data)
        {
            var reference = GetCachedDataReferenceCache(data.GetType());

            foreach (var (propertyInfo, attribute) in reference.Properties)
            {
                var value = GetCachedPropertyValue(propertyInfo, attribute);

                if (value == null)
                    continue;

                // set the reference
                propertyInfo.SetValue(data, value);
            }
        }

        private DataReferenceCache GetCachedDataReferenceCache(Type type)
        {
            if (!CachedDataReferenceCaches.ContainsKey(type))
                CachedDataReferenceCaches.Add(type, new DataReferenceCache(type));

            return CachedDataReferenceCaches[type];
        }

        private object GetPropertyValue(PropertyInfo property, Attribute attribute)
        {
            if (attribute is GetComponentAttribute)
                return GetCachedComponentReference(property.PropertyType);

            if (attribute is GetComponentInChildrenAttribute)
                return GetCachedComponentInChildrenReference(property.PropertyType);

            if (attribute is GetComponentInParentAttribute)
                return GetCachedComponentInParentReference(property.PropertyType);

            return null;
        }

        private object GetCachedPropertyValue(PropertyInfo property, Attribute attribute)
        {
            if (!_cachedReferences.ContainsKey(property))
            {
                _cachedReferences.Add(property, GetPropertyValue(property, attribute));
            }

            return _cachedReferences[property];
        }

        private object GetCachedComponentReference(Type type)
        {
            // check if we have a reference for this type
            if (!_references.ContainsKey(type))
                _references.Add(type, _agent.GetComponent(type));

            // get the reference
            return _references[type];
        }

        public T GetCachedComponent<T>()
        {
            return (T)GetCachedComponentReference(typeof(T));
        }

        private object GetCachedComponentInChildrenReference(Type type)
        {
            // check if we have a reference for this type
            if (!_references.ContainsKey(type))
                _references.Add(type, _agent.GetComponentInChildren(type));

            // get the reference
            return _references[type];
        }

        public T GetCachedComponentInChildren<T>()
        {
            return (T)GetCachedComponentInChildrenReference(typeof(T));
        }

        private object GetCachedComponentInParentReference(Type type)
        {
            // check if we have a reference for this type
            if (!_references.ContainsKey(type))
                _references.Add(type, _agent.GetComponentInParent(type));

            // get the reference
            return _references[type];
        }

        public T GetCachedComponentInParent<T>()
        {
            return (T)GetCachedComponentInParentReference(typeof(T));
        }

        private class DataReferenceCache
        {
            public Dictionary<PropertyInfo, Attribute> Properties { get; private set; }

            public DataReferenceCache(Type type)
            {
                Properties = new Dictionary<PropertyInfo, Attribute>();

                // find all properties with the GetComponent attribute
                var props = type.GetProperties();

                foreach (var prop in props)
                {
                    foreach (var attribute in prop.GetCustomAttributes(true))
                    {
                        if (attribute is not (GetComponentAttribute or GetComponentInChildrenAttribute
                            or GetComponentInParentAttribute))
                            continue;

                        Properties.Add(prop, (Attribute)attribute);
                        break;
                    }
                }
            }
        }
    }
}