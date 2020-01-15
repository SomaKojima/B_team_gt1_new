using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigid;

    [SerializeField]
    HingeJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        //joint.enablePreprocessing = false;
        //joint.enableCollision = false;

        //rigid.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
