using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconMove : MonoBehaviour
{
    const float SPEED = 0.3f;
    const float START_TRANSPARENT_TIME = 0.4f;
    ITEM_TYPE type = ITEM_TYPE.WOOD;

    [SerializeField]
    ItemContextTable itemContextTable;

    Vector3 velocity = Vector3.zero;
    Vector3 startLocalPosition = Vector3.zero;

    Renderer renderer;

    float time = 0.0f;

    float alpha = 1.0f;
    float during = 0.2f;
    float subAlpha = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        velocity = new Vector3(0, SPEED, 0);
        subAlpha = 1.0f / during;
        startLocalPosition = gameObject.transform.localPosition;

        if (renderer.material.HasProperty("_Color"))
        {
            Color color = renderer.material.GetColor("_Color");
            color.a = 0.0f;
            renderer.material.SetColor("_Color", color);
            time = 0;
        }
    }

    public void Initialize(ITEM_TYPE _type)
    {
        type = _type;
        ChangeMaterial(type);
        gameObject.transform.localPosition = startLocalPosition;
        alpha = 1.0f;
        if (renderer.material.HasProperty("_Color"))
        {
            Color color = renderer.material.GetColor("_Color");
            color.a = 1.0f;
            renderer.material.SetColor("_Color", color);
            time = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += velocity;

        //if (Input.GetMouseButtonDown(0))
        //{
        //    switch (type)
        //    {
        //        case ITEM_TYPE.WOOD:
        //            type = ITEM_TYPE.ORE;
        //            break;
        //        case ITEM_TYPE.ORE:
        //            type = ITEM_TYPE.PARTS;
        //            break;
        //        case ITEM_TYPE.PARTS:
        //            type = ITEM_TYPE.WOOD;
        //            break;
        //    }
        //    Initialize(type);
        //}

        if (time > START_TRANSPARENT_TIME)
        {
            UpdateAlpha();
        }
        time += Time.deltaTime;
    }

    void UpdateAlpha()
    {
        alpha -= subAlpha * Time.deltaTime;
        if (alpha < 0)
        {
            alpha = 0;
        }

        if (renderer.material.HasProperty("_Color"))
        {
            Color color = renderer.material.GetColor("_Color");
            color.a = alpha;
            renderer.material.SetColor("_Color", color);
        }
    }

    void ChangeMaterial(ITEM_TYPE _type)
    {
        renderer.material = itemContextTable.GetItemContex(_type).GetMaterial();
    }
}
