using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Fukidashi : MonoBehaviour
{
    /*☆*☨*☆*★*☨*★*☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★*/
    //吹き出しのイメージ
    /*☆*☨*☆*★*☨*★*☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★*/
    [SerializeField]
    private Image ui_Fukidashi = null;

    /*☆*☨*☆*★*☨*★*☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★*/
    //場所指定
    /*☆*☨*☆*★*☨*★*☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★*/
    [SerializeField]
    private Vector3 offset;

    /*☆*☨*☆*★*☨*★*☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★*/
    //見切れた時のスプライト変更
    /*☆*☨*☆*★*☨*★*☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★*/
    [SerializeField]
    private Sprite sprite = null;

    /*☆*☨*☆*★*☨*★*☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★*/
    //建物のポジション取得
    /*☆*☨*☆*★*☨*★*☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★**☆*☨*☆*★*☨*★*/
    private Vector3 target;  


    //更新
    private void Update()
    {

        //表示オン
        ui_Fukidashi.enabled = true;

        
        //座標をワールドからスクリーンに変更
        ui_Fukidashi.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target + offset);

        //見切れそうになったらポジションを代入
        if (ui_Fukidashi.rectTransform.position.y > Screen.height ||
            ui_Fukidashi.rectTransform.position.y < 0)
        {


            //ポジションを固定する
            ui_Fukidashi.rectTransform.position = new Vector3(ui_Fukidashi.rectTransform.position.x,0+20,ui_Fukidashi.rectTransform.position.z);

            //スプライトを丸に変更
            ui_Fukidashi.sprite = sprite;

        }
       
    }

    //ポジションの取得
    public void SetTarget(Vector3 _target)
    {
        target = _target;
       
    }


}
