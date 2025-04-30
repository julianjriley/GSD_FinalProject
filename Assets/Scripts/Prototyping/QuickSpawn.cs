using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class QuickSpawn : MonoBehaviourPunCallbacks
{

    bool startedSpawning = false;

    [SerializeField] Transform[] spawnPoints;

    private void Start()
    {
        
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedLobby();
        
        if(PhotonNetwork.IsMasterClient)
        {
            startedSpawning = true;
            StartCoroutine(SpawnEnemy());
        }
    }

    [PunRPC]
    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            PhotonNetwork.Instantiate("EnemyJulian", spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
            PhotonNetwork.Instantiate("EnemyJulian", spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
            yield return new WaitForSeconds(6f);
        }
        
    }


}
