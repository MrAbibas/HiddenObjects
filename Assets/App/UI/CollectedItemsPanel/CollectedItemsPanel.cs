using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.UI.CollectedItemsPanel
{
    public class CollectedItemsPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform slotsContainer;
        public List<CollectedItemSlot> Slots { get; private set; }

        private void Start()
        {
            Slots = slotsContainer.GetComponentsInChildren<CollectedItemSlot>().ToList();
        }
    }
}
