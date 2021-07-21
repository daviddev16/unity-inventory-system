using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public DisplayedItem DisplayedItem { get => displayedItem; set => displayedItem = value; }
    
    [SerializeField] private DisplayedItem displayedItem;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool IsEmpty()
    {
        return displayedItem == null;
    }
}
