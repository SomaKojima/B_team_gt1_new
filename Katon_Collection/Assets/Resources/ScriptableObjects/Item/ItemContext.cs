using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create ItemContext", fileName = "item")]
public class ItemContext : ScriptableObject
{
    [SerializeField]
    Sprite sprite;
    [SerializeField]
    Material material;

    public Sprite GetSprite()
    {
        return sprite;
    }

    public Material GetMaterial()
    {
        return material;
    }
}
