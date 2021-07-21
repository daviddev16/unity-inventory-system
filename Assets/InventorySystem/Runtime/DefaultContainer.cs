using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DefaultContainer : MonoBehaviour, System.IComparable<DefaultContainer>
{
    public List<Slot> Slots { get => slots; set => slots = value; }

    [SerializeField]
    private List<Slot> slots;

    [SerializeField]
    private int SortingLayer = -1;

    public int CompareTo(DefaultContainer other)
    {
        return (other.SortingLayer > SortingLayer) ? -1 : 1;
    }
}