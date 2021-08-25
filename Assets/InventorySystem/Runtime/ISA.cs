using UnityEngine;

namespace InventorySystem
{
    public class ISA : MonoBehaviour
    {
        [Header("Inventory System Assets")]
        public InventoryAssets InventoryAssets;

        private InventorySystem InventorySystem;

        private static ISA IS_A;

        private void Awake()
        {
            if(IS_A == null)
            {
                InventorySystem = GetComponent<InventorySystem>();
                IS_A = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            InventorySystem.OnInventoryInit();
        }

        public static InventoryAssets GetAssets()
        {
            return IS_A.InventoryAssets;
        }

        public static GameObject GetItemStackHandlerAsset()
        {
            return GetAssets().ItemStackHandlerAsset;
        }

    }
}