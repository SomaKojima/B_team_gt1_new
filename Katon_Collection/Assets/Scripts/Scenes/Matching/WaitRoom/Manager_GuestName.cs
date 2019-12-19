using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_GuestName : MonoBehaviour
{
    [SerializeField]
    public List<GuestName> names;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(GuestName guestName)
    {
        names.Add(guestName);
    }

    public void AllDelete()
    {
        foreach (GuestName name in names)
        {
            Destroy(name.gameObject);
        }
        names.Clear();
    }
}
