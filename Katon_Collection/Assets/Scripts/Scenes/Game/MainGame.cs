using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    QR_ReaderWindow qrReaderWindow;

    [SerializeField]
    Manager_PlaceBar manager_placeBar;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (manager_placeBar.GetIsQRLeader())
        {
            qrReaderWindow.Initialize();
        }
    }
}
