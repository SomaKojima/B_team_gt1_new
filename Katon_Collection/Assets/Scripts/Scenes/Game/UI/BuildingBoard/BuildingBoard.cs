using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBoard : MonoBehaviour
{
    [SerializeField]
    UI_Button button;

    [SerializeField]
    Owner_BuildingItemUnit owner_BuildingItemUnit;

    [SerializeField]
    GameObject missMessage;

    bool isClickBuildingButton = false;

    float missFrame = 0;
    float missDuringFrame = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize()
    {
        owner_BuildingItemUnit.Initialize();
        missMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        isClickBuildingButton = false;
        if (button.IsClick())
        {
            button.OnClickProcess();
            isClickBuildingButton = true;
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

    public bool IsClickBuildingButton()
    {
        return isClickBuildingButton;
    }

    public void ActiveMissMessage()
    {
        missMessage.SetActive(true);
    }
}
