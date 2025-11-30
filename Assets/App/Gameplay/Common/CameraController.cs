using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using VContainer;
using VContainer.Unity;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace App.Gameplay.Common
{
    public class CameraController : IInitializable, ITickable
    {
        private CameraConfig _config;

        private Camera cam;
        private float targetZoom;
        private Vector2? lastTouchPosition;
        private Vector2? lastMousePosition;
        private float? lastPinchDistance;

        [Inject]
        public void Construct(CameraConfig config)
        {
            _config = config;
        }
        
        public void Initialize()
        {
            cam = Camera.main;
            targetZoom = cam.orthographicSize;
        }

        public void Tick()
        {
            HandleZoom();
            HandlePan();
            ApplyZoom();
        }

        void HandleZoom()
        {
#if UNITY_EDITOR
            if (Mouse.current != null && Mouse.current.scroll.ReadValue().y != 0)
            {
                float scroll = Mouse.current.scroll.ReadValue().y;
                targetZoom -= scroll * _config.zoomSpeed;
                targetZoom = Mathf.Clamp(targetZoom, _config.minZoom, _config.maxZoom);
                return;
            }
#endif
            if (Touchscreen.current != null && Touchscreen.current.touches[0].phase.value == TouchPhase.Moved && Touchscreen.current.touches[1].phase.value == TouchPhase.Moved)
            {
                var touch1 = Touchscreen.current.touches[0].position.ReadValue();
                var touch2 = Touchscreen.current.touches[1].position.ReadValue();

                float currentDistance = Vector2.Distance(touch1, touch2);

                if (lastPinchDistance.HasValue)
                {
                    float delta = lastPinchDistance.Value - currentDistance;
                    targetZoom += delta * _config.pinchZoomSpeed;
                    targetZoom = Mathf.Clamp(targetZoom, _config.minZoom, _config.maxZoom);
                }

                lastPinchDistance = currentDistance;
                Debug.Log(targetZoom);
                return;
            }

            lastPinchDistance = null;
        }

        void HandlePan()
        {
#if UNITY_EDITOR
            if (Input.touchCount > 0 && !IsPointerOverUI())
            {
                Touch touch = Input.GetTouch(0);
                if (lastMousePosition.HasValue)
                {
                    Vector2 mousePos = touch.position;
                    Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
                    Vector3 lastWorldPos = cam.ScreenToWorldPoint(lastMousePosition.Value);
                    Vector3 worldDelta = (worldPos - lastWorldPos) *  _config.panSpeed;
                    cam.transform.position -= new Vector3(worldDelta.x, worldDelta.y, 0);
                }

                lastMousePosition = touch.position;
                return;
            }
#endif

            if (Touchscreen.current != null && Touchscreen.current.touches[0].phase.value == TouchPhase.Moved  && Touchscreen.current.touches[1].phase.value != TouchPhase.Moved)
            {
                var touch = Touchscreen.current.touches[0];
                if (lastTouchPosition.HasValue)
                {
                    if (touch.press.isPressed && !IsPointerOverUI())
                    {
                        Vector2 touchPos = touch.position.ReadValue();
                        Vector3 worldPos = cam.ScreenToWorldPoint(touchPos);
                        Vector3 lastWorldPos = cam.ScreenToWorldPoint(lastTouchPosition.Value);
                        Vector3 worldDelta = (worldPos - lastWorldPos) *  _config.panSpeed;
                        cam.transform.position -= new Vector3(worldDelta.x, worldDelta.y, 0);
                    }
                }

                lastTouchPosition = touch.position.ReadValue();
                return;
            }

            lastMousePosition = null;
            lastTouchPosition = null;
        }

        void ApplyZoom()
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * 10f);
        }

        bool IsPointerOverUI()
        {
            var uiModule = EventSystem.current?.currentInputModule as InputSystemUIInputModule;
            return uiModule != null && uiModule.IsPointerOverGameObject(0);
        }
    }
}