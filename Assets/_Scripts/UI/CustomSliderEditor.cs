using UnityEditor;
using UnityEditor.UI;

namespace GrassVsFps
{
    [CustomEditor(typeof(CustomSlider), true)]
    [CanEditMultipleObjects]
    public class CustomSliderEditor : SliderEditor
    {
        private SerializedProperty _onEndDragAndValueChanged;

        protected override void OnEnable()
        {
            base.OnEnable();
            _onEndDragAndValueChanged = serializedObject.FindProperty("_onEndDragAndValueChanged");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_onEndDragAndValueChanged);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
