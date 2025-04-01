using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    public GameObject bullet;
    public GameObject spawnPoint;
    public float moveSpeed = 2.5f;
    public float turnSpeed = 2f;
    public float range = 10;
    public bool isMoving = true;

    GameObject[] players;
    Vector2 moveDirection;
    Vector2 target;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
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

        float angleRad = Mathf.Atan2(target.y - gameObject.transform.position.y, target.x - gameObject.transform.position.x);
        float angleDeg = (180/Mathf.PI)*angleRad;
        if (angleDeg < 0)
        {
            angleDeg += 360;
        }
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(transform.rotation.z, angleDeg, turnSpeed * Time.time)); 

    }
}
