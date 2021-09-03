using System.Collections;
using UnityEngine;
using InventorySys;

namespace Assets
{
    public class GameInventorySystem : InventorySystem
    {
        private void Start()
        {
            AddAllSamples();
            Set("Milk", 20, 32);
        }
    }
}