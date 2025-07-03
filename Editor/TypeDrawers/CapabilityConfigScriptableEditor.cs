using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using ZeroFramework.Goap;

namespace ZeroFramework.Editor.Package
{
    [CustomEditor(typeof(CapabilityConfigScriptable))]
    public class CapabilityConfigScriptableEditor : UnityEditor.Editor
    {
        private CapabilityConfigScriptable scriptable;
        private GoalList goalList;
        private ActionList actionList;
        private SensorList<CapabilityWorldSensor> worldSensorList;
        private SensorList<CapabilityTargetSensor> targetSensorList;
        private SensorList<CapabilityMultiSensor> multiSensorList;

        public override VisualElement CreateInspectorGUI()
        {
            scriptable = (CapabilityConfigScriptable)target;
            
            var root = new VisualElement();
            
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>($"{GoapEditorSettings.BasePath}/Styles/Generic.uss");
            
            root.styleSheets.Add(styleSheet);
            
            
            // Create ListView
            var generator = scriptable.GetGenerator();
            
            root.Add(new Header("Goals"));
            goalList = new GoalList(serializedObject, scriptable, generator);
            root.Add(goalList);

            root.Add(new Header("Actions"));
            actionList = new ActionList(serializedObject, scriptable, generator);
            root.Add(actionList);

            root.Add(new Header("WorldSensors"));
            worldSensorList = new SensorList<CapabilityWorldSensor>(serializedObject, scriptable, generator, scriptable.worldSensors, "worldSensors");
            root.Add(worldSensorList);

            root.Add(new Header("TargetSensors"));
            targetSensorList = new SensorList<CapabilityTargetSensor>(serializedObject, scriptable, generator, scriptable.targetSensors, "targetSensors");
            root.Add(targetSensorList);

            root.Add(new Header("MultiSensors"));
            multiSensorList = new SensorList<CapabilityMultiSensor>(serializedObject, scriptable, generator, scriptable.multiSensors, "multiSensors");
            root.Add(multiSensorList);

            var checkButton = new Button(() =>
            {
                var issues = new ScriptReferenceValidator().CheckAll(scriptable);
                if (issues.Length == 0)
                {
                    Debug.Log("No issues found!");
                    return;
                }
                
                foreach (var issue in issues)
                {
                    Debug.Log(issue.GetMessage());
                }
            });
            checkButton.Add(new Label("Check issues"));
            root.Add(checkButton);
            
            var fixButton = new Button(() =>
            {
                var validator = new ScriptReferenceValidator();
                
                var issues = validator.CheckAll(scriptable);
                
                if (issues.Length == 0)
                {
                    Debug.Log("No issues found!");
                    return;
                }
           
                foreach (var issue in issues)
                {
                    issue.Fix(scriptable.GetGenerator());
                }
                
                EditorUtility.SetDirty(scriptable);
                AssetDatabase.SaveAssetIfDirty(scriptable);
                AssetDatabase.Refresh();
            });
            fixButton.Add(new Label("Fix issues!"));
            root.Add(fixButton);
            
            return root;
        }
    }
}