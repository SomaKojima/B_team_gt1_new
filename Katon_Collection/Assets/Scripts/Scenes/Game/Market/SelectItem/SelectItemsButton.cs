using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemsButton : UI_Button
{
    [SerializeField]
    private Image image = null;
    [SerializeField]
    Text text;

    IItem item = new Item();

    int maxCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = item.GetCount().ToString();
    }

    public void Initialize(ITEM_TYPE _type, Sprite _sprite)
    {
        image.sprite = _sprite;
        item.Initialize(0, _type);
        text.text = "0";
    }

    public void OnClickPlus()
    {
        item.AddCount(1);
        if (item.GetCount() > maxCount) item.SetCount(maxCount);
    }

    public void OnClickMinus()
    {
        item.AddCount(-1);
        if (item.GetCount() < 0) item.SetCount(0);
    }

    public void SetMaxCount(int cnt)
    {
        maxCount = cnt;
    }

    public IItem GetItem()
    {
        return item;
    }
}
