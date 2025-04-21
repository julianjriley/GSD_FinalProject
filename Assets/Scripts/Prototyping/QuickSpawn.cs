using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class QuickSpawn : MonoBehaviourPunCallbacks
{

    bool startedSpawning = false;

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] string[] enemyPrefabs;

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
            List<int> availableSpawnPoints = new List<int>();
            for(int i = 0;  i < spawnPoints.Length; i++)
                availableSpawnPoints.Add(i);
            for(int i = 0; i < 2; i++)
            {
                int randomIndex = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count - 1)];
                availableSpawnPoints.Remove(randomIndex);
                PhotonNetwork.Instantiate("EnemyJulian", spawnPoints[randomIndex].position, Quaternion.identity);
            }
            //PhotonNetwork.Instantiate("EnemyJulian", spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
            //PhotonNetwork.Instantiate("EnemyJulian", spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
            yield return new WaitForSeconds(6f);
        }
        //enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)]

    }


}
