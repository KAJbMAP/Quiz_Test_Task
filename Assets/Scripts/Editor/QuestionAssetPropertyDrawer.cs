using Data;
using Enums;
using Misc;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(QuestionAsset))]
    [CanEditMultipleObjects]
    public class QuestionAssetPropertyDrawer : UnityEditor.Editor
    {
        private SerializedProperty _tileChilds;
        private SerializedProperty _leftChild;
        private SerializedProperty _topChild;
        private SerializedProperty _rightChild;
        private SerializedProperty _bottomChild;
        private SerializedProperty _questionId;
        private SerializedProperty _questionCategoryAsset;
        private SerializedProperty _questionInfo;

        private void OnEnable()
        {
            _tileChilds = serializedObject.FindProperty("_tileChilds");
            _leftChild = serializedObject.FindProperty("_leftChild");
            _topChild = serializedObject.FindProperty("_topChild");
            _rightChild = serializedObject.FindProperty("_rightChild");
            _bottomChild = serializedObject.FindProperty("_bottomChild");
            _questionId = serializedObject.FindProperty("_questionId");
            _questionCategoryAsset = serializedObject.FindProperty("_questionCategoryAsset");
            _questionInfo = serializedObject.FindProperty("_questionInfo");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_tileChilds);

            var tileChilds = (TileChilds) _tileChilds.enumValueFlag;
            
            if (FlagsHelper.IsSet(tileChilds, TileChilds.Left)) EditorGUILayout.PropertyField(_leftChild);
            
            if (FlagsHelper.IsSet(tileChilds, TileChilds.Top)) EditorGUILayout.PropertyField(_topChild);
            
            if (FlagsHelper.IsSet(tileChilds, TileChilds.Right)) EditorGUILayout.PropertyField(_rightChild);
            
            if (FlagsHelper.IsSet(tileChilds, TileChilds.Bottom)) EditorGUILayout.PropertyField(_bottomChild);

            EditorGUILayout.PropertyField(_questionId);
            EditorGUILayout.PropertyField(_questionCategoryAsset);
            EditorGUILayout.PropertyField(_questionInfo);

            serializedObject.ApplyModifiedProperties();
        }
    }
}