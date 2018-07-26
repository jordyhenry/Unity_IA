using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ScenePicker))]
public class ScenePickerEditor : PropertyDrawer
{
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {

		if (property.propertyType == SerializedPropertyType.String) {
			SceneAsset sceneObject = GetSceneObject(property.stringValue);
			Object scene = EditorGUI.ObjectField(position, label, sceneObject, typeof(SceneAsset), true);
			if (scene == null) {
				property.stringValue = "";
			} else if (scene.name != property.stringValue) {
				SceneAsset sceneObj = GetSceneObject(scene.name);
				if (sceneObj == null) {
					Debug.LogError("The scene " + scene.name + " cannot be used. To use this scene add it to the build settings for the project");
				} else {
					property.stringValue = scene.name;
				}
			}
		}
		else
			EditorGUI.LabelField (position, label.text, "Use [Scene] with strings.");
	}

	protected SceneAsset GetSceneObject(string sceneObjectName) {
		if (string.IsNullOrEmpty(sceneObjectName)) {
			return null;
		}

		foreach (var editorScene in EditorBuildSettings.scenes) {
			if (editorScene.path.IndexOf(sceneObjectName) != -1) {
				return AssetDatabase.LoadAssetAtPath(editorScene.path, typeof(SceneAsset)) as SceneAsset;
			}
		}
		Debug.LogError("Scene [" + sceneObjectName + "] cannot be used. Add this scene to the 'Scenes in the Build' in build settings.");
		return null;
	}
}
