using UnityEditor;
using ZeroFramework.Goap;

namespace ZeroFramework.Editor.Package
{
    [CustomPropertyDrawer(typeof(WorldSensorAttribute))]
    public class WorldSensorDrawer : ClassDrawerBase<IWorldSensor>
    {
    }
}