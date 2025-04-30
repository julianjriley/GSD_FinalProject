using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class FodderAI : MonoBehaviour
{
    GameObject[] players;
    Vector2 moveDirection;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    bool playerSpawned = false;

    public float moveSpeed = 2.5f;
    public float contactDPS = 0.1f;

    private void Start()
    {
    }

    private void OnEnable()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        InvokeRepeating("FindNewPlayers", 0.1f, 5f);
    }



    private void Update()
    {
        //Move towards the closest player.
        //This seems like a dumb way to do this, but Google isn't giving me a better one so... Too bad!

        float minDistance = float.MaxValue;
        GameObject closestPlayer = null;
        foreach (var player in players)
        {
            float distance = Vector2.Distance(player.transform.position, gameObject.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPlayer = player;
            }
        }
        moveDirection = (closestPlayer.transform.position - gameObject.transform.position).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        PlayerHealth ph;
        if (col.gameObject.TryGetComponent<PlayerHealth>(out ph))
        {
            ph.TakeDamage(contactDPS * Time.deltaTime);
        }
    }

    void FindNewPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
}
