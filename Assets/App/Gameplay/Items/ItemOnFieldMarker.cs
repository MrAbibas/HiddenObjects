using System;
using UnityEngine;

namespace App.Gameplay.Items
{
    [Serializable]
    public class ItemOnFieldMarker
    {
        public ItemType Type;
        public Vector3 Position;
        public int SortOrder;
    }
}