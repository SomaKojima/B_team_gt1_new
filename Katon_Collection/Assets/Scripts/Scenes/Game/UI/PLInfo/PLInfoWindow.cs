using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLInfoWindow : MonoBehaviour
{
    // マンションを管理
    [SerializeField]
    Text nameText;

    private SI_Player data;

    // 所持リスト管理オブジェクト
    List<GameObject> possessLists = new List<GameObject>();

    // 所持リスト管理オブジェクト
    List<GameObject> mansionLists = new List<GameObject>();

    // リスト内のユニットプレハブ
    [SerializeField]
    GameObject listUnitPrefab = null;

    // ユニットを生成する場所
    [SerializeField]
    Transform prefabPerent = null;

    [SerializeField]
    Transform prefabPerentMansion = null;

    // ItemManagerオブジェクト
    private Manager_Item itemManager = null;

    [SerializeField]
    ItemContextTable table;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateList();
    }

    public void Initialize()
    {
        //createList();
    }

    public void SetNameText(string name)
    {
        nameText.text = name;
    }

    public void DataSet(SI_Player data)
    {
        this.data = data;
    }

    public void createList()
    {
        for (int i = 0; i < (int)ITEM_TYPE.NUM; i++)
        {
            GameObject obj = Instantiate(listUnitPrefab, prefabPerent);
            obj.transform.Find("Icon").transform.Find("Icon").GetComponent<Image>().sprite =
                table.GetItemContex((ITEM_TYPE)i).GetSprite();
            obj.transform.Find("CountText").GetComponent<Text>().text =
                "×" + (data.GetItemCount(i)).ToString();

            possessLists.Add(obj);
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(listUnitPrefab, prefabPerentMansion);
            obj.transform.Find("Icon").transform.Find("Icon").GetComponent<Image>().sprite =
                table.GetItemContex((ITEM_TYPE)(i+ ItemType.HumanMax)).GetSprite();
            obj.transform.Find("CountText").GetComponent<Text>().text =
                "×" + (data.GetPlacePoint(i+(int)2)).ToString();
            //Debug.Log(data.GetPlacePoint(i + (int)2));
            mansionLists.Add(obj);
        }
    }

    public void SetItemData(Manager_Item itemManager)
    {
        this.itemManager = itemManager;
    }
}
