using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_CommonUnitButton : MonoBehaviour
{
    const int HUMAN_NECESSARY = 100;

    [SerializeField]
    Factory_CommonUnitButton factory;
    [SerializeField]
    Manager_CommonUnitButton manager;

    List<IItem> humanGetItems = new List<IItem>();

    CommonUnitButton selectButton = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (CommonUnitButton button in manager.GetButtons())
        {
            if (button != selectButton && selectButton != null)
            {
                button.ActiveMask();
            }
            else
            {
                button.UnActiveMask();
            }
            if (button.IsClick())
            {
                button.OnClickProcess();
                button.UnActiveMask();
                selectButton = button;
            }
        }
    }

    public void Create(List<IItem> items, int requiredNum, bool isBr, bool isHuman)
    {
        manager.Add(factory.Create(items, requiredNum, isBr, isHuman));
    }

    public CommonUnitButton GetSelectCommonUnitButton()
    {
        return selectButton;
    }

    public void ClearSelect()
    {
        selectButton = null;
    }
}
