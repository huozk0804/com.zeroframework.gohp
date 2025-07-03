using UnityEngine.UIElements;
using ZeroFramework.Goap;

namespace ZeroFramework.Editor.Package
{
    public class AgentTypeDrawer : VisualElement
    {
        public  AgentTypeDrawer(IAgentType agentType)
        {
            name = "agent-type";
            
            var card = new Card((card) =>
            {
                card.Add(new Header(agentType.Id));

                var root = new VisualElement();

                card.schedule.Execute(() =>
                {
                    root.Clear();
                    root.Add(new Label($"Count: {agentType.Agents.All().Count}"));
                }).Every(1000);
                
                card.Add(root);
            });
            
            Add(card);
        }
    }
}