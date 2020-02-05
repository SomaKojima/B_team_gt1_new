using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    // サウンド
    [SerializeField]
    Sound_Floor sound;


    bool isLanding = false; // 着地したかどうか
    bool isBase = false;     // ベースかどうか
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (isBase)
        {
            if (other.gameObject.tag == "Ground")
            {
                // 着地音
                sound.PlaySound(SoundType_Floor.PileUp, 0.5f);
            }
            return;
        }
        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        if (other.gameObject.tag == "Floor")
        {
            // 着地音
            sound.PlaySound(SoundType_Floor.PileUp, 0.5f);
            isLanding = true;
        }
    }

    public bool IsLanding()
    {
        return isLanding;
    }

    public void InitializeBase()
    {
        isBase = true;
        isLanding = true;
    }

    public bool IsBase()
    {
        return isBase;
    }
}
