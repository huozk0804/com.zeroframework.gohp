using System.IO;
using UnityEditor;

namespace ZeroFramework.Editor.Package
{
    public static class GoapEditorSettings
    {
        public static string BasePath
        {
            get
            {
                var assets = AssetDatabase.FindAssets($"t:Script {nameof(GoapEditorSettings)}");
                
                // This should not happen, but who knows
                if (assets.Length == 0)
                    return "ZeroFramework/Editor/Packages/com.Goap/GoapEditorSettings.asset";
                
                return Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(assets[0]));
            }
        }
      
        public const string Version = "3.0.32";
    }
}
