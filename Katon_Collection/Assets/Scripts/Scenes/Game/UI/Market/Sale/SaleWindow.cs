using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleWindow : MonoBehaviour
{

    [SerializeField]
    UI_Button_Market applyButton;

    bool isExchange = false;

    List<IItem> exchangeItems = new List<IItem>();

    public void Initialize()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        isExchange = false;

        // 交換ボタンが押された
        if (applyButton.IsClick())
        {
            isExchange = true;
        }
    }


    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void UnActive()
    {
        gameObject.SetActive(false);
    }



    public bool IsExchange()
    {
        return isExchange;
    }
}
