using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResultMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 target;

    private Vector3 vec = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(Vector3 target)
    {
        this.gameObject.transform.LookAt(target);
        this.gameObject.transform.Translate(0.0f, 0.71f, 0.0f);
      
    }

    public void Initialize(Vector3 createPosion)
    {

    }
}
