using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QR_ReaderWindow : MonoBehaviour
{
    [SerializeField]
    QR_Reader reader;

    [SerializeField]
    QR_ResultWindow resultWindow;
    
    [SerializeField]
    UI_Button backButton;

    public void Initialize()
    {
        gameObject.SetActive(true);
        reader.Initialize();
        resultWindow.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (backButton.IsClick())
        {
            backButton.OnClickProcess();
            gameObject.SetActive(false);
        }

        if (reader.IsCorrectRead())
        {
            resultWindow.Active();
        }

        if(resultWindow.IsClickYes())
        {
            gameObject.SetActive(false);
        }
    }
}
