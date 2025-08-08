using UnityEngine.UIElements;
using Keystone.Goap;

#if UNITY_2021
using UnityEditor.UIElements;
#endif

namespace Keystone.Editor.Package
{
    public class CapabilityEffectElement : VisualElement, IFoldable
    {
        public Foldout Foldout { get; set; }
        public ClassRefField WorldKeyField { get; set; }

        public EnumField DirectionField { get; set; }
        
        public CapabilityEffectElement(CapabilityConfigScriptable scriptable, GeneratorScriptable generator)
        {
            Foldout = new Foldout
            {
                value = false,
            };
            Add(Foldout);

            var row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            
            WorldKeyField = new ClassRefField();
            WorldKeyField.style.width = new StyleLength(new Length(50, LengthUnit.Percent));
            row.Add(WorldKeyField);

            DirectionField = new EnumField(EffectType.Increase);
            DirectionField.style.width = new StyleLength(new Length(50, LengthUnit.Percent));
            row.Add(DirectionField);
            
            Foldout.Add(row);
        }
    }
}