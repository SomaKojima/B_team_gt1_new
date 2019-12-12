using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory_CommonUnitButton : MonoBehaviour
{
    // Commonボタンのブレハブ
    [SerializeField]
    GameObject commonUnitPrefab = null;
    // プレハブを生成する場所(親オブジェクト)
    [SerializeField]
    Transform  prefabParent = null;

    // CommonUnitButtonオブジェクト
    [SerializeField]
    CommonUnitButton commonUnitButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CommonUnitButton Create(List<IItem> getItems, int requiredNum)
    {
        GameObject obj = Instantiate(commonUnitPrefab, prefabParent);

        CommonUnitButton cmnUnitBtn = new CommonUnitButton();
        cmnUnitBtn.Initialize(getItems, requiredNum);

        return cmnUnitBtn;
    }
}
