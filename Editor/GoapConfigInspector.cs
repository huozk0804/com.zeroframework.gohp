using UnityEditor;
using ZeroFramework.Editor;

namespace ZeroFramework.Goap.Editor
{
    [CustomEditor(typeof(GoapConfig))]
    public sealed class GameFrameworkConfigInspector : GameFrameworkInspector
    {
        private readonly HelperInfo<GoapControllerBase> _goapControllerHelperInfo =
            new HelperInfo<GoapControllerBase>("goapController");

        public override void OnInspectorGUI () {
            base.OnInspectorGUI();
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                EditorGUILayout.LabelField("Goap", EditorStyles.boldLabel);
                _goapControllerHelperInfo.Draw();
            }
            EditorGUI.EndDisabledGroup();
        }

        protected override void OnCompileComplete () {
            base.OnCompileComplete();
            _goapControllerHelperInfo.Refresh();
        }

        private void OnEnable () {
            _goapControllerHelperInfo.Init(serializedObject);
            _goapControllerHelperInfo.Refresh();
        }
    }
}