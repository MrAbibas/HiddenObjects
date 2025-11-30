using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using VContainer;
using VContainer.Unity;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace App.Gameplay.Cameras
{
    public class CameraController : IInitializable, ITickable
    {
        private CameraConfig _config;
        private Camera _camera;
        private float _targetZoom;
        private Vector2? _lastTouchPosition;
        private Vector2? _lastMousePosition;
        private float? _lastPinchDistance;

        [Inject]
        public void Construct(CameraConfig config)
        {
            _config = config;
        }

        public void Initialize()
        {
            _camera = Camera.main;
            _targetZoom = _camera.orthographicSize;
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
                _targetZoom -= scroll * _config.zoomSpeed;
                _targetZoom = Mathf.Clamp(_targetZoom, _config.minZoom, _config.maxZoom);
                return;
            }
#endif
            if (Touchscreen.current != null && Touchscreen.current.touches[0].phase.value == TouchPhase.Moved &&
                Touchscreen.current.touches[1].phase.value == TouchPhase.Moved)
            {
                var touch1 = Touchscreen.current.touches[0].position.ReadValue();
                var touch2 = Touchscreen.current.touches[1].position.ReadValue();

                float currentDistance = Vector2.Distance(touch1, touch2);

                if (_lastPinchDistance.HasValue)
                {
                    float delta = _lastPinchDistance.Value - currentDistance;
                    _targetZoom += delta * _config.pinchZoomSpeed;
                    _targetZoom = Mathf.Clamp(_targetZoom, _config.minZoom, _config.maxZoom);
                }

                _lastPinchDistance = currentDistance;
                Debug.Log(_targetZoom);
                return;
            }

            _lastPinchDistance = null;
        }

        void HandlePan()
        {
#if UNITY_EDITOR
            if (Input.touchCount > 0 && !IsPointerOverUI())
            {
                Touch touch = Input.GetTouch(0);
                if (_lastMousePosition.HasValue)
                {
                    Vector2 mousePos = touch.position;
                    Vector3 worldPos = _camera.ScreenToWorldPoint(mousePos);
                    Vector3 lastWorldPos = _camera.ScreenToWorldPoint(_lastMousePosition.Value);
                    Vector3 worldDelta = (worldPos - lastWorldPos) * _config.panSpeed;
                    _camera.transform.position -= new Vector3(worldDelta.x, worldDelta.y, 0);
                }

                _lastMousePosition = touch.position;
                return;
            }
#endif

            if (Touchscreen.current != null && Touchscreen.current.touches[0].phase.value == TouchPhase.Moved &&
                Touchscreen.current.touches[1].phase.value != TouchPhase.Moved)
            {
                var touch = Touchscreen.current.touches[0];
                if (_lastTouchPosition.HasValue)
                {
                    if (touch.press.isPressed && !IsPointerOverUI())
                    {
                        Vector2 touchPos = touch.position.ReadValue();
                        Vector3 worldPos = _camera.ScreenToWorldPoint(touchPos);
                        Vector3 lastWorldPos = _camera.ScreenToWorldPoint(_lastTouchPosition.Value);
                        Vector3 worldDelta = (worldPos - lastWorldPos) * _config.panSpeed;
                        _camera.transform.position -= new Vector3(worldDelta.x, worldDelta.y, 0);
                    }
                }

                _lastTouchPosition = touch.position.ReadValue();
                return;
            }

            _lastMousePosition = null;
            _lastTouchPosition = null;
        }

        void ApplyZoom()
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targetZoom,
                Time.deltaTime * _config.zoomSmoothTime);
        }

        bool IsPointerOverUI()
        {
            var uiModule = EventSystem.current?.currentInputModule as InputSystemUIInputModule;
            return uiModule != null && uiModule.IsPointerOverGameObject(0);
        }
    }
}