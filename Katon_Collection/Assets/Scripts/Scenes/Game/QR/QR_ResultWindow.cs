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

    public void Initialize()
    {
        isClickYes = false;
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

        if (noButton.IsClick())
        {
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
}
