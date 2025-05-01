using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class Money : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI moneyText;
    public int moneyValue;
    ExitGames.Client.Photon.Hashtable moneyTable = new ExitGames.Client.Photon.Hashtable();
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
        if(PhotonNetwork.IsMasterClient)
        {

            moneyTable.Add("money", 0);
            PhotonNetwork.CurrentRoom.SetCustomProperties(moneyTable);
        }
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
        if(PhotonNetwork.IsMasterClient)
        {
            moneyTable["money"] = moneyValue;
            moneyText.text = "$" + moneyValue.ToString();
            PhotonNetwork.CurrentRoom.SetCustomProperties(moneyTable);
        }
            

    }

    [PunRPC]
    void UseMoneyInternal(int amount)
    {
        moneyValue -= amount;
        if(moneyValue < 0)
            moneyValue = 0;
        moneyText.text = "$" + moneyValue.ToString();
        
    }


}
