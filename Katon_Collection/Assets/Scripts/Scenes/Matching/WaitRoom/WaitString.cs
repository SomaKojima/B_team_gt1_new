using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitString : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    float during = 1.0f;

    float time = 0.0f;
    int count = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > during)
        {
            time = 0.0f;
            count++;
            if (count > 3)
            {
                count = 0;
            }
        }

        text.text = "WAIT";
        for (int i = 0; i < count; i++)
        {
            text.text += ".";
        }
    }
}
