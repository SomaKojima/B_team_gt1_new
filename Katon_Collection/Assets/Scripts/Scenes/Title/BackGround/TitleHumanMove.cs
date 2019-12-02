using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleHumanMove : MonoBehaviour
{
    [SerializeField]
    Transform m_rightPosition;

    [SerializeField]
    Transform m_leftPosition;


    public float time;
    private Transform deltaPos;
    private float elapsedTime;
    private bool bStartToEnd = true;
    void Start()
    {
        // StartPosをオブジェクトに初期位置に設定
        transform.position = m_rightPosition.localPosition;
        // 1秒当たりの移動量を算出
        deltaPos.localPosition = (m_rightPosition.localPosition - m_leftPosition.localPosition) / time;
        elapsedTime = 0;
    }

    void Update()
    {
       
        this.transform.position += deltaPos * Time.deltaTime;
        // 往路復路反転用経過時間
        elapsedTime += Time.deltaTime;
        // 移動開始してからの経過時間がtimeを超えると往路復路反転
        if (elapsedTime > time)
        {
            if (bStartToEnd)
            {
                // StartPos→EndPosだったので反転してEndPos→StartPosにする
                // 現在の位置がEndPosなので StartPos - EndPosでEndPos→StartPosの移動量になる
                deltaPos = (StartPos - EndPos) / time;
                // 誤差があるとずれる可能性があるので念のためオブジェクトの位置をEndPosに設定
                transform.position = EndPos;
            }
            else
            {
                // EndPos→StartPosだったので反転してにStartPos→EndPosする
                // 現在の位置がStartPosなので EndPos - StartPosでStartPos→EndPosの移動量になる
                deltaPos = (EndPos - StartPos) / time;
                // 誤差があるとずれる可能性があるので念のためオブジェクトの位置をSrartPosに設定
                transform.position = StartPos;
            }
            // 往路復路のフラグ反転
            bStartToEnd = !bStartToEnd;
            // 往路復路反転用経過時間クリア
            elapsedTime = 0;
        }
    }
