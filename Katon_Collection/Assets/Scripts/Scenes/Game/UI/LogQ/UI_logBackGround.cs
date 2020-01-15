using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_logBackGround : MonoBehaviour
{
    [SerializeField]
    RectTransform backGround;

    [SerializeField]
    RectTransform obj;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        backGround.sizeDelta.Set(obj.rect.width,obj.rect.height);
    }
}
