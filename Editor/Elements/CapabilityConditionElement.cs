using UnityEngine.UIElements;
using Keystone.Goap;

#if UNITY_2021
using UnityEditor.UIElements;
#endif

namespace Keystone.Editor.Package
{
    public class CapabilityConditionElement : VisualElement, IFoldable
    {
        public Foldout Foldout { get; set; }
        public IntegerField AmountField { get; set; }
        public EnumField ComparisonField { get; set; }
        public ClassRefField WorldKeyField { get; set; }
        
        public CapabilityConditionElement(CapabilityConfigScriptable scriptable, GeneratorScriptable generator)
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
            
            ComparisonField = new EnumField(Comparison.GreaterThan);
            ComparisonField.style.width = new StyleLength(new Length(30, LengthUnit.Percent));
            row.Add(ComparisonField);

            AmountField = new IntegerField();
            AmountField.style.width = new StyleLength(new Length(20, LengthUnit.Percent));
            row.Add(AmountField);
            
            Foldout.Add(row);
        }
    }
}