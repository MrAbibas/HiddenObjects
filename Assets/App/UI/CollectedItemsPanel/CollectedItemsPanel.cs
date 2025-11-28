using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.UI.CollectedItemsPanel
{
    public class CollectedItemsPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform slotsContainer;
        private List<CollectedItemSlot> _slots;

        private void Start()
        {
            _slots = slotsContainer.GetComponentsInChildren<CollectedItemSlot>().ToList();
        }
    }
}
