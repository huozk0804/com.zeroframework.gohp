using UnityEngine.UIElements;

namespace Keystone.Editor.Package
{
    public class LabeledField<T> : VisualElement
        where T : VisualElement, new()
    {
        public Label Label { get; private set; }
        public T Field { get; private set; }

        public LabeledField(string labelText)
        {
            Init(labelText, new T());
        }

        public LabeledField(string labelText, T field)
        {
            Init(labelText, field);
        }

        private void Init(string labelText, T field)
        {
            style.flexDirection = FlexDirection.Row;

            Label = new Label(labelText);
            Label.style.width = new StyleLength(new Length(40, LengthUnit.Percent));
            Add(Label);

            Field = field;
            Field.style.width = new StyleLength(new Length(60, LengthUnit.Percent));
            Add(Field);
        }
    }
}