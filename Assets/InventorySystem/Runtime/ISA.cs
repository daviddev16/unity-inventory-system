using System.Collections;
using UnityEngine;

namespace InventorySystem
{
    public class ISA : MonoBehaviour
    {
        [Header("Inventory System Assets")]
        public InventoryAssets InventoryAssets;

        private static ISA IS_A;

        private void Awake()
        {
            if(IS_A == null)
            {
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