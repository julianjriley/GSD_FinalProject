using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public int bounty;
    public static event Action<int> OnEnemyDeath;
    PhotonView pv;
    private void OnEnable()
    {
        pv = GetComponent<PhotonView>();
    }
    [PunRPC]
    public void TakeDamage(float damage)
    {
        if(pv.IsMine)
        {
            health -= damage;
            if (health <= 0)
            {
                OnEnemyDeath.Invoke(bounty);
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }
}
