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
    UI_Button_GoToTitle ui_Button_GoToTitle;
    [SerializeField]
    CameraResultMove cameraResultMove;
    [SerializeField]
    Owner_Floor owner_Floor;

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
            owner_Floor.Building(Type.cave);
            time = 0;
        }
        if (owner_Floor.GetTop(Type.cave) != null)
        {
            cameraResultMove.Move(owner_Floor.GetTop(Type.cave).transform.position);
        }
    }
}
