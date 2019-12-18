using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomName : MonoBehaviour
{
    [SerializeField]
    public Text name;

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
        name.text = _name;
    }
}
