using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace App.UI.CollectedItemsPanel
{
    public class CollectedItemsPanel : MonoBehaviour, IInitializable
    {
        [SerializeField] private RectTransform slotsContainer;
        public List<CollectedItemSlot> Slots { get; private set; }
        
        public void Initialize()
        {
            Slots = slotsContainer.GetComponentsInChildren<CollectedItemSlot>().ToList();
        }
    }
}
