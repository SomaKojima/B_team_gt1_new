using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLInfoManager : MonoBehaviour
{
    private GameObject[] PLInfoWindows;
    private PLInfoWindow[] PLInfoWindowComponets = null;
    private List<SI_Player> data = new List<SI_Player>();
    private int currentNumber = 0;
    [SerializeField]
    private Manager_Item itemManager = null;
    [SerializeField]
    private GameObject RightBtn;
    [SerializeField]
    private GameObject LeftBtn;
    [SerializeField]
    private GameObject playerWindows;
    private bool isActive = false;

    bool isNext = false;
    bool isBack = false;

    bool isWindowActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isNext = false;

        isBack = IsJudgeBack();

        isWindowActive = false;
        if (playerWindows.transform.childCount != 0)
        {
            isWindowActive = true;
        }
    }

    public void CreatePLInfoWindow()
    {
        //Prefabの読み込み
        GameObject prefab = (GameObject)Resources.Load("Prefabs/Scenes/Game/UI/PLInfo/PLInfoWindow");
        PLInfoWindows = new GameObject[data.Count];
        PLInfoWindowComponets = new PLInfoWindow[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            PLInfoWindows[i] = Instantiate(prefab, this.transform.position, Quaternion.identity);
            PLInfoWindows[i].transform.parent = playerWindows.transform;
            PLInfoWindows[i].transform.localScale = new Vector3(1, 1, 1);
            PLInfoWindows[i].GetComponent<PLInfoWindow>().SetNameText(data[i].name);
            PLInfoWindows[i].GetComponent<PLInfoWindow>().SetItemData(itemManager);
            PLInfoWindows[i].GetComponent<PLInfoWindow>().DataSet(data[i]);
            PLInfoWindows[i].GetComponent<PLInfoWindow>().createList();

            PLInfoWindowComponets[i] = PLInfoWindows[i].GetComponent<PLInfoWindow>();

            if (data[i].ID == PhotonNetwork.player.ID)
            {
                currentNumber = i;
            }
        }
        isActive = true;
        BtnSetActive(true);
        PLInfoActives();
    }

    public void SetPlInfo(List<SI_Player> data)
    {
        this.data = data;
    }

    public void PLInfoActives()
    {
        for (int i = 0; i < PLInfoWindows.Length; i++)
        {
            PLInfoWindows[i].SetActive(false);
        }
        PLInfoWindows[currentNumber].SetActive(true);
    }

    public void RightBtnOnClick()
    {
        if (currentNumber + 1 < PLInfoWindows.Length) 
        {
            currentNumber++;
            PLInfoActives();
            LeftBtn.SetActive(true);
            if (currentNumber + 1 == PLInfoWindows.Length)
            {
                RightBtn.SetActive(false);
            }
        }
        isNext = true;
    }

    public void LeftBtnOnClick()
    {
        if (currentNumber > 0)
        {
            currentNumber--;
            PLInfoActives();
            RightBtn.SetActive(true);
            if (currentNumber == 0)
            {
                LeftBtn.SetActive(false);
            }
        }
        isNext = true;
    }

    public void DeleteWindow()
    {
        isActive = false;
        BtnSetActive(false);
        currentNumber = 0;
        for (int i = 0; i < PLInfoWindows.Length; i++)
        {
            Destroy(PLInfoWindows[i]);
        }
        isWindowActive = false;
    }

    public bool GetWindowIsActive()
    {
        return isActive;
    }

    public void BtnSetActive(bool isActive)
    {
        RightBtn.SetActive(isActive);
        LeftBtn.SetActive(isActive);
        if(isActive)
        {
            if(currentNumber == 0)
            {
                LeftBtn.SetActive(false);
            }
            if(currentNumber+1== PLInfoWindows.Length)
            {
                RightBtn.SetActive(false);
            }
        }
    }

    public bool IsActive()
    {
        return isWindowActive;
    }

    public bool IsJudgeBack()
    {
        if (PLInfoWindowComponets == null) return false;
        if (PLInfoWindowComponets[currentNumber] == null) return false;
        if (PLInfoWindowComponets[currentNumber].IsClickOutSide())
        {
            if (isNext) return false;
            return true;
        }

        return false;
    }

    public bool IsBack()
    {
        return isBack;
    }
}
