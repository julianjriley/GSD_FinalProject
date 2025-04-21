using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<float> HealthChanged;

    PhotonView pv;

    public float maxHealth = 5;
    float health;

    private void OnEnable()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(pv.IsMine)
            health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!pv.IsMine)
            return;
        health -= damage;
        if (health < 0 )
        {
            //TODO: What happens when the player dies
            PhotonNetwork.Disconnect();
        }
        HealthChanged.Invoke( health/maxHealth );
        Debug.Log(health);
    }
}
