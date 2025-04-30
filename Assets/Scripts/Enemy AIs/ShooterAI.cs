using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    public GameObject bullet;
    public GameObject spawnPoint;
    public float fireDelay = 1f;
    public float moveSpeed = 2.5f;
    public float turnSpeed = 30f;
    public float range = 10;
    public bool isMoving = true;

    GameObject[] players;
    Vector2 moveDirection;
    Vector3 target;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    bool canFire = true;

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

        isMoving = minDistance > range;
        target = closestPlayer.transform.position;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, turnSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, q) <= 10 && canFire && !isMoving)
        {
            StartCoroutine(Shoot());
        }

    }

    IEnumerator Shoot()
    {
        canFire = false;
        Instantiate(bullet, spawnPoint.transform.position, gameObject.transform.rotation);
        yield return new WaitForSeconds(fireDelay);
        canFire = true;
    }

    void FindNewPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
}
