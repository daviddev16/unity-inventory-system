using UnityEditor;
using InventorySys;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    SerializedProperty ItemAssetProperty;

    void OnEnable()
    {
        ItemAssetProperty = serializedObject.FindProperty("ItemAsset");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(ItemAssetProperty);
        serializedObject.ApplyModifiedProperties();
    }

}
