using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class EventSystemCreator
    {
        public EventSystemCreator()
        {
            new GameObject("GameplayEventSystem").AddComponent<EventSystem>().AddComponent<StandaloneInputModule>();
        }
    }
}