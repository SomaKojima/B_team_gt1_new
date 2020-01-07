using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner_SignBoard : MonoBehaviour
{
    [SerializeField]
    List<SignBoard> signBoards = new List<SignBoard>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public List<SignBoard> GetSignBoards()
    {
        return signBoards;
    }
}
