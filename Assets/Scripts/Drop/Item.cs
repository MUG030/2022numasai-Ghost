using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public enum Type
    {
        HPportion//HP回復アイテム
    }

    [SerializeField]
    public Type itemType = Type.HPportion;
    //アイテムの名前
    [SerializeField]
    private string ItemName = "";
    //アイテムの情報
    [SerializeField]
    private string information = "";
    //アイテムの個数（コインなら何枚なのか等）
    [SerializeField]
    private int amount = 0;

    public Type GetItemType()
    {
        return itemType;
    }

    public string GetItemName()
    {
        return ItemName;
    }

    public string GetInformation()
    {
        return information;
    }

    public int GetAmount()
    {
        return amount;
    }
}
