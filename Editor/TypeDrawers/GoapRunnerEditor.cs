using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    [CustomEditor(typeof(GoapControllerBase))]
    public class GoapRunnerEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            //var runner = Zero.goap;
            var root = new VisualElement();

            root.styleSheets.Add(
                AssetDatabase.LoadAssetAtPath<StyleSheet>($"{GoapEditorSettings.BasePath}/Styles/Generic.uss"));
            root.Add(new PropertyField(serializedObject.FindProperty("configInitializer")));

            RenderConfigFactories(root);
            if (Application.isPlaying)
            {
                root.Add(new Header("Agent Types"));
                //foreach (var agentTypes in runner.AgentTypes)
                //{
                //    root.Add(new AgentTypeDrawer(agentTypes));
                //}
            }

            return root;
        }

        private void RenderConfigFactories(VisualElement root)
        {
            root.Add(new PropertyField(serializedObject.FindProperty("agentTypeConfigFactories")));
        }
    }
}