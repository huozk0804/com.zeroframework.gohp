using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace ZeroFramework.Editor.Package
{
    public abstract class ListElementBase<TItemType, TRenderType> : VisualElement
        where TItemType : class, new()
        where TRenderType : VisualElement, IFoldable
    {
        protected readonly SerializedProperty property;
        private readonly List<TItemType> list;
        private int selectedItemIndex;
        private readonly VisualElement elementsRoot;
        private VisualElement buttonContainer;

        public ListElementBase(SerializedProperty property, List<TItemType> list)
        {
            this.property = property;
            this.list = list;
            elementsRoot = new VisualElement();
            Add(elementsRoot);
        }

        public void Rebuild()
        {
            elementsRoot.Clear();

            if (!property.isArray)
            {
                return;
            }

            for (var i = 0; i < property.arraySize; i++)
            {
                CreateElement(i);
            }

            if (buttonContainer is not null)
                return;

            buttonContainer = new VisualElement();
            buttonContainer.style.flexDirection = FlexDirection.Row;
            buttonContainer.style.justifyContent = Justify.FlexEnd;

            var addButton = new Button(AddItem)
            {
                text = "+",
            };
            buttonContainer.Add(addButton);

            var removeButton = new Button(RemoveSelectedItem)
            {
                text = "-",
            };
            buttonContainer.Add(removeButton);

            Add(buttonContainer);
        }

        private void CreateElement(int i)
        {
            if (i < 0 || i >= list.Count)
                return;

            var value = list[i];
            var prop = property.GetArrayElementAtIndex(i);

            var element = CreateListItem(prop, value);
            element.RegisterCallback<ClickEvent>(evt =>
            {
                selectedItemIndex = i;
            });
            elementsRoot.Add(element);

            BindListItem(prop, element, value, i);
        }

        protected abstract TRenderType CreateListItem(SerializedProperty property, TItemType item);

        private void BindListItem(SerializedProperty property, VisualElement element, int index)
        {
            BindListItem(property, element as TRenderType, list[index], index);
        }

        protected abstract void BindListItem(SerializedProperty property, TRenderType element, TItemType item, int index);

        private void AddItem()
        {
            // Add your item to the scriptable's list
            // Example: scriptable.items.Add(newItem);
            property.arraySize++;

            // var element = this.property.GetArrayElementAtIndex(this.property.arraySize -1);
            // element.managedReferenceValue = new TItemType();

            list.Add(new TItemType());

            CreateElement(property.arraySize - 1);

            CloseAll();

            var foldable = GetFoldable(property.arraySize - 1);

            if (foldable is null)
                return;

            foldable.Foldout.value = true;

            Rebuild(); // Refresh the ListView
        }

        private void RemoveSelectedItem()
        {
            if (selectedItemIndex < 0 || selectedItemIndex >= property.arraySize)
                return;

            list.RemoveAt(selectedItemIndex);

            Rebuild();
        }

        private void CloseAll()
        {
            for (var i = 0; i < property.arraySize; i++)
            {
                var foldable = GetFoldable(i);

                if (foldable is null)
                    continue;

                foldable.Foldout.value = false;
            }
        }

        private IFoldable GetFoldable(int index)
        {
            if (index < 0 || index >= elementsRoot.childCount)
                return null;

            return elementsRoot.ElementAt(index) as IFoldable;
        }
    }

    public interface IFoldable
    {
        public Foldout Foldout { get; }
    }
}
