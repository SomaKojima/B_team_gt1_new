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
    Floor landingFloor = null;
    // Start is called before the first frame update
    void Start()
    {
        owner_Floor.Initialize();
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
        landingFloor = owner_Floor.GetTopLandingOf(Type.cave);
        if (landingFloor != null)
        {
            cameraResultMove.SetTarget(landingFloor.transform.position);
            cameraResultMove.Move();
        }
    }
}
