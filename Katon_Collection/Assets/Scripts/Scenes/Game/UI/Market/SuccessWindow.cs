using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessWindow : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    Button bt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //this.gameObject.SetActive(false);
        }
    }

    public void Success()
    {
        text.text = "こうかんした";
    }
    public void Field()
    {
        text.text = "しっぱいした";
    }

    public void OnClick()
    {
        this.gameObject.SetActive(false);
    }
}
