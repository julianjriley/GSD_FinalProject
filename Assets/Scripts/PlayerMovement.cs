using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using Unity.VisualScripting;

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody2D rb;
    float vertical;
    float horizontal;
    Vector2 moveDirection;
    

    public float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizontal, vertical).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if(base.IsOwner)
        {

        }
        else
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
