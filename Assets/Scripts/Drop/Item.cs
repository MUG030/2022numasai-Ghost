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
        HPportion//HP�񕜃A�C�e��
    }

    [SerializeField]
    public Type itemType = Type.HPportion;
    //�A�C�e���̖��O
    [SerializeField]
    private string ItemName = "";
    //�A�C�e���̏��
    [SerializeField]
    private string information = "";
    //�A�C�e���̌��i�R�C���Ȃ牽���Ȃ̂����j
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
