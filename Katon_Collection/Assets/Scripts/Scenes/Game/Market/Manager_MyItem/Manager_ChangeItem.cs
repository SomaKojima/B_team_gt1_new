using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_ChangeItem : MonoBehaviour
{
    // 交換するアイテムのリスト
    public List<CommonUnitIcon> myItems;

    // アイテム追加アイコンのブレハブ
    [SerializeField]
    GameObject addItemPrefab = null;
    // プレハブを生成する場所(親オブジェクト)
    [SerializeField]
    Transform addPrefabParent = null;
    // アイテム無しのブレハブ
    [SerializeField]
    GameObject enptyItemPrefab = null;
    // プレハブを生成する場所(親オブジェクト)
    [SerializeField]
    Transform enptyPrefabParent = null;

    // 数量調整ウィンドウのブレハブ
    [SerializeField]
    GameObject countingItemPrefab = null;
    // プレハブを生成する場所(親オブジェクト)
    [SerializeField]
    Transform countingItemPrefabParent = null;

    // 交換するアイテムの種類の最大数
    public static int MAX_CHANGE_ITEM = 12;

    // 交換するアイテムの合計数
    private int totalItemCount = 0;
    // 交換するアイテムの合計数(テキスト)
    [SerializeField]
    Text totalItemCountText = null;

    public enum MENU_STATE
    {
        SELECT_NONE,        // 以下のどれでもない
        SELECT_GET_ITEM,    // 欲しいアイテムを選択(Common)
        SELECT_ADD_ITEM,    // アイテム追加アイコンを選択
        SELECT_COUNT_ITEM   // アイテムの数量を変更中
    }

    // 現行の状態
    private MENU_STATE menuState = MENU_STATE.SELECT_NONE;

    // 数量調整オブジェクトが入る
    private GameObject countingObj = null;

    // Rayが当たったオブジェクトの情報を入れる箱
    private RaycastHit hit;
    // Rayの飛ばせる距離
    private float rayDistance = 200.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // クリックしたとき
        if (Input.GetMouseButtonDown(0))
        {
            GameObject checkObj = CheckClickObject();
            if (checkObj == null) return;

            if (checkObj.tag == "AddChangeItem")
            {
                menuState = MENU_STATE.SELECT_ADD_ITEM;
                Debug.Log("aa");
            }

            // 交換するアイテムを追加中でなく既にアイテムの数量を調整中でなく、交換するアイテムを選択したら
            if ((menuState == MENU_STATE.SELECT_NONE || menuState == MENU_STATE.SELECT_GET_ITEM) &&
                (countingObj == null && checkObj.tag == "CountingItem"))
            {
                // 状態を数量調整中に
                menuState = MENU_STATE.SELECT_COUNT_ITEM;
                // 数量調整ウィンドウ生成
                countingObj = Instantiate(countingItemPrefab, countingItemPrefabParent);
                // スプライトを選択したアイテムに変更
                countingObj.transform.FindChild("UnitIcon").transform.FindChild("IconImage").GetComponent<Image>().sprite =
                    checkObj.transform.FindChild("icon").GetComponent<Image>().sprite;
                // テキストを選択したアイテムの個数分に変更
                countingObj.transform.FindChild("UnitIcon").transform.FindChild("IconCount").GetComponent<Text>().text =
                    checkObj.transform.FindChild("num").GetComponent<Text>().text;
                // スライダーの最大値設定、現在の量に設定
                countingObj.transform.FindChild("Slider").GetComponent<Slider>().maxValue = 50;
                countingObj.transform.FindChild("Slider").GetComponent<Slider>().value =
                    int.Parse(checkObj.transform.FindChild("num").GetComponent<Text>().text);
                Debug.Log("CountingItem");
            }

            // 増減ボタンの数量調整
            if (menuState == MENU_STATE.SELECT_COUNT_ITEM && checkObj.tag == "CountUp")
            {
                int sliderValue = (int)countingObj.transform.FindChild("Slider").GetComponent<Slider>().value;
                sliderValue++;
                if (sliderValue > countingObj.transform.FindChild("Slider").GetComponent<Slider>().maxValue)
                {
                    sliderValue = (int)countingObj.transform.FindChild("Slider").GetComponent<Slider>().maxValue;
                }

                countingObj.transform.FindChild("UnitIcon").transform.FindChild("IconCount").GetComponent<Text>().text =
                    sliderValue.ToString();
                countingObj.transform.FindChild("Slider").GetComponent<Slider>().value = sliderValue;
            }
            else if(menuState == MENU_STATE.SELECT_COUNT_ITEM && checkObj.tag == "CountDown")
            {
                int sliderValue = (int)countingObj.transform.FindChild("Slider").GetComponent<Slider>().value;
                sliderValue--;
                if (sliderValue < 0) sliderValue = 0;

                countingObj.transform.FindChild("UnitIcon").transform.FindChild("IconCount").GetComponent<Text>().text =
                    sliderValue.ToString();
                countingObj.transform.FindChild("Slider").GetComponent<Slider>().value = sliderValue;
            }

            // 数量調整ウィンドウの操作終了
            if (countingObj != null && checkObj.tag == "CountingItemApply")
            {
                menuState = MENU_STATE.SELECT_NONE;
                Destroy(countingObj);
                countingObj = null;
            }
        }

        // スライダーの数量調整
        if (menuState == MENU_STATE.SELECT_COUNT_ITEM)
        {
            countingObj.transform.FindChild("UnitIcon").transform.FindChild("IconCount").GetComponent<Text>().text =
                ((int)countingObj.transform.FindChild("Slider").GetComponent<Slider>().value).ToString();
        }
    }

    /// <summary>
    /// 交換するアイテムの残りを表示
    /// 交換するアイテムの種類が増減したらこの関数を呼ぶ
    /// </summary>
    public void LineupRemainItem()
    {
        if (myItems.Count < MAX_CHANGE_ITEM)
        {
            int difCount = MAX_CHANGE_ITEM - myItems.Count;

            for (int i = 0; i < difCount; i++)
            {
                // 初回は追加アイコンにする
                if (i == 0) Instantiate(addItemPrefab, addPrefabParent);
                // 残りの余りの枠をアイテム無しのアイコンで埋める
                else Instantiate(enptyItemPrefab, enptyPrefabParent);
            }
        }
    }

    /// <summary>
    /// 交換するアイテムの合計数を表示
    /// 交換するアイテムの個数が増減したらこの関数を呼ぶ
    /// </summary>
    public void DisplayTotalCount()
    {
        foreach (CommonUnitIcon cmnUntIcn in myItems)
        {
            totalItemCount += cmnUntIcn.GetCount();
        }

        totalItemCountText.text = "合計：" + totalItemCount + "個";
    }

    /// <summary>
    /// アイテムを増やす
    /// </summary>
    /// <param name="item">増やすボタン</param>
    public void Add(CommonUnitIcon item)
    {
        myItems.Add(item);
    }

    /// <summary>
    /// 交換するアイテムリストを取得
    /// </summary>
    /// <returns>ボタンのリスト</returns>
    public List<CommonUnitIcon> GetChangeItems()
    {
        return myItems;
    }

    /// <summary>
    /// クリックしたところのオブジェクトを調べる
    /// </summary>
    /// <returns>衝突したオブジェクト(衝突しなかった場合はnull)</returns>
    private GameObject CheckClickObject()
    {
        //　カメラからクリックした位置にレイを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // もしRayにオブジェクトが衝突したら
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // オブジェクトの名前を取得
            GameObject obj = hit.collider.gameObject;
            return obj;
        }
        return null;
    }


    public MENU_STATE MenuState { get { return menuState; } set { menuState = value; } }
}
