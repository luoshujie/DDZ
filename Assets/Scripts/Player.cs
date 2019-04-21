using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    /// <summary>
    /// 玩家姓名
    /// </summary>
    public string ID;
    /// <summary>
    /// 持有的卡牌
    /// </summary>
    public List<GameObject> Poke;
    /// <summary>
    /// 是否轮到当前玩家出牌
    /// </summary>
    public bool IsOnThis;
    /// <summary>
    /// 玩家角色类型
    /// </summary>
    public string GameType="NongMin";
    /// <summary>
    /// 玩家的操作UI
    /// </summary>
    public UIManager MyUIManager;
    /// <summary>
    /// 抢地主的索引
    /// </summary>
    public int DiZhuIndex;

    public bool gittest;

    public void OnServerConnect()
    {

    }
    void Start () {
        Poke = new List<GameObject>();
        gittest = false;
	}
    /// <summary>
    /// 出牌
    /// </summary>
	public void OutCard()
    {
        if (!IsOnThis) return;
        List<PokeValue> card = new List<PokeValue>();
        for (int i = 0; i < Poke.Count; i++)
        {
            if (this.Poke[i].GetComponent<PokeValue>().IsSelect)
            {
                card.Add(Poke[i].GetComponent<PokeValue>());
            }
        }
        Debug.Log(card.Count);
        for (int i = 0; i < card.Count; i++)
        {
            card[i].AddNum(card);
            card[i].Account = ID;
        }
        if(GameManager.gameManager.ComparerPokeValue(card))//可以出牌
        {
            //将要出的牌赋值给游戏管理系统里面的上家牌
            int cardslen = GameManager.gameManager.UpAccountPoke.Count;
            for (int i = 0; i < cardslen; i++)
            {
                Destroy(GameManager.gameManager.UpAccountPoke[i].gameObject);
            }
            GameManager.gameManager.UpAccountPoke.Clear();
            GameManager.gameManager.UpAccountPoke = card;
            //实例化到桌面
            for (int i = 0; i < card.Count; i++)
            {
                //card[i].transform.position = new Vector3(688+70*i,384,0);
                card[i].transform.parent = GameManager.gameManager.OutCardParent;
            }
            GameManager.gameManager.OutCardParent.GetComponent<OutCardPosition>().CardPosition(card);
            //删除手中要出的牌
            for (int i = 0; i < card.Count; i++)
            {
                card[i].GetComponent<PokeValue>().IsOut = true;
                Poke.Remove(card[i].gameObject);
            }
            
            //关闭本家出牌状态
            IsOnThis = false;
            OnInstanceCard();
            //提示下一家出牌
            MyUIManager.CPUIParent.SetActive(false);
            if(Poke.Count<=0)
            {
                //该玩家胜利
                Debug.Log(this.name + "IsWin");
                return;
            }
            
            GameManager.gameManager.NextPlayerOnCard();
            
        }
        else
        {
            Debug.Log("不能出牌");
        }
    }
    /// <summary>
    /// 不出牌
    /// </summary>
	public void NotCard()
    {
        for (int i = 0; i < Poke.Count; i++)
        {
            if(Poke[i].GetComponent<PokeValue>().IsSelect==true)
            {
                Poke[i].transform.position -= transform.up * 10f;
                Poke[i].GetComponent<PokeValue>().IsSelect = false;
            }
        }
        //关闭本家出牌状态
        IsOnThis = false;
        //提示下一家出牌
        GameManager.gameManager.NextPlayerOnCard();
    }
    public void AddDZ()
    {
        if (DiZhuIndex == -1)
        {
            GameManager.gameManager.NextPlayerQDZ();
            return;
        }
        DiZhuIndex++;
        if(DiZhuIndex>=2)
        {
            GameManager.gameManager.DiZhu(this);
            return;
        }
        GameManager.gameManager.NextPlayerQDZ();
    }
    /// <summary>
    /// 实例化持有的卡牌
    /// </summary>
    public void OnInstanceCard()
    {
        for (int i = 0; i < Poke.Count; i++)
        {
            Poke[i].GetComponent<PokeValue>().IsSelect = false;
            //Poke[i].transform.localScale = new Vector3(90, 90, 1);
            Poke[i].transform.position = new Vector3((i+2.5f)*60, transform.position.y, 0);
            
            Poke[i].transform.parent = transform;
            Poke[i].GetComponent<SpriteRenderer>().sortingOrder = i;
        }
        
    }

}
