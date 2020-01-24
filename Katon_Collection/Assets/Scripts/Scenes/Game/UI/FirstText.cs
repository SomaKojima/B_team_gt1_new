using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstText : MonoBehaviour
{
    [SerializeField]
    Text firstText;

    bool isFirstBuild = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFirstBuilding(bool _isFirstBuild)
    {
        isFirstBuild = _isFirstBuild;
    }

    public void Active()
    {
        if (isFirstBuild) return;
        firstText.gameObject.SetActive(true);
    }

    public void UnActive()
    {
        firstText.gameObject.SetActive(false);
    }
}
