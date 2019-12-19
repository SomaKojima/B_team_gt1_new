using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QR_ResultWindow : MonoBehaviour
{
    [SerializeField]
    UI_Button yesButton;

    [SerializeField]
    UI_Button noButton;

    bool isClickYes = false;
    bool isClickNo = false;

    public void Initialize()
    {
        isClickYes = false;
        isClickNo = false;
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isClickYes = false;
        if (yesButton.IsClick())
        {
            yesButton.OnClickProcess();
            isClickYes = true;
        }

        isClickNo = false;
        if (noButton.IsClick())
        {
            isClickNo = true;
            noButton.OnClickProcess();
            gameObject.SetActive(false);
        }
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public bool IsClickYes()
    {
        return isClickYes;
    }
    public bool IsClickNo()
    {
        return isClickNo;
    }
}
