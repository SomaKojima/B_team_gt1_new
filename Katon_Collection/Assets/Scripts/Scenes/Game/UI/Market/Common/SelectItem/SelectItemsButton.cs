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
    
    public IItem GetItem()
    {
        return item;
    }
}
