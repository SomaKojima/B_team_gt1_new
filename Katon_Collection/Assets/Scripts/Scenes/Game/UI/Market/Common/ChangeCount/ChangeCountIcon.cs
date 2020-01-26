using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCountIcon : MonoBehaviour
{
    [SerializeField]
    ItemContextTable table;

    [SerializeField]
    Image image;
    [SerializeField]
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(ITEM_TYPE type, bool isPowerUp)
    {
        image.sprite = table.GetItemContex(type).GetSprite();
        if (isPowerUp)
        {
            image.sprite = table.GetItemContex(type).GetPowerUpSprite();
        }
    }

    public void SetNum(int num)
    {
        text.text = num.ToString();
    }
}
