using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 20f;
    PhotonView pv;
    private void Start()
    {

    }
    private void OnEnable()
    {
        StartCoroutine(DestroyAfterDelay());
        pv = GetComponent<PhotonView>();
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.up);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!pv.IsMine)
            return;
        PlayerHealth player;
        if (col.gameObject.TryGetComponent<PlayerHealth>(out player))
        {
            col.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            //enemy.TakeDamage(damage);
        }
        PhotonNetwork.Destroy(gameObject);
    }


    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.Destroy(gameObject);
    }

}
