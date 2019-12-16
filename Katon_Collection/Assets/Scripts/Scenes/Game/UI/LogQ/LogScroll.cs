using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogScroll : MonoBehaviour
{
    [SerializeField]
    float speed = 0.1f;

    [SerializeField]
    RectTransform maskRect=null;

    [SerializeField]
    Scrollbar verticalScrollbar=null;

    [SerializeField]
    Scrollbar horizonScrollbar=null;

    [SerializeField]
    GameObject scrollArea=null;

    RectTransform rectTransform;
    Vector3 startPosition;
    int endChildCount = 0;

    bool isPointerDown = false;


    public RectTransform GetMask
    {
        get { return maskRect; }
    
    }


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = scrollArea.GetComponent<RectTransform>();
        startPosition = rectTransform.localPosition;
        endChildCount = scrollArea.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        ///----------------------------------------------
        /// 縦スクロール
        ///----------------------------------------------
        rectTransform.localPosition = new Vector3(
            rectTransform.localPosition.x,
            ScrollValue(rectTransform.sizeDelta.y, maskRect.sizeDelta.y, startPosition.y, verticalScrollbar.value),
            rectTransform.localPosition.z
            );

        // 自動的に一番新しいログに移動
        if (endChildCount != scrollArea.transform.childCount)
        {
            verticalScrollbar.value = 1.0f;
        }
        endChildCount = scrollArea.transform.childCount;


        if (Input.GetMouseButton(0) && isPointerDown)
        {
            verticalScrollbar.value += Input.GetAxis("Mouse Y") * speed;
            verticalScrollbar.value  = Mathf.Clamp(verticalScrollbar.value, 0.0f, 1.0f);
        }


        ///----------------------------------------------
        /// 横スクロール
        ///----------------------------------------------
        rectTransform.localPosition = new Vector3(
            ScrollValue(rectTransform.sizeDelta.x, maskRect.sizeDelta.x, startPosition.x, horizonScrollbar.value, true),
            rectTransform.localPosition.y,
            rectTransform.localPosition.z
            );

        if (Input.GetMouseButton(0) && isPointerDown)
        {
            horizonScrollbar.value += Input.GetAxis("Mouse X") * -speed;
            horizonScrollbar.value = Mathf.Clamp(horizonScrollbar.value, 0.0f, 1.0f);
        }
    }

    float ScrollValue(float size, float maskSize, float startPosition, float value, bool turn = false)
    {
        size -= maskSize;
        if (size < 0)
        {
            size = 0;
        }

        if (turn)
        {
            return startPosition - size * value;
        }
        return startPosition + size * value;
    }


    public void OnPointerDown()
    {
        isPointerDown = true;
    }

    public void OnPointerUp()
    {
        isPointerDown = false;
    }
}

