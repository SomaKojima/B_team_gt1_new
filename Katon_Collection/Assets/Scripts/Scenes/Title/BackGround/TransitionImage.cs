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

    [SerializeField]
    Image image;

    //フェード時間
    [SerializeField]
    private float m_fadeTime = 1.0f;

    // 実行中かどうかのフラグ
    bool isExcuting = false;

    //フェードの処理を行うためのフラグ
    bool isProcess = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }


    //フェードイン
    public IEnumerator TransitionIn()
    {
        // 複数実行しない
        if (isExcuting) yield break;

        // 初期化
        isProcess = false;
        isExcuting = true;
        image.raycastTarget = true;

        // 実行中
        yield return Animate(m_transitionIn, m_fadeTime, true);

        isProcess = false;
        // フェードイン終了
        yield return new WaitForEndOfFrame();
    }

    //フェードアウト
    public IEnumerator TransitionOut()
    {
        // 複数実行しない
        if (isExcuting && !isProcess) yield break;


        // 初期化
        isProcess = false;
        isExcuting = true;
        image.raycastTarget = true;

        // 実行中
        yield return Animate(m_transitionOut, m_fadeTime, false);

        isProcess = false;
        // フェードアウト終了
        image.raycastTarget = false;
        isExcuting = false;

        yield return new WaitForEndOfFrame();
    }


    /// <summary>
    /// time秒かけてトランジションを行う
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Animate(Material material, float time, bool isIn)
    {
        image.material = material;
        float current = 0;

        while (current < time)
        {
            material.SetFloat("_Alpha", current / time);
            
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }
        if (isIn)
        {
            isProcess = true;
        }
        yield return new WaitForEndOfFrame();
    }

    //取得
    public bool IsProcess
    {
        get { return isProcess; }
    }

    public bool IsExcuting
    {
        get { return isExcuting; }
    }
}
