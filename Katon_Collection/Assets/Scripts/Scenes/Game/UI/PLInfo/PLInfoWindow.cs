using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLInfoWindow : MonoBehaviour
{
    // マンションを管理
    [SerializeField]
    Text nameText;

    private SI_Player data;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNameText(string name)
    {
        nameText.text = name;
    }

    public void DataSet(SI_Player data)
    {
        this.data = data;
    }

}
