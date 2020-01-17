using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_MainGame : MonoBehaviour
{
    [SerializeField]
    AudioSource auditoSource;

    [SerializeField, EnumListLabel(typeof(SoundType_MainGame))]
    AudioClip[] audioClips;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(SoundType_MainGame _type)
    {
        auditoSource.PlayOneShot(audioClips[(int)_type]);
    }
}
