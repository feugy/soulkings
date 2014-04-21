using UnityEditor;
using UnityEngine;

[CustomEditor ( typeof ( Quad ) ), CanEditMultipleObjects]
class QuadInspector : Editor
{
	SerializedProperty TopLeftColor;
	SerializedProperty TopRightColor;
	SerializedProperty BottomLeftColor;
	SerializedProperty BottomRightColor;
	SerializedProperty Width;
	SerializedProperty Height;
	SerializedProperty Alignment;
	
	void OnEnable ()
	{
		TopLeftColor = serializedObject.FindProperty ( "TopLeftColor" );
		TopRightColor = serializedObject.FindProperty ( "TopRightColor" );
		BottomLeftColor = serializedObject.FindProperty ( "BottomLeftColor" );
		BottomRightColor = serializedObject.FindProperty ( "BottomRightColor" );
		Width = serializedObject.FindProperty ( "Width" );
		Height = serializedObject.FindProperty ( "Height" );
		Alignment = serializedObject.FindProperty ( "Alignment" );
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();

		Quad quad = target as Quad;
		Color newColor = EditorGUILayout.ColorField ( "Color", quad.TopLeftColor );
		if ( GUI.changed )
		{
			TopLeftColor.colorValue = newColor;
			TopRightColor.colorValue = newColor;
			BottomLeftColor.colorValue = newColor;
			BottomRightColor.colorValue = newColor;
		}

		EditorGUILayout.BeginHorizontal ();

			GUILayout.FlexibleSpace ();
			EditorGUILayout.PropertyField ( TopLeftColor, GUIContent.none, GUILayout.Width ( 60 ) );
			EditorGUILayout.PropertyField ( TopRightColor, GUIContent.none, GUILayout.Width ( 60 ) );

		EditorGUILayout.EndHorizontal ();


		EditorGUILayout.BeginHorizontal ();

			GUILayout.FlexibleSpace ();
			EditorGUILayout.PropertyField ( BottomLeftColor, GUIContent.none, GUILayout.Width ( 60 ) );
			EditorGUILayout.PropertyField ( BottomRightColor, GUIContent.none, GUILayout.Width ( 60 ) );

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();

			EditorGUILayout.LabelField ( "Size", GUILayout.Width ( 60 ) );
			GUILayout.FlexibleSpace ();
			EditorGUILayout.PropertyField ( Width, GUIContent.none, GUILayout.Width ( 50 ) );
			EditorGUILayout.LabelField ( "x", GUILayout.Width ( 12 ) );
			EditorGUILayout.PropertyField ( Height, GUIContent.none, GUILayout.Width ( 50 ) );

		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.PropertyField ( Alignment );

		serializedObject.ApplyModifiedProperties ();

		if ( GUI.changed )
		{
			foreach ( Quad q in targets )
				q.Recreate ();
		}
	}
}