using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    [CustomEditor(typeof(AgentTypeFactoryBase), true)]
    public class AgentTypeFactoryBaseEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var factory = (AgentTypeFactoryBase) target;
            
            var root = new VisualElement();
            
            var defaultInspector = new VisualElement();
            InspectorElement.FillDefaultInspector(defaultInspector, serializedObject, this);
            
            root.Add(defaultInspector);

            var validateButton = new Button(() =>
            {
                var validator = new AgentTypeConfigValidatorRunner();
                var results = validator.Validate(factory.Create());
                
                foreach (var error in results.GetErrors())
                {
                    Debug.LogError(error);
                }
            
                foreach (var warning in results.GetWarnings())
                {
                    Debug.LogWarning(warning);
                }
                
                if (!results.HasErrors() && !results.HasWarnings())
                    Debug.Log("No errors or warnings found!");
            });
            
            validateButton.Add(new Label("Validate"));

            root.Add(validateButton);
            
            return root;
        }
    }
}