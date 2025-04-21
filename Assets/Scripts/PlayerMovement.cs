using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    float vertical;
    float horizontal;
    Vector2 moveDirection;

    GunPickup closestGun;

    [SerializeField] PlayerGun playerGun;

    public static event Action<string> NearWeapon;
    public static event Action LeaveWeapon;

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

        if(closestGun != null && Input.GetKeyDown(KeyCode.E))
        {
            if(Money.Instance.moneyValue >= closestGun.gunValue)
            {
                Money.Instance.UseMoney(closestGun.gunValue);
                playerGun.SetGun(closestGun.Gun);
                closestGun.NetworkDestory();
            }
        }
    }

    public void SetClosestGun(GunPickup gun)
    {
        closestGun = gun;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pickup")
        {
            GunPickup theGun = collision.GetComponent<GunPickup>();
            NearWeapon.Invoke(theGun.gunName);
            closestGun = theGun;
            Debug.Log("hehu");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Pickup")
        {
            LeaveWeapon.Invoke();
            closestGun = null;
        }
    }
}
