using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
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
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }
}
