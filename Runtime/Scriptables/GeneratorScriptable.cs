//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.IO;
using UnityEditor;
using UnityEngine;

namespace ZeroFramework.Goap
{
    [CreateAssetMenu(menuName = "Goap/Generator")]
    public class GeneratorScriptable : ScriptableObject
    {
        private readonly ClassGenerator _generator = new();

        public string nameSpace = "ZeroFramework.Goap.GenTest";

        [SerializeField] public Scripts scripts = new();

#if UNITY_EDITOR
        public Script CreateGoal(string name)
        {
            var assetPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));

            return _generator.CreateGoal(assetPath, name, nameSpace);
        }

        public Script CreateAction(string name)
        {
            var assetPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));

            return _generator.CreateAction(assetPath, name, nameSpace);
        }

        public Script CreateTargetKey(string name)
        {
            var assetPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));

            return _generator.CreateTargetKey(assetPath, name, nameSpace);
        }

        public Script CreateWorldKey(string name)
        {
            var assetPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));

            return _generator.CreateWorldKey(assetPath, name, nameSpace);
        }

        public Script CreateMultiSensor(string name)
        {
            var assetPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));

            return _generator.CreateMultiSensor(assetPath, name, nameSpace);
        }
#endif

        public Scripts GetClasses()
        {
#if UNITY_EDITOR

            scripts = ClassScanner.GetClasses(nameSpace, Path.GetDirectoryName(AssetDatabase.GetAssetPath(this)));
            EditorUtility.SetDirty(this);
            // UnityEditor.AssetDatabase.SaveAssets();
#endif
            return scripts;
        }

        public Script[] GetActions() => GetClasses().actions;
        public Script[] GetGoals() => GetClasses().goals;
        public Script[] GetTargetKeys() => GetClasses().targetKeys;
        public Script[] GetWorldKeys() => GetClasses().worldKeys;
        public Script[] GetTargetSensors() => GetClasses().targetSensors;
        public Script[] GetWorldSensors() => GetClasses().worldSensors;
        public Script[] GetMultiSensors() => GetClasses().multiSensors;
    }
}