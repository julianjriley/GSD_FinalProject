using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;

public class Money : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI moneyText;
    public int moneyValue;

    private static Money _instance;
    public static Money Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Where the hell is it");
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

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

    public void UseMoney(int amount)
    {
        this.photonView.RPC("UseMoneyInternal", RpcTarget.All, amount);
    }

    public int GetMoney()
    {
        return moneyValue;
    }

    [PunRPC]
    void UpdateMoneyInternal(int amount)
    {
        moneyValue += amount;
        moneyText.text = "$" + moneyValue.ToString();
    }

    [PunRPC]
    void UseMoneyInternal(int amount)
    {
        moneyValue -= amount;
        moneyText.text = "$" + moneyValue.ToString();
    }
}
