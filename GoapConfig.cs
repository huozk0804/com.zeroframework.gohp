using UnityEngine;

namespace ZeroFramework.Goap.Config
{
    [CreateAssetMenu(fileName = "GoapConfig", menuName = "Zero/Goap Config", order = 100)]
    public sealed class GoapConfig : ScriptableObjectSingleton<GoapConfig>
    {
        public string goapControllerHelperTypeName = "ZeroFramework.Goap.GoapControllerHelper";
        public GoapControllerBase goapCustomControllerCustomHelper = null;
    }
}