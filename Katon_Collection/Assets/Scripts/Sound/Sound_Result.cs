using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Result : MonoBehaviour
{
    [SerializeField]
    AudioSource auditoSource;

    [SerializeField]
    AudioSource bgmAuditoSource;

    [SerializeField, EnumListLabel(typeof(SoundType_Result))]
    AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlaySound(SoundType_Result _type, float volumeScale)
    {
        if (SoundType_Result.BGM == _type || SoundType_Result.WinBGM == _type)
        {
            bgmAuditoSource.Stop();
            bgmAuditoSource.PlayOneShot(audioClips[(int)_type], volumeScale);
        }
        else
        {
            auditoSource.PlayOneShot(audioClips[(int)_type], volumeScale);
        }
        
    }
}
