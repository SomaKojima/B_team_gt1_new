using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingItemUnit : MonoBehaviour
{
    [SerializeField]
    ItemContextTable itemContextTable;

    [SerializeField]
    Image icon;
    [SerializeField]
    Text numText;

    int count = 0;

    public void Initialize(ITEM_TYPE _type, int _count)
    {
        icon.sprite = itemContextTable.GetItemContex(_type).GetSprite();
        count = _count;
        numText.text = count.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
