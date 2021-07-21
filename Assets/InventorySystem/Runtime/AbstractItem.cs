using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AbstractItem
{
    public bool Stackable { get; set; }
    public string Name { get; set; }

    public AbstractItem(bool Stackable, string Name)
    {
        this.Stackable = Stackable;
        this.Name = Name;
    }

}
