using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemsButton : MonoBehaviour
{
    private ITEM_TYPE type = ITEM_TYPE.NONE;

    [SerializeField]
    private Image image = null;

    int count = 0;

    int maxCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(ITEM_TYPE _type, Sprite _sprite)
    {
        type = _type;
        image.sprite = _sprite;
    }

    public void OnClickPlus()
    {
        count++;
        if (count > maxCount) count = maxCount;
    }

    public void OnClickMinus()
    {
        count--;
        if (count < 0) count = 0;
    }

    public void SetMaxCount(int cnt)
    {
        count = cnt;
    }

    public int GetCount()
    {
        return count;
    }
}
