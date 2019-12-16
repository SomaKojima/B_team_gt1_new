using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostName : MonoBehaviour
{
    [SerializeField]
    public string name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Inititalize(string _name)
    {
        name = _name;
    }
}
