using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_Human : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Human Create(Vector3 position, ITEM_TYPE type)
    {
        GameObject instance = Instantiate(prefab);
        instance.transform.SetParent(parent);
        instance.transform.position = position;

        Human human = instance.GetComponent<Human>();

        human.Initialize(type);

        return human;
    }
}
