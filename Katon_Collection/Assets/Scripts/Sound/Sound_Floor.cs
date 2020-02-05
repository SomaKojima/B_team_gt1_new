using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Floor : MonoBehaviour
{
    [SerializeField]
    AudioSource auditoSource;

    [SerializeField, EnumListLabel(typeof(SoundType_Floor))]
    AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(SoundType_Floor _type, float volumeScale)
    {
        auditoSource.PlayOneShot(audioClips[(int)_type], volumeScale);
    }
}
