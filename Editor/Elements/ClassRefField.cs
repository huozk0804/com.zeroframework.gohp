using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using ZeroFramework.Goap;

#if UNITY_2021
using UnityEditor.UIElements;
#endif

namespace ZeroFramework.Editor.Package
{
    public class ClassRefField : VisualElement
    {
        public PopupField<Script> SelectField { get; set; }
        public TextField NameField { get; set; }
        public Circle Status { get; set; }
        
        public ClassRefField()
        {
            var row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            
            Status = new Circle(Color.black, 10);
            Status.style.marginLeft = new StyleLength(new Length(5, LengthUnit.Pixel));
            Status.style.marginRight = new StyleLength(new Length(2, LengthUnit.Pixel));
            Status.style.marginTop = new StyleLength(new Length(5, LengthUnit.Pixel));
            row.Add(Status);
            
            NameField = new TextField();
            NameField.style.flexGrow = 1;
            
            row.Add(NameField);

            SelectField = new PopupField<Script>();
            SelectField.formatListItemCallback = item => item.Type.Name;
            SelectField.style.width = new StyleLength(new Length(20, LengthUnit.Pixel));
            row.Add(SelectField);
            
            Add(row);
        }

        public void Bind(ScriptableObject scriptable, ClassRef classRef, Script[] scripts, Action<ClassRef> onValueChanged)
        {
            NameField.value = classRef.Name; // Replace with the actual property
            NameField.RegisterValueChangedCallback(evt =>
            {
                classRef.Name = evt.newValue;
                classRef.Id = "";
                onValueChanged(classRef);
                UpdateStatus(classRef, scripts);
                EditorUtility.SetDirty(scriptable); // Mark the scriptable object as dirty
            });

            // element.SelectField.value = item.name;
            SelectField.choices = scripts.ToList();
            SelectField.RegisterValueChangedCallback(evt =>
            {
                NameField.SetValueWithoutNotify(evt.newValue.Type.Name);
                classRef.Name = evt.newValue.Type.Name;
                classRef.Id = evt.newValue.Id;
                SelectField.SetValueWithoutNotify(null);
                onValueChanged(classRef);
                UpdateStatus(classRef, scripts);
                EditorUtility.SetDirty(scriptable); // Mark the scriptable object as dirty
            });
            
            UpdateStatus(classRef, scripts);
            
            schedule.Execute(() =>
            {
                UpdateStatus(classRef, scripts);
            }).Every(1000);
        }

        private void UpdateStatus(ClassRef classRef, Script[] scripts)
        {
            var status = classRef.GetStatus(scripts);

            switch (status)
            {
                case ClassRefStatus.Full:
                    Status.SetColor(Color.green);
                    Status.tooltip = $"Class is found by name and id {GetTooltip(classRef)}";
                    break;
                case ClassRefStatus.Id:
                    Status.SetColor(Color.cyan);
                    Status.tooltip = $"Class is only found by id! {GetTooltip(classRef)}";
                    break;
                case ClassRefStatus.Name:
                    Status.SetColor(Color.yellow);
                    Status.tooltip = $"Class is only found by name! {GetTooltip(classRef)}";
                    break;
                case ClassRefStatus.None:
                    Status.SetColor(Color.red);
                    Status.tooltip = $"Class is not found! {GetTooltip(classRef)}";
                    break;
                case ClassRefStatus.Empty:
                    Status.SetColor(Color.black);
                    Status.tooltip = $"There is no name or id! {GetTooltip(classRef)}";
                    break;
            }
        }

        private string GetTooltip(ClassRef classRef)
        {
            return $"\nname: {classRef.Name ?? "-"}\nid: {classRef.Id ?? "-"}\nhash: {classRef.GetHashCode()}";
        }
    }
}