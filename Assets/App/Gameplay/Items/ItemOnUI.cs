using UnityEngine;

namespace App.Gameplay.Items
{
    public class ItemOnUI : MonoBehaviour
    {
        [field: SerializeField] public ItemType Type {get; private set;}
    }
}