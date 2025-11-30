using UnityEngine;

namespace App.Gameplay.Common
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Gameplay/CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        [Header("Pan Settings")]
        public float panSpeed = 5f;

        [Header("Zoom Settings")]
        public float zoomSpeed = 3f;
        public float minZoom = 0.5f;
        public float maxZoom = 3f;
        public float zoomSmoothTime = 0.2f;
    
        [Header("Touch Settings")]
        public float pinchZoomSpeed = 0.1f;
    }
}