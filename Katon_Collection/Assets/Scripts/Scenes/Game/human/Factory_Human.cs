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

    public Human Create(Vector3 position, ITEM_TYPE type, Type _placeType)
    {
        GameObject instance = Instantiate(prefab);
        instance.transform.SetParent(parent);
        instance.transform.position = position;

        Human human = instance.GetComponent<Human>();

        human.Initialize(type, _placeType);

        return human;
    }

    public Human CreateRandomPosition(Vector3 position, float width, float height, float depth, ITEM_TYPE type, Type placeType)
    {

        // 座標はランダム
        float x = Random.Range(position.x - (width * 0.5f), position.x + (width * 0.5f));
        float y = Random.Range(position.y - (height * 0.5f), position.y + (height * 0.5f));
        float z = Random.Range(position.z - (depth * 0.5f), position.z + (depth * 0.5f));

        Human human = Create(new Vector3(x, y, z), type, placeType);

        return human;
    }
}
