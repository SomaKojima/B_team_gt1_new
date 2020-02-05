using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_MatchingRoom : MonoBehaviour
{
    [SerializeField]
    AudioSource auditoSource;

    [SerializeField, EnumListLabel(typeof(SoundType_MatchingRoom))]
    AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(SoundType_MatchingRoom _type, float volumeScale)
    {
        auditoSource.PlayOneShot(audioClips[(int)_type], volumeScale);
    }
}
