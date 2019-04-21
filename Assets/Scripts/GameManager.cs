using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    /// <summary>
    /// 扑克牌排序类,按点数大小排序
    /// </summary>
    public class PokeSortOnValue : IComparer<PokeValue>
    {
        public int Compare(PokeValue x, PokeValue y)//升序
        {
            return (x.Value.CompareTo(y.Value)*2+x.Colors.CompareTo(y.Colors));
        }
    }
    /// <summary>
    /// 扑克牌排序类,按卡牌张数排序
    /// </summary>
    public class PokeSortOnNum : IComparer<PokeValue>
    {
        public int Compare(PokeValue x, PokeValue y)//降序
        {
            
            return (- x.NumIndex.CompareTo(y.NumIndex)*3-x.Value.CompareTo(y.Value)*2+x.Colors.CompareTo(y.Colors));            
        }
    }
    public static GameManager gameManager;
    /// <summary>
    /// 扑克牌预制体
    /// </summary>
    public List<GameObject> PokePrefabs=new List<GameObject> ();
    /// <summary>
    /// 本局要发的扑克牌
    /// </summary>
    private List<GameObject> ThisPoke = new List<GameObject>();
    /// <summary>
    /// 上一家出的牌
    /// </summary>
    public List<PokeValue> UpAccountPoke = new List<PokeValue>();
    /// <summary>
    /// 当前所有玩家
    /// </summary>
    public Player[] players = new Player[3];
    /// <summary>
    /// 出牌玩家索引
    /// </summary>
    public int PlayerIndex;
    public Transform OutCardParent;
    private void Awake()
    {
        gameManager = this;
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                PokePrefabs[i*4+ j].GetComponent<PokeValue>().Value = 3+i;
                PokePrefabs[i * 4 + j].GetComponent<PokeValue>().Colors = j + 1;
            }
        }
        PokePrefabs[52].GetComponent<PokeValue>().Value = 16;
        PokePrefabs[52].GetComponent<PokeValue>().Colors = 1;
        PokePrefabs[53].GetComponent<PokeValue>().Value = 16;
        PokePrefabs[53].GetComponent<PokeValue>().Colors = 2;
    }
    private void Start()
    {
        //有三个人的时候才开始发牌
        Invoke("OnStartOutCard", 0.5f);
        //抢地主
        //将抢到地主的玩家角色改为“DiZhu”
        //执行DiZhu()操作
    }
    public void OnStartOutCard()
    {
        //有三个人的时候才开始发牌
        //发牌
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DiZhuIndex = 0;
            players[i].Poke = AddPoke(players[i].ID);
            players[i].OnInstanceCard();
        }
        PlayerIndex = Random.Range(0, players.Length);
        Invoke("OpenQDZUI", 0.2f);
        
    }
    /// <summary>
    /// 打开第一个抢地主的玩家的UI
    /// </summary>
    public void OpenQDZUI()
    {
        
        players[PlayerIndex].MyUIManager.QDZUIParent.SetActive(true);
    }
    /// <summary>
    /// 抢到地主的操作
    /// </summary>
    public void DiZhu(Player dizhu)
    {
        dizhu.GameType = "DiZhu";
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].GameType=="DiZhu")
            {
                //将剩下三张牌给地主
                for (int j = 0; j < ThisPoke.Count; j++)
                {
                    players[i].Poke.Add(ThisPoke[j].gameObject);
                }
                //地主牌排序
                players[i].Poke = SortPokeOnPlayer(players[i].Poke);
                ThisPoke.Clear();
                //地主可以出牌
                players[i].IsOnThis = true;
                PlayerIndex = i;
                players[i].OnInstanceCard();
                players[i].MyUIManager.CloseBCBtn(false);
                players[i].MyUIManager.CPUIParent.SetActive(true);
                break;
            }
        }
    }
    /// <summary>
    /// 复制一副新牌
    /// </summary>
    /// <returns></returns>
	public List<GameObject> ReturnPoke()
    {
        List<GameObject> newPoke = new List<GameObject>();
        for (int i = 0; i < PokePrefabs .Count; i++)
        {
            newPoke.Add(Instantiate(PokePrefabs[i]));
        }
        return newPoke;
    }
    /// <summary>
    /// 发牌，每次返回17张牌
    /// </summary>
	public List<GameObject> AddPoke(string ID)
    {
        List<GameObject> MyPoke = new List<GameObject>();
        if(ThisPoke.Count<=0||ThisPoke==null)
        {
            ThisPoke = ReturnPoke();
        }
        int PokeNum=0;
        for (int i = 0; i < 17; i++)
        {
            PokeNum = Random.Range(0,ThisPoke.Count);
            MyPoke.Add(ThisPoke[PokeNum]);
            //持有该卡牌的玩家
            MyPoke[MyPoke.Count - 1].GetComponent<PokeValue>().Account = ID;
            ThisPoke.RemoveAt(PokeNum);
        }
        MyPoke=SortPokeOnPlayer(MyPoke);
        return MyPoke;
    }
    /// <summary>
    /// 出牌排序
    /// </summary>
    /// <param name="poke"></param>
    /// <returns></returns>
    public List<PokeValue> SortPoke(List<PokeValue> poke)
    {
        for (int i = 0; i < poke.Count-2; i++)
        {
            if(poke[i].Value==poke[i+1].Value)
            {
                if(poke[i+1].Value==poke[i+2].Value)
                {
                    poke.Sort(new PokeSortOnNum());
                    return poke;
                }
            }
        }
        for (int i = 0; i < poke.Count-2; i++)
        {
            if(poke[i].Value-poke[i+2].Value==-1)
            {
                if(i+2==poke.Count-3)
                {
                    return poke;
                }
            }
        }
        return poke;
    }
    /// <summary>
    /// 发完卡牌后进行的排序
    /// </summary>
    /// <param name="poke"></param>
    /// <returns></returns>
	public List<GameObject> SortPokeOnPlayer(List<GameObject>poke)
    {
        List<PokeValue> newPoke = new List<PokeValue>();
        for (int i = 0; i < poke.Count; i++)
        {
            newPoke.Add(poke[i].GetComponent<PokeValue>());
        }
        newPoke.Sort(new PokeSortOnValue());
        poke.Clear();
        for (int i = 0; i < newPoke.Count; i++)
        {
            poke.Add(newPoke[i].gameObject);
        }
        return poke;
    }
    /// <summary>
    /// 判断牌型
    /// </summary>
    /// <param name="poke"></param>
    /// <returns></returns>
    public PokeType.PokeSType SwitchPokeType(List<PokeValue>poke)
    {
        PokeType.PokeSType pokeSType = PokeType.PokeSType.Empty;
        poke = SortPoke(poke);
        switch(poke.Count)
        {
            case 1: pokeSType=PokeType.pokeType.IsOnePoke(poke);break;
            case 2: pokeSType = PokeType.pokeType.IsTwoPoke(poke); break;
            case 3: pokeSType = PokeType.pokeType.IsThreePoke(poke); break;
            case 4: pokeSType = PokeType.pokeType.IsFourPoke(poke); break;
            case 5: pokeSType = PokeType.pokeType.IsFivePoke(poke); break;
            case 6: pokeSType = PokeType.pokeType.IsSixPoke(poke); break;
            case 7: pokeSType = PokeType.pokeType.IsSevenPoke(poke); break;
            case 8: pokeSType = PokeType.pokeType.IsEightPoke(poke); break;
            case 9: pokeSType = PokeType.pokeType.IsNinePoke(poke); break;
            case 10: pokeSType = PokeType.pokeType.IsTenPoke(poke); break;
            case 11: pokeSType = PokeType.pokeType.IsElevenPoke(poke); break;
            case 12: pokeSType = PokeType.pokeType.IsTwelvePoke(poke); break;
            case 13: pokeSType = PokeType.pokeType.IsThirteenPoke(poke); break;
            case 14: pokeSType = PokeType.pokeType.IsFourteenPoke(poke); break;
            case 15: pokeSType = PokeType.pokeType.IsFifteenPoke(poke); break;
            case 16: pokeSType = PokeType.pokeType.IsSixteenPoke(poke); break;
            case 17: pokeSType = PokeType.pokeType.IsSeventeenPoke(poke); break;
            case 18: pokeSType = PokeType.pokeType.IsEighteenPoke(poke); break;
            case 19: pokeSType = PokeType.pokeType.IsNineteenPoke(poke); break;
            case 20: pokeSType = PokeType.pokeType.IsTwentyPoke(poke); break;
        }
        return pokeSType;
    }
    /// <summary>
    /// 判断可否出牌
    /// </summary>
    /// <param name="UpAccountPoke"></param>
    /// <param name="DownAccountPoke"></param>
    /// <returns></returns>
    public bool ComparerPokeValue(List<PokeValue>DownAccountPoke)
    {
        //与上一家的卡牌对比，如果比上家大就可以出
        //或者如果上家出牌为自己，则可以出
        //或者上家出牌数量为0则可以出
        if (SwitchPokeType(DownAccountPoke) == PokeType.PokeSType.Empty)
        {
            Debug.Log("牌型不对");
            return false;
        }
        if (UpAccountPoke.Count <= 0)return true;
        
        if (UpAccountPoke[0].Account == DownAccountPoke[0].Account)return true;

        if(SwitchPokeType(UpAccountPoke)==SwitchPokeType(DownAccountPoke)&&UpAccountPoke.Count==DownAccountPoke.Count)
        {
            if(DownAccountPoke[0].Value>UpAccountPoke[0].Value)
            {
                return true;
            }
        }
        else
        {
            if(SwitchPokeType(DownAccountPoke)==PokeType.PokeSType.IsBoom)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 下一家玩家出牌状态
    /// </summary>
    public void NextPlayerOnCard()
    {
        PlayerIndex++;
        if (PlayerIndex >= players.Length)
        {
            PlayerIndex = 0;
        }
        if (UpAccountPoke[0].Account==players[PlayerIndex].ID)
        {
            players[PlayerIndex].MyUIManager.CloseBCBtn(false);
        }
        else
        {
            players[PlayerIndex].MyUIManager.CloseBCBtn(true);
        }
        players[PlayerIndex].IsOnThis = true;
        players[PlayerIndex].MyUIManager.CPUIParent.SetActive(true);
        
    }
    /// <summary>
    /// 抢地主UI开启
    /// </summary>
    public void NextPlayerQDZ()
    {
        int index = 0;
        PlayerIndex++;
        if (PlayerIndex >= players.Length)
        {
            PlayerIndex = 0;
        }
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].DiZhuIndex==-1)
            {
                index++;
            }
            if(i==players.Length-1)
            {
                if(index==2)
                {
                    //就那个人是地主
                    for (int j = 0; j < players.Length; j++)
                    {
                        if (players[j].DiZhuIndex > 0)
                        {
                            DiZhu(players[j]);
                            return;
                        }
                    }
                }
                else if(index==3)
                {
                    //没人当地主，重开
                    for (int k = 0; k < players.Length; k++)
                    {
                        int q = players[k].Poke.Count;
                        for (int l = 0; l < q; l++)
                        {
                            Destroy(players[k].Poke[l].gameObject);
                        }
                        players[k].Poke.Clear();
                    }
                    for (int k = 0; k < ThisPoke.Count; k++)
                    {
                        Destroy(ThisPoke[k].gameObject);
                    }
                    ThisPoke.Clear();
                    OnStartOutCard();
                }
            }
        }
        while(players[PlayerIndex].DiZhuIndex==-1)
        {
            PlayerIndex++;
            if (PlayerIndex >= players.Length)
            {
                PlayerIndex = 0;
            }
        }
        players[PlayerIndex].MyUIManager.QDZUIParent.SetActive(true);
    }
}
