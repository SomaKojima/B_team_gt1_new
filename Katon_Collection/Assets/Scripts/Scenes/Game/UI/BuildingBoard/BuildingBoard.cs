using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBoard : MonoBehaviour
{
    // 建築ボタンを押したときの状態
    enum MODE
    {
        NONE = -1,
        ONE,        // 建築ボタン一回目
        TWO,        // 建築ボタン二回目

        MAX
    }

    // 建築ボタン
    [SerializeField]
    UI_Button button;

    // 素材の項目
    [SerializeField]
    Owner_BuildingItemUnit owner_BuildingItemUnit;

    // 素材が足らないメッセージウィンドウ
    [SerializeField]
    GameObject missMessage;

    // 素材を表示させるボード
    [SerializeField]
    GameObject board;

    // 建築するかどうかを判定
    bool isClickBuildingButton = false;

    // メッセージウィンドウを表示させる時間関係
    float missFrame = 0;
    float missDuringFrame = 1.0f;
    
    // 現在の状態
    MODE mode = MODE.NONE;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize()
    {
        owner_BuildingItemUnit.Initialize();
        missMessage.SetActive(false);
        mode = MODE.ONE;
        board.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        isClickBuildingButton = false;
        // 建築ボタンをクリック
        if (button.IsClick())
        {
            Debug.Log("a");
            button.OnClickProcess();
            UpdateMode();
            mode = mode + 1;
            if (mode == MODE.MAX) mode = MODE.MAX - 1;
        }
        // ボタン以外をクリックした場合
        else if (Input.GetMouseButtonDown(0))
        {
            missMessage.SetActive(false);
            mode = MODE.ONE;
            board.SetActive(false);
        }

        // 素材が足らないウィンドウの処理
        if (missMessage.activeSelf)
        {
            missFrame += Time.deltaTime;
            if(missFrame > missDuringFrame)
            {
                missFrame = 0;
                missMessage.SetActive(false);
            }
        }
    }

    // 建築ボードを表示する
    public void Active(List<IItem> _items)
    {
        if (this.gameObject.activeSelf) return;
        this.gameObject.SetActive(true);
        Initialize();
        owner_BuildingItemUnit.SetUnits(_items);
    }

    // 建築ボードを非表示する
    public void UnActive()
    {
        if (!this.gameObject.activeSelf) return;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// モードごとの更新処理
    /// </summary>
    void UpdateMode()
    {
        switch (mode)
        {
            case MODE.ONE:
                if(!board.activeSelf)
                board.SetActive(true);
                break;
            case MODE.TWO:
                isClickBuildingButton = true;
                break;
        }
    }

    // 建築するかどうかを判定
    public bool IsClickBuildingButton()
    {
        return isClickBuildingButton;
    }

    // 素材が足らないしたウィンドウを出す
    public void ActiveMissMessage()
    {
        missMessage.SetActive(true);
    }
}
