using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    bool isLanding = false; // 着地したかどうか

    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        if (other.gameObject.tag == "Floor")
        {
            isLanding = true;
        }
    }

    public bool IsLanding()
    {
        return isLanding;
    }
}
