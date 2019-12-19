using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_GuestName : MonoBehaviour
{
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public RectTransform parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GuestName Create(string name)
    {
        GameObject instance = Instantiate(prefab);
        instance.transform.SetParent(parent,false);
        GuestName guestGame = instance.GetComponent<GuestName>();
        guestGame.Inititalize(name);
        return guestGame;
    }
}
