using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_CloudEffect : MonoBehaviour
{


    [SerializeField]
    TransitionImage m_transitionImage = null;

    [SerializeField]
    List<Fade_Cloud> clouds = new List<Fade_Cloud>();

    bool isFirstProcess = true;
    bool isIn = false;

      
    //フェードインする関数
    IEnumerator FadeIn()
    {
        if (!isIn)
        {
            isIn = true;
            isFirstProcess = true;
        }
        if (isFirstProcess)
        {
            StartCoroutine(m_transitionImage.TransitionIn());
            isFirstProcess = false;
        }


        foreach (Fade_Cloud cloud in clouds)
        {
            cloud.Move(isIn);
        }

        yield return new WaitForEndOfFrame();
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    //フェードアウトする関数
    IEnumerator FadeOut()
    {

        if (isIn)
        {
            isIn = false;
            isFirstProcess = true;
        }
        if (isFirstProcess)
        {
            StartCoroutine(m_transitionImage.TransitionOut());
            isFirstProcess = false;
        }

       foreach (Fade_Cloud cloud in clouds)
        {
            cloud.Move(isIn);
        }


        yield return new WaitForEndOfFrame();
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }


    public bool GetIsProcess
    {
        get{ return m_transitionImage.IsProcess; }
    }
    
   
}
