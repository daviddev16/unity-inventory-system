using System.Collections;
using UnityEngine;

namespace InventorySys
{
    [CreateAssetMenu(fileName = "NewInventoryBook", menuName = "Create a Book...", order = 5)]
    public class InventoryBook : ScriptableObject
    {
        public ItemStack[] Items;
    }
}