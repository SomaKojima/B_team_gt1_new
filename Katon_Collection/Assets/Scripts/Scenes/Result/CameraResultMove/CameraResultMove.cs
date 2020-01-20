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

    public void Move()
    {
        Vector3 buf = Vector3.Lerp(this.gameObject.transform.position, target, 0.05f);
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, buf.y, this.gameObject.transform.position.z);
    }

    public void SetTarget(Vector3 _target)
    {
        target = _target;
    }

    public void Initialize(Vector3 createPosion)
    {

    }
}
