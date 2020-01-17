using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_CommonUnitButton : MonoBehaviour
{
    [SerializeField]
    Factory_CommonUnitButton factory;
    [SerializeField]
    Manager_CommonUnitButton manager;

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
            if (button.IsClick())
            {
                button.OnClickProcess();
                button.UnActiveMask();
                selectButton = button;
            }
        }
    }

    public void Create(List<IItem> items, int requiredNum)
    {
        manager.Add(factory.Create(items, requiredNum));
    }

    public CommonUnitButton GetSelectCommonUnitButton()
    {
        return selectButton;
    }
}
