using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_RoomNameButton : MonoBehaviour
{
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UI_Button_RoomName Create(string name)
    {
        GameObject instance = Instantiate(prefab);
        instance.transform.SetParent(parent);
        UI_Button_RoomName roomName = instance.GetComponent<UI_Button_RoomName>();
        roomName.Initialize(name);

        return roomName;
    }
}
