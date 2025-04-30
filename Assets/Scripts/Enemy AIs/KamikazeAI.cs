using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeAI : MonoBehaviour
{
    public float acceleration = 1f;
    public float maxSpeed = 5f;
    public float detonationRange = 1f;

    public GameObject explosion;
    GameObject[] players;
    Vector2 moveDirection;
    Vector3 target;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("FindNewPlayers", 0.1f, 5f);
    }

    // Update is called once per frame
    void Update()
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
        if (minDistance < detonationRange/2)
        {
            GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
            e.GetComponent<Explosion>().maxScale = detonationRange;
            e.GetComponent<Explosion>().damagesPlayer = true;
            Destroy(gameObject);
        }
        moveDirection = (closestPlayer.transform.position - gameObject.transform.position).normalized;
    }
    private void FixedUpdate()
    {

        rb.AddForce(moveDirection * acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
    void FindNewPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
}
