using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeType : MonoBehaviour {
    public static PokeType pokeType;
    private void Awake()
    {
        pokeType = this;
    }
    /// <summary>
    /// 牌型
    /// </summary>
	public enum PokeSType
    {
        Empty,
        IsOne,
        IsTwo,
        IsThree,
        IsThreeAndOne,
        IsThreeAndTwo,
        IsBoom,
        IsBoomAndOne,
        IsBoomAndTwo,
        IsBoomAndDui,
        IsBoomAndFour,
        IsBoomAndTwoDui,
        IsTwoBoomAndTwo,
        IsTwoBoomAndTwoDui,
        IsThreeBoomAndThree,
        IsThreeBoomAndThreeDui,
        IsFourBoomAndFourDui,
        IsManyDui,
        IsMany,
        IsPlane
    }
    /// <summary>
    /// 一张牌的时候：单张
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsOnePoke(List<PokeValue> ValueList)
    {
        if(ValueList.Count==1)
        {
            return PokeSType.IsOne;
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 两张牌的时候：对子、炸弹
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsTwoPoke(List<PokeValue> ValueList)
    {
        if (ValueList.Count == 2)
        {
            if(ValueList[0].Value== ValueList[1].Value)
            {
                if(ValueList[0].Value==16)
                {
                    return PokeSType.IsBoom;
                }
                else
                {
                    return PokeSType.IsTwo;
                }
            }
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 三张牌的时候：三张牌
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsThreePoke(List<PokeValue> ValueList)
    {
        if (ValueList.Count == 3)
        {
            for (int i = 1; i < ValueList.Count; i++)
            {
                if(ValueList[i].Value!=ValueList[0].Value)
                {
                    return PokeSType.Empty;
                }
                if(i==ValueList.Count-1)
                {
                    return PokeSType.IsThree;
                }
            }
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 四张牌的时候：炸弹、三带一
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsFourPoke(List<PokeValue> ValueList)
    {
        if (ValueList.Count == 4)
        {
            if (ValueList[0].Value == ValueList[1].Value && ValueList[1].Value == ValueList[2].Value)
            {
                if(ValueList[0].Value== ValueList[3].Value)
                {
                    return PokeSType.IsBoom;
                }
                else
                {
                    return PokeSType.IsThreeAndOne;
                }
            }
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 五张牌的时候：三带二、顺子、一个炸弹带一张
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsFivePoke(List<PokeValue> ValueList)
    {
        if (ValueList.Count == 5)
        {
            if (ValueList[0].Value == ValueList[1].Value && ValueList[1].Value == ValueList[2].Value)
            {
                if (ValueList[0].Value == ValueList[3].Value)
                {
                    return PokeSType.IsBoomAndOne;
                }
                else if(ValueList[3].Value== ValueList[4].Value)
                {
                    return PokeSType.IsThreeAndTwo;
                }
            }
            else
            {
                return IsManyPoke(ValueList);
            }
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 六张牌的时候炸弹带一对、连对、顺子、炸弹带两单张
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsSixPoke(List<PokeValue> ValueList)
    {
        if(IsTwoDuiPoke(ValueList))
        {
            return PokeSType.IsManyDui;
        }
        if (ValueList.Count == 6)
        {
            if (ValueList[0].Value == ValueList[1].Value && ValueList[1].Value == ValueList[2].Value)
            {
                if (ValueList[0].Value == ValueList[3].Value)
                {
                    if(ValueList[4].Value== ValueList[5].Value)
                    {
                        return PokeSType.IsBoomAndDui;
                    }
                    else
                    {
                        return PokeSType.IsBoomAndTwo;
                    }
                }
            }
            else
            {
                return IsManyPoke(ValueList);
            }
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 七张牌的时候：顺子
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsSevenPoke(List<PokeValue>ValueList)
    {
        return IsManyPoke(ValueList);
    }
    /// <summary>
    /// 八张牌的时候：顺子、连对、飞机、炸弹带两对
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsEightPoke(List<PokeValue>ValueList)
    {
        if (IsTwoDuiPoke(ValueList))
        {
            return PokeSType.IsManyDui;
        }
        if (ValueList[0].Value == ValueList[1].Value && ValueList[1].Value == ValueList[2].Value)
        {
            if(ValueList[2].Value==ValueList[3].Value)
            {
                if (ValueList[4].Value == ValueList[5].Value&&ValueList[6].Value==ValueList[7].Value)
                {
                    return PokeSType.IsBoomAndTwoDui;
                }
            }
            if (ValueList[3].Value == ValueList[4].Value && ValueList[4].Value == ValueList[5].Value)
            {
                if(ValueList[0].Value-ValueList[3].Value==1)
                {
                    return PokeSType.IsPlane;
                }
                
            }
        }
        else 
        {
            return IsManyPoke(ValueList);
        }
        return PokeSType.Empty;
    }
    /// <summary>
    ///九张牌的时候：顺子
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsNinePoke(List<PokeValue>ValueList)
    {
        return IsManyPoke(ValueList);
    }
    /// <summary>
    /// 十张牌的时候：飞机、顺子、连对、两炸带两张
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsTenPoke(List<PokeValue>ValueList)
    {
        if (IsTwoDuiPoke(ValueList))
        {
            return PokeSType.IsManyDui;
        }
        if (ValueList[0].Value == ValueList[1].Value && ValueList[1].Value == ValueList[2].Value)
        {
            if(ValueList[2].Value == ValueList[3].Value)
            {
                for (int i = 5; i < 8; i++)
                {
                    if(ValueList[i].Value!= ValueList[4].Value)
                    {
                        break;
                    }
                    if(i==7)
                    {
                        return PokeSType.IsTwoBoomAndTwo;
                    }
                }
            }
            if (ValueList[3].Value == ValueList[4].Value && ValueList[4].Value == ValueList[5].Value)
            {
                if(ValueList[9].Value == ValueList[8].Value&& ValueList[7].Value == ValueList[6].Value)
                {
                    if (ValueList[0].Value - ValueList[3].Value == 1)
                    {
                        return PokeSType.IsPlane;
                    }
                    
                }
            }
        }
        else
        {
            return IsManyPoke(ValueList);
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 十一张牌的时候：顺子
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsElevenPoke(List<PokeValue>ValueList)
    {
        return IsManyPoke(ValueList);
    }
    /// <summary>
    /// 十二张牌的时候：连对、顺子、飞机、两炸带两对
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsTwelvePoke(List<PokeValue>ValueList)
    {
        if (IsTwoDuiPoke(ValueList))
        {
            return PokeSType.IsManyDui;
        }
        if (ValueList[0].Value == ValueList[1].Value && ValueList[1].Value == ValueList[2].Value)
        {
            if(ValueList[2].Value == ValueList[3].Value)
            {
                for (int i = 5; i < 8; i++)
                {
                    if (ValueList[i].Value != ValueList[4].Value)
                    {
                        break;
                    }
                    if (i == 7)
                    {
                        if(ValueList[11].Value== ValueList[10].Value&& ValueList[9].Value == ValueList[8].Value)
                        {
                            return PokeSType.IsTwoBoomAndTwoDui;
                        }
                        
                    }
                }
            }
            if (ValueList[3].Value == ValueList[4].Value && ValueList[4].Value == ValueList[5].Value)
            {
                if (ValueList[6].Value == ValueList[7].Value && ValueList[7].Value == ValueList[8].Value)
                {
                    if (ValueList[0].Value - ValueList[3].Value == -1&&ValueList[3].Value-ValueList[6].Value==1)
                    {
                        return PokeSType.IsPlane;
                    }
                }
            }
        }
        else
        {
            return IsManyPoke(ValueList);
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 十三张牌的时候：顺子
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsThirteenPoke(List<PokeValue>ValueList)
    {
        return IsManyPoke(ValueList);
    }
    /// <summary>
    /// 十四张牌的时候：连对
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsFourteenPoke(List<PokeValue>ValueList)
    {
        if (IsTwoDuiPoke(ValueList))
        {
            return PokeSType.IsManyDui;
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 十五张的时候:三炸带三、飞机
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsFifteenPoke(List<PokeValue>ValueList)
    {
        if (ValueList[0].Value == ValueList[1].Value && ValueList[1].Value == ValueList[2].Value)
        {
            if (ValueList[2].Value == ValueList[3].Value)
            {
                for (int i = 5; i < 8; i++)
                {
                    if (ValueList[i].Value != ValueList[4].Value)
                    {
                        break;
                    }
                    if(ValueList[i+4].Value != ValueList[8].Value)
                    {
                        break;
                    }
                    if (i == 7)
                    {
                        return PokeSType.IsThreeBoomAndThree;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)//3+2  *3
                {
                    if(ValueList[3].Value!= ValueList[i + 4].Value)
                    {
                        break;
                    }
                    if (ValueList[6].Value != ValueList[i + 7].Value)
                    {
                        break;
                    }
                    if(i==1)
                    {
                        for (int j = 9; j < ValueList.Count; j+=2)
                        {
                            if(ValueList[j].Value!=ValueList[j+1].Value)
                            {
                                break;
                            }
                            if(j==ValueList.Count-1)
                            {
                                if (ValueList[0].Value - ValueList[3].Value == -1 && ValueList[3].Value - ValueList[6].Value == 1)
                                {
                                    return PokeSType.IsPlane;
                                }
                            }
                        }
                    }
                }
            }
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 十六张的时候：连对、飞机
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsSixteenPoke(List<PokeValue>ValueList)
    {
        if (IsTwoDuiPoke(ValueList))
        {
            return PokeSType.IsManyDui;
        }
        for (int i = 1; i < 3; i++)//3*4+4
        {
            if (ValueList[0].Value != ValueList[i].Value)
            {
                break;
            }
            if (ValueList[3].Value != ValueList[i + 3].Value)
            {
                break;
            }
            if (ValueList[6].Value != ValueList[i + 6].Value)
            {
                break;
            }
            if (i == 2)
            {
                if (ValueList[0].Value - ValueList[3].Value == -1 && ValueList[3].Value - ValueList[6].Value == -1&&ValueList[6].Value-ValueList[9].Value==1)
                {
                    return PokeSType.IsPlane;
                }
            }
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 十七张的时候
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsSeventeenPoke(List<PokeValue> ValueList)
    {//
        return PokeSType.Empty;
    }
    /// <summary>
    /// 十八张的时候：连对、三炸带三对
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsEighteenPoke(List<PokeValue> ValueList)
    {
        if (IsTwoDuiPoke(ValueList))
        {
            return PokeSType.IsManyDui;
        }
        for (int i = 1; i < 4; i++)
        {
            if (ValueList[0].Value != ValueList[i].Value)
            {
                break;
            }
            if (ValueList[4].Value != ValueList[4+i].Value)
            {
                break;
            }
            if (ValueList[8].Value != ValueList[8 + i].Value)
            {
                break;
            }
            if (i == 3)
            {
                for (int j = 12; j < ValueList.Count; j+=2)
                {
                    if(ValueList[j].Value!=ValueList[j+1].Value)
                    {
                        break;
                    }
                    if(j==ValueList.Count-1)
                    {
                        return PokeSType.IsThreeBoomAndThreeDui;
                    }
                }
                
            }
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 十九张的时候
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsNineteenPoke(List<PokeValue> ValueList)
    {
        return PokeSType.Empty;
    }
    /// <summary>
    /// 二十张的时候：连对、四炸带四只、飞机三带二*4
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsTwentyPoke(List<PokeValue> ValueList)
    {
        if (IsTwoDuiPoke(ValueList))
        {
            return PokeSType.IsManyDui;
        }
        for (int i = 1; i < 4; i++)//四炸带四
        {
            if (ValueList[0].Value != ValueList[i].Value)
            {
                break;
            }
            if (ValueList[4].Value != ValueList[4 + i].Value)
            {
                break;
            }
            if (ValueList[8].Value != ValueList[8 + i].Value)
            {
                break;
            }
            if (ValueList[8].Value != ValueList[8 + i].Value)
            {
                break;
            }
            if (ValueList[12].Value != ValueList[12 + i].Value)
            {
                break;
            }
            if (i == 3)
            {
                return PokeSType.IsFourBoomAndFourDui;
            }
        }
        for (int i = 1; i < 3; i++)//3+2  *4
        {
            if (ValueList[0].Value != ValueList[i].Value)
            {
                break;
            }
            if (ValueList[3].Value != ValueList[i+3].Value)
            {
                break;
            }
            if (ValueList[6].Value != ValueList[i + 6].Value)
            {
                break;
            }
            if (ValueList[9].Value != ValueList[i + 9].Value)
            {
                break;
            }
            if (i == 2)
            {
                for (int j = 13; j < ValueList.Count; j += 2)
                {
                    if (ValueList[j].Value != ValueList[j + 1].Value)
                    {
                        break;
                    }
                    if (j == ValueList.Count - 1)
                    {
                        if (ValueList[0].Value - ValueList[3].Value == -1 && ValueList[3].Value - ValueList[6].Value == -1&&ValueList[6].Value-ValueList[9].Value==-1&&ValueList[9].Value-ValueList[12].Value==1)
                        {
                            return PokeSType.IsPlane;
                        }
                        
                    }
                }
            }
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 顺子
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public PokeSType IsManyPoke(List<PokeValue> ValueList)
    {
        for (int i = 0; i < ValueList.Count-1; i++)
        {
            if(ValueList[i].Value-ValueList[i+1].Value!=-1)
            {
                return PokeSType.Empty;
            }
            else
            {
                if(i==ValueList.Count-2)
                {
                    return PokeSType.IsMany;
                }
            }
        }
        return PokeSType.Empty;
    }
    /// <summary>
    /// 连对
    /// </summary>
    /// <param name="ValueList"></param>
    /// <returns></returns>
    public bool IsTwoDuiPoke(List<PokeValue>ValueList)
    {
        if(ValueList.Count>=6&&ValueList.Count%2==0)
        {
            for (int i = 0; i < ValueList.Count - 1; i += 2)
            {
                if (ValueList[i].Value != ValueList[i + 1].Value)
                {
                    break;
                }
                if (i == ValueList.Count - 2)
                {
                    for (int j = 0; j < ValueList.Count -2; j += 2)
                    {
                        if (ValueList[j].Value - ValueList[j + 2].Value == -1)
                        {
                            if (j == ValueList.Count - 4) return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
