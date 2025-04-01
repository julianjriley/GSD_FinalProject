using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [Tooltip("The damage each bullet does.")]
    public float defaultDamage = 1f;

    [Tooltip("The delay between each shot, in seconds.")]
    public float defaultFireDelay = 0.5f;

    [Tooltip("The number of shots fired for each trigger pull. Set to 1 for single-shot weapons.")]
    public int defaultBurstCount = 1;

    [Tooltip("The time between each shot in a burst, in seconds.")]
    public float defaultBurstShotTime = 0f;

    [Tooltip("The number of bullets fired in a spread, for shotgun-type weapons.")]
    public int defaultBulletCount = 1;

    [Tooltip("The random variation in bullet direction, in degrees.")]
    public float defaultBulletSpreadDegrees = 0f;

    [Tooltip("The velocity of each bullet.")]
    public float defaultBulletVelocity = 20f;

    [Tooltip("The image used to represent the gun.")]
    public Sprite defaultGunSprite;

    GunType gun;
    PlayerGun playerGun;

    void Start()
    {
        gun = new GunType(defaultDamage, defaultFireDelay, defaultBurstCount, defaultBurstShotTime, defaultBulletCount, defaultBulletSpreadDegrees, defaultBulletVelocity, defaultGunSprite);
        playerGun = GameObject.FindFirstObjectByType<PlayerGun>();
        SpriteRenderer gunRenderer;
        gunRenderer = gameObject.GetComponent<SpriteRenderer>();
        gunRenderer.sprite = defaultGunSprite;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerGun.SetGun(gun);
        }
    }
}
