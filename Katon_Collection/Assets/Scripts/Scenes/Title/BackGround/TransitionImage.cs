using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionImage : MonoBehaviour
{

    //フェードインするときのMaterial
    [SerializeField]
    private Material m_transitionOut;

    //フェードアウトするときのMaterial
    [SerializeField]
    private Material m_transitionIn;

    //フェード時間
    [SerializeField]
    private float m_fadeTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TransitionOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator Transition(bool isinTransition)
    //{
    //    if (isinTransition)
    //    {

    //    }
    //    return;
    //}

    //フェードイン
    public IEnumerator TransitionIn()
    {
        yield return Animate(m_transitionIn, m_fadeTime);

        yield return new WaitForEndOfFrame();
    }

    //フェードアウト
    public IEnumerator TransitionOut()
    {
        yield return Animate(m_transitionOut, m_fadeTime);

        yield return new WaitForEndOfFrame();
    }


    /// <summary>
    /// time秒かけてトランジションを行う
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Animate(Material material, float time)
    {
        GetComponent<Image>().material = material;
        float current = 0;
        while (current < time)
        {
            material.SetFloat("_Alpha", current / time);
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }
        material.SetFloat("_Alpha", 1);
    }
}
