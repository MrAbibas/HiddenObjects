using App.Gameplay.Items;
using App.Gameplay.Level;
using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class HiddenLevelExporter : EditorWindow
    {
        public LevelConfig levelConfig;
        public SpriteRenderer background;

        private SerializedObject _serializedLevelConfig;
        private SerializedProperty _targetItemsProperty;

        [MenuItem("Tools/Hidden Object Level Exporter")]
        public static void ShowWindow()
        {
            GetWindow<HiddenLevelExporter>("Level Exporter");
        }

        private void OnEnable()
        {
            if (levelConfig != null)
                InitSerializedObject();
        }

        private void InitSerializedObject()
        {
            _serializedLevelConfig = new SerializedObject(levelConfig);
            _targetItemsProperty = _serializedLevelConfig.FindProperty(nameof(LevelConfig.targetItems));
        }

        private void OnGUI()
        {
            levelConfig =
                (LevelConfig)EditorGUILayout.ObjectField("Level Config", levelConfig, typeof(LevelConfig),
                    false);

            if (levelConfig != null && _serializedLevelConfig == null)
            {
                InitSerializedObject();
            }

            background = (SpriteRenderer)EditorGUILayout.ObjectField("Background Transform", background,
                typeof(SpriteRenderer), true);

            if (_serializedLevelConfig != null)
            {
                _serializedLevelConfig.Update();
                EditorGUILayout.PropertyField(_targetItemsProperty, new GUIContent("Target Items"), true);
                _serializedLevelConfig.ApplyModifiedProperties();
            }

            if (GUILayout.Button("Export Markers to LevelConfig"))
            {
                ExportMarkers();
            }
        }

        void ExportMarkers()
        {
            if (levelConfig == null || background == null)
            {
                Debug.LogError("Assign LevelConfig and BackgroundTransform!");
                return;
            }
            
            var items = FindObjectsByType<ItemOnField>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            levelConfig.SetItemOnField(items);
            
            var size = background.sprite.bounds.size;
            var bgPos = background.transform.position;

            levelConfig.SetBackground(background.sprite, background.transform.localScale);

            EditorUtility.SetDirty(levelConfig);
            AssetDatabase.SaveAssets();

            Debug.Log($"Exported {items.Length} markers to {levelConfig.name}");
        }
    }
}
