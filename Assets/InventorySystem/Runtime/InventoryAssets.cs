using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "InventoryAsset", menuName = "New InventoryAsset", order = -1)]
    public class InventoryAssets : ScriptableObject
    {
        public GameObject ItemStackHandlerAsset;
    }
}