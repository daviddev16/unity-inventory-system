using UnityEditor;
using UnityEngine;
using InventorySys;

class InventorySystemEditor : EditorWindow
{

    readonly string[] PRESET_NAMES = { "#1 Survival! Inventory", "#2 Going_Crazy! Inventory", "#3 Touchpad! Inventory" };

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
                GUILayout.BeginVertical();
                GUILayout.Label("New Container...");
                containerType = (ContainerType)EditorGUILayout.EnumPopup("Type: ", containerType);

                if(GUILayout.Button("Create Container"))
                {
                    if(CreateNewContainer(inventory))
                    {
                        Debug.Log("New container added.");
                    }
                }

                if(GUILayout.Button("Create Slot"))
                {
                    GameObject selectedObject = Selection.activeGameObject;
                    if(selectedObject != null && selectedObject.GetComponent<SlotGroup>() != null)
                    {
                        if(CreateNewSlot(selectedObject.GetComponent<SlotGroup>()))
                        {
                            Debug.Log("Slot added.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("You need to select a SlotGroup gameObject before.");
                    }
                }

                GUILayout.EndVertical();
            }
            else
            {
                if(GUILayout.Button("Create Inventory..."))
                {
                    if(CreateNewInventory(canvas))
                    {
                        Debug.Log("Inventory created.");
                    }
                    return;
                }

                EditorGUILayout.Separator();
                GUILayout.Label("Create by preset: ");
                for(int i = 0; i < PRESET_NAMES.Length; i++)
                {
                    if(GUILayout.Button(PRESET_NAMES[i] + "..."))
                    {
                        if(CreateNewInventoryPreset(canvas, i))
                        {
                            Debug.Log("Preset created.");
                        }
                        return;
                    }
                }

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

    bool CreateNewSlot(SlotGroup slotGroup)
    {
        GameObject slotInstance = Instantiate(GetSystemPrefab("NewSlot"), Vector3.zero, Quaternion.identity, slotGroup.transform);
        slotInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        slotInstance.name = "Slot";
        return slotInstance != null;
    }

    bool CreateNewInventoryPreset(Canvas canvas, int num)
    {
        GameObject inventoryInstance = Instantiate(GetSystemPrefab("InventoryPreset" + (num + 1)), Vector3.zero, Quaternion.identity, canvas.transform);
        inventoryInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        inventoryInstance.name = PRESET_NAMES[num];
        return inventoryInstance != null;
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
