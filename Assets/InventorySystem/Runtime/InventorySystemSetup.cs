using System.Collections;
using UnityEngine;

public class InventorySystemSetup : MonoBehaviour
{

    public static GameObject DisplayedItemPrefab;
    
    
    [SerializeField] private InventoryPrefabs InventoryPrefabs;

    void Awake()
    {
        DisplayedItemPrefab = InventoryPrefabs.DisplayedItemPrefab;
    }

}