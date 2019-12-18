using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_Floor_Result : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Floor_Result Create()
    {
        GameObject instance = Instantiate(prefab);
        instance.transform.SetParent(parent, false);
        Floor_Result floor_result = instance.GetComponent<Floor_Result>();
        return floor_result;
    }
}
