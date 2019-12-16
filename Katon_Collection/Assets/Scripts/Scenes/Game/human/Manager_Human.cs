using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Human : MonoBehaviour
{
    List<Human> humans = new List<Human>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(Human human)
    {
        humans.Add(human);
    }

    public List<Human> List
    {
        get { return humans; }
    }
}
