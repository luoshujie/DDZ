using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    /// <summary>
    /// 操作UI的玩家脚本
    /// </summary>
    public Player player;
    /// <summary>
    /// 抢地主的按钮UI父对象
    /// </summary>
    public GameObject QDZUIParent;
    /// <summary>
    /// 出牌的按钮UI父对象
    /// </summary>
    public GameObject CPUIParent;
    public GameObject BC;
    public void CloseBCBtn(bool b)
    {
        BC.SetActive(b);
    }

    void Start()
    {
        player.MyUIManager = this;
        QDZUIParent.transform.Find("BtnQDZ").GetComponent<Button>().onClick.AddListener(() =>
        {
            player.AddDZ();
            QDZUIParent.SetActive(false);
        });
        CPUIParent.transform.Find("BtnCP").GetComponent<Button>().onClick.AddListener(()=> {
            player.OutCard();
            
        });
        QDZUIParent.transform.Find("BtnBQ").GetComponent<Button>().onClick.AddListener(() => {
            player.DiZhuIndex = -1;
            player.AddDZ();
            QDZUIParent.SetActive(false);
        });
        BC=CPUIParent.transform.Find("BtnBC").gameObject;
        BC.GetComponent<Button>().onClick.AddListener(()=> {
            player.NotCard();
            CPUIParent.SetActive(false);
        });
        CPUIParent.SetActive(false);
        QDZUIParent.SetActive(false);
    }
}
