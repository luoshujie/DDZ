using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeValue : MonoBehaviour {
    /// <summary>
    /// 卡牌的牌值
    /// </summary>
    public int Value;
    /// <summary>
    /// 卡牌的花色
    /// </summary>
    public int Colors;
    /// <summary>
    /// 持有该卡牌的玩家
    /// </summary>
    public string Account;
    /// <summary>
    /// 出牌是该类型卡牌的数量
    /// </summary>
    public int NumIndex;
    /// <summary>
    /// 是否被选中
    /// </summary>
    public bool IsSelect;
    /// <summary>
    /// 赋值卡牌值
    /// </summary>
    /// <param name="value"></param>
    /// <param name="colors"></param>
    
    public void AddValue(int value,int colors)
    {
        this.Value = value;
        this.Colors = colors;
    }
    /// <summary>
    /// 是否是已经打出的卡牌
    /// </summary>
    public bool IsOut;
    /// <summary>
    /// 判断该链表有多少张该类型的卡牌
    /// </summary>
    /// <param name="pokeValues"></param>
    public void AddNum(List<PokeValue> pokeValues)
    {
        for (int i = 0; i < pokeValues.Count; i++)
        {
            if(this.Value==pokeValues[i].Value)
            {
                this.NumIndex++;
            }
        }
    }
    /// <summary>
    /// 选中该卡牌
    /// </summary>
    private void OnMouseDown()
    {
        if (IsOut) return;
        if (IsSelect)
        {
            this.IsSelect = false;
            transform.position -= transform.up*10f;
        }
        else
        {
            this.IsSelect = true;
            transform.position += transform.up*10f;
        }
    }
}
