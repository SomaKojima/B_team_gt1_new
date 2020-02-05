using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JudgeClickUI : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;

    PointerEventData pointer;

    // クリックの判定をするかどうか
    bool isClickProcess = false;
    bool isClick = false;


    public void Initialize()
    {
        isClickProcess = false;
        isClick = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        pointer = new PointerEventData(EventSystem.current);
    }

    // Update is called once per frame
    void Update()
    {
        string name = rectTransform.name;

        isClickProcess = false;
        isClick = false;
        if (IsClick(name))
        {
            isClick = true;
        }
    }


    bool IsClick(string _name)
    {
        if (!Input.GetMouseButtonDown(0)) return false;
        isClickProcess = true;
        List<RaycastResult> results = new List<RaycastResult>();
        // マウスポインタの位置にレイ飛ばし、ヒットしたものを保存
        pointer.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointer, results);
        // ヒットしたUIの名前
        foreach (RaycastResult target in results)
        {
            if (target.gameObject.name == _name) return true;
        }

        return false;
    }

    /// <summary>
    /// rectTransform以外の場所をクリックしたかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsClickOutSide()
    {
        return isClickProcess && !isClick;
    }

    /// <summary>
    /// rectTransformをクリックしたかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsClickInSizde()
    {
        return isClickProcess && isClick;
    }
}
