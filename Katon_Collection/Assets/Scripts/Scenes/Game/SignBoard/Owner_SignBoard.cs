using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_SignBoard : MonoBehaviour
{
    [SerializeField]
    List<SignBoard> signBoards = new List<SignBoard>();
    
    SignBoard clickSignBoasd = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clickSignBoasd = null;
        foreach (SignBoard signBoard in signBoards)
        {
            if (signBoard.IsClick())
            {
                //signBoard.gameObject.SetActive(false);
                clickSignBoasd = signBoard;
            }
        }
    }

    public bool IsBuilding()
    {
        if (clickSignBoasd == null) return false;
        return true;
    }

    public Type GetPlaceType()
    {
        if (clickSignBoasd == null) return Type.none;
        return clickSignBoasd.GetPlaceType();
    }
    
}
