using System;
using UnityEngine;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public class Floor : MonoBehaviour
{
    private const int WoodMaterialIndex = 0;
    private const int CarpetMaterialIndex = 1;
    private const int StoneMaterialIndex = 2;
    
    [SerializeField] private Material _selectedMaterial = Material.Wood;
    
    public int GetFMODMaterialIndex()
    {
        return _selectedMaterial switch
        {
            Material.Wood => WoodMaterialIndex,
            Material.Carpet => CarpetMaterialIndex,
            Material.Stone => StoneMaterialIndex,
            _ => WoodMaterialIndex
        };
    }
}

public enum Material
{
    Wood,
    Carpet,
    Stone
}