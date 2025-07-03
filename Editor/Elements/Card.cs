using System;
using UnityEngine.UIElements;

namespace ZeroFramework.Editor.Package
{
    public class Card : VisualElement
    {
        public Card(Action<Card> callback)
        {
            name = "card";
            callback(this);
        }
    }
}