//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace Keystone.Goap
{
    public class ClassGenerator
    {
        private readonly Dictionary<string, Script> _scripts = new();

        private void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private bool EnsureVariables(string basePath, string name, string namespaceName)
        {
            if (basePath == string.Empty)
                throw new GoapException("Base path cannot be empty!");

            if (name == string.Empty)
                return false;

            if (namespaceName == string.Empty)
                throw new GoapException("Namespace cannot be empty!");

            return true;
        }

        public Script CreateTargetKey(string basePath, string name, string namespaceName)
        {
            if (!EnsureVariables(basePath, name, namespaceName))
                return null;

            var template = LoadTemplate("target-key");
            var id = GetId(name);

            var result = Replace(template, id, name, namespaceName);
            var path = $"{basePath}/TargetKeys/{name}.cs";

            var created = StoreAtPath(result, path);

            return GetScript(id, path, name, !created);
        }

        public Script CreateWorldKey(string basePath, string name, string namespaceName)
        {
            if (!EnsureVariables(basePath, name, namespaceName))
                return null;

            var template = LoadTemplate("world-key");
            var id = GetId(name);

            var result = Replace(template, id, name, namespaceName);
            var path = $"{basePath}/WorldKeys/{name}.cs";
            var created = StoreAtPath(result, path);

            return GetScript(id, path, name, !created);
        }

        public Script CreateGoal(string basePath, string name, string namespaceName)
        {
            var template = LoadTemplate("goal");
            name = name.Replace("Goal", "");

            if (!EnsureVariables(basePath, name, namespaceName))
                return null;

            var id = GetId(name);

            var result = Replace(template, id, name, namespaceName);
            var path = $"{basePath}/Goals/{name}Goal.cs";
            var created = StoreAtPath(result, path);

            return GetScript(id, path, name + "Goal", !created);
        }

        public Script CreateAction(string basePath, string name, string namespaceName)
        {
            var template = LoadTemplate("action");
            name = name.Replace("Action", "");

            if (!EnsureVariables(basePath, name, namespaceName))
                return null;

            var id = GetId(name);

            var result = Replace(template, id, name, namespaceName);
            var path = $"{basePath}/Actions/{name}Action.cs";
            var created = StoreAtPath(result, path);

            return GetScript(id, path, name + "Action", !created);
        }

        public Script CreateMultiSensor(string basePath, string name, string namespaceName)
        {
            if (!EnsureVariables(basePath, name, namespaceName))
                return null;

            var template = LoadTemplate("multi-sensor");
            name = name.Replace("Sensor", "");

            var id = GetId(name);

            var result = Replace(template, id, name, namespaceName);
            var path = $"{basePath}/Sensors/Multi/{name}Sensor.cs";
            var created = StoreAtPath(result, path);

            return GetScript(id, path, name + "Sensor", !created);
        }

        private string GetId(string name)
        {
            return $"{name}-{Guid.NewGuid().ToString()}";
        }

        private string Replace(string template, string id, string name, string namespaceName)
        {
            template = template.Replace("{{id}}", id);
            template = template.Replace("{{name}}", name);
            template = template.Replace("{{namespace}}", namespaceName);

            return template;
        }

        private bool StoreAtPath(string content, string path)
        {
            EnsureDirectoryExists(Path.GetDirectoryName(path));

            if (File.Exists(path))
                return false;

            File.WriteAllText(path, content);
            return true;
        }

        private string LoadTemplate(string name)
        {
            var path = GetTemplatePath(name);

            return File.ReadAllText(path);
        }

        private string GetTemplatePath(string name)
        {
            var basePath = "Packages/com.ZeroFramework.goap/Runtime/ZeroFramework.Goap.Runtime/Generators";
            return basePath + "/Templates/" + name + ".template";
        }

        private Script GetScript(string id, string path, string name, bool existing)
        {
            if (existing && _scripts.TryGetValue(path, out var s))
                return s;

            var script = new Script
            {
                Id = id,
                Path = path,
                Name = name,
            };

            _scripts.Add(path, script);
            return script;
        }
    }
}
