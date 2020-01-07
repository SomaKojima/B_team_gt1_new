using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBoard : MonoBehaviour
{
    enum MODE
    {
        NONE = -1,
        ONE,
        TWO,

        MAX
    }
    [SerializeField]
    UI_Button button;

    [SerializeField]
    Owner_BuildingItemUnit owner_BuildingItemUnit;

    [SerializeField]
    GameObject missMessage;

    [SerializeField]
    GameObject board;

    bool isClickBuildingButton = false;

    float missFrame = 0;
    float missDuringFrame = 1.0f;

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
        if (button.IsClick())
        {
            button.OnClickProcess();
            UpdateMode();
            mode = mode + 1;
            if (mode == MODE.MAX) mode = MODE.MAX - 1;
        }

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

    public void Active(List<IItem> _items)
    {
        if (this.gameObject.activeSelf) return;
        this.gameObject.SetActive(true);
        Initialize();
        owner_BuildingItemUnit.SetUnits(_items);
    }

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

    public bool IsClickBuildingButton()
    {
        return isClickBuildingButton;
    }

    public void ActiveMissMessage()
    {
        missMessage.SetActive(true);
    }
}
