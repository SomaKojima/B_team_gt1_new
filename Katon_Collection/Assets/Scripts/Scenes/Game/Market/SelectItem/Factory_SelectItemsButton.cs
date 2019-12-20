using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_SelectItemsButton : MonoBehaviour
{
    [SerializeField]
    GameObject selectItemPrefab = null;

    [SerializeField]
    Transform prefabPerent = null;

    [SerializeField]
    ItemContextTable table;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SelectItemsButton Create(ITEM_TYPE type)
    {
        GameObject obj = Instantiate(selectItemPrefab, prefabPerent);

        SelectItemsButton btn = obj.GetComponent<SelectItemsButton>();
        btn.Initialize(type, table.GetItemContex(type).GetSprite());

        return btn;
    }
}
