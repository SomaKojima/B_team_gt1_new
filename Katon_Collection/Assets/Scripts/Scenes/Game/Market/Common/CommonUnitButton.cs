using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUnitButton : UI_Button_Market
{
    private List<IItem> getItems = null;
    private int requiredNum = 0;

    [SerializeField]
    Transform parent;

    // Factory_CommonUnitIconオブジェクト
    [SerializeField]
    Factory_CommonUnitIcon factoryCmnIcn;
    // Manager_CommonUnitIconオブジェクト
    [SerializeField]
    Manager_CommonUnitIcon managerCmnIcn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(List<IItem> _getItems, int _requiredNum)
    {
        getItems = _getItems;
        requiredNum = _requiredNum;

        foreach(IItem item in _getItems)
        {
            factoryCmnIcn.Create(parent, item.GetItemType(), requiredNum);
        }
    }

    public int GetRequiredNum() { return requiredNum; }

    public List<IItem> GetGetItems() { return getItems; }
}
