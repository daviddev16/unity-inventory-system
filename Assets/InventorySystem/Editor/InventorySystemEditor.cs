using UnityEditor;
using UnityEngine;
using InventorySys;

class InventorySystemEditor : EditorWindow
{

    Inventory inventory;
    ContainerType containerType;

    [MenuItem("InventorySystem/Builder...")]
    public static void ShowWindow()
    {
        GetWindow(typeof(InventorySystemEditor));
    }

    void OnGUI()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if(canvas != null)
        {
            inventory = canvas.GetComponentInChildren<Inventory>();
            if(inventory != null)
            {
                GUILayout.Label("Handled inventory: Canvas -> " + inventory.name);
                EditorGUILayout.Separator();
                GUILayout.Label("New Container...");
                GUILayout.BeginVertical();
                containerType = (ContainerType)EditorGUILayout.EnumPopup("Type: ", containerType);

                if(GUILayout.Button("Create Container"))
                {
                    if(CreateNewContainer(inventory))
                    {
                        Debug.Log("New container added.");
                    }
                }
                GUILayout.EndVertical();
            }
            else if(GUILayout.Button("Create Inventory..."))
            {
                if(CreateNewInventory(canvas))
                {
                    Debug.Log("Inventory created.");
                }
                return;
            }
        }
        else
        {
            Debug.LogWarning("There is no canvas.");
        }
    }

    GameObject GetSystemPrefab(string name)
    {
        return AssetDatabase.LoadAssetAtPath("Assets/InventorySystem/Prefabs/"+name+".prefab", typeof(GameObject)) as GameObject;
    }

    bool CreateNewContainer(Inventory inventory)
    {
        string containerName = containerType == ContainerType.GRID_SLOT ? "NewGridContainer" : "NewFreeContainer";
        GameObject containerInstance = Instantiate(GetSystemPrefab(containerName), Vector3.zero, Quaternion.identity, inventory.transform);
        containerInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        containerInstance.name = containerType == ContainerType.GRID_SLOT ? "Grid Container" : "Free Container"; ;
        return containerInstance != null;
    }

    bool CreateNewInventory(Canvas canvas)
    {
        GameObject inventoryInstance = Instantiate(GetSystemPrefab("NewInventory"), Vector3.zero, Quaternion.identity, canvas.transform);
        inventoryInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        inventoryInstance.name = "Inventory";
        return inventoryInstance != null;
    }
}
