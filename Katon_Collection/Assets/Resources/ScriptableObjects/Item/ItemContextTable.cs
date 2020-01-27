using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

[CreateAssetMenu(menuName = "MyGame/Create ItemContextTable", fileName = "itemTable")]
public class ItemContextTable : ScriptableObject
{
    public Texture2D icon;

    [SerializeField, EnumListLabel(typeof(ITEM_TYPE))]
    ItemContext[] items = new ItemContext[(int)ITEM_TYPE.NUM];

    public ItemContext GetItemContex(ITEM_TYPE type)
    {
        if (type == ITEM_TYPE.NONE) return null;
        return items[(int)type];
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(ItemContextTable))]
public class ItemContextTableEditor : Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height)
    {
        ItemContextTable table = (ItemContextTable)target;

        if (table == null || table.icon == null) return null;

        Texture2D tex = new Texture2D(width, height);
        EditorUtility.CopySerialized(table.icon, tex);
        return tex;
    }
}
#endif // UNITY_EDITORG

