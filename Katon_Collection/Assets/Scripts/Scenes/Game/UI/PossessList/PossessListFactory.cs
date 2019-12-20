using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessListFactory : MonoBehaviour
{
    // リストのプレファブ
    [SerializeField]
    private GameObject listPrefab = null;

    // プレファブの親オブジェクト
    [SerializeField]
    private Transform listParent = null;

    // リスト生成
    public UI_Log Create()
    {
        // リストの実体化
        GameObject obj = Instantiate(listPrefab, listParent);

        UI_Log log = obj.GetComponent<UI_Log>();

        return log;
    }
}
