using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultGame : MonoBehaviour
{
    [SerializeField]
    Congratulation congratulation;
    [SerializeField]
    RankIcon rankIcon;
    [SerializeField]
    Manager_Floor_Result manager_Floor_Result;
    [SerializeField]
    Factory_Floor_Result factory_Floor_Result;
    [SerializeField]
    UI_Button_GoToTitle ui_Button_GoToTitle;
    [SerializeField]
    CameraResultMove cameraResultMove;

    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time++;
        if(time > 60)
        {

            time = 0;
        }
    }
}
