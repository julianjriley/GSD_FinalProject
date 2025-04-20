using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Money : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI moneyText;
    public int moneyValue;

    private void OnEnable()
    {
        EnemyHealth.OnEnemyDeath += UpdateMoney;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= UpdateMoney;
    }

    void UpdateMoney(int amount)
    {
        this.photonView.RPC("UpdateMoneyInternal", RpcTarget.All, amount);
    }

    void UpdateMoneyInternal(int amount)
    {
        moneyValue += amount;
        moneyText.text = "Money: " + moneyValue.ToString();
    }
}
