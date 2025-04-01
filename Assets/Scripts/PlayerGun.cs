using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GunType defaultGun;
    public Bullet bullet;
    public GameObject spawnPoint;
    public SpriteRenderer gunImage;

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

    bool canShoot = true;

    private void Start()
    {
        defaultGun = new GunType(defaultDamage, defaultFireDelay, defaultBurstCount, defaultBurstShotTime, defaultBulletCount, defaultBulletSpreadDegrees, defaultBulletVelocity, defaultGunSprite);
    }


    void Update()
    {
        //Point the gun at the mouse
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mouseScreenPosition.y - gameObject.transform.position.y, mouseScreenPosition.x - gameObject.transform.position.x);
        float angleDeg = (180/Mathf.PI)*angleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, angleDeg);

        //Flip the gun if it would be facing upside-down
        if(transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
        {
            gunImage.flipY = true;
        }
        else
        {
            gunImage.flipY = false;
        }

        //Shoot when the player clicks
        if (canShoot && Input.GetMouseButton(0))
        {
            StartCoroutine(Shoot(defaultGun));
        }
    }

    public void SetGun(GunType type)
    {
        this.defaultGun = type;
        gunImage.sprite = defaultGun.gunSprite;
    }

    IEnumerator Shoot(GunType gun)
    {
        canShoot = false;
        for (int i = 0; i < gun.burstCount; i++)
        {
            for (int j = 0; j < gun.bulletCount; j++)
            {
                Bullet firedBullet = Instantiate(bullet, spawnPoint.transform.position, gameObject.transform.rotation * Quaternion.Euler(0, 0, UnityEngine.Random.Range(-1*gun.bulletSpreadDegrees, gun.bulletSpreadDegrees)));
                firedBullet.speed = gun.bulletVelocity;
                firedBullet.damage = gun.damage;
            }
            yield return new WaitForSeconds(gun.burstShotTime);
        }
        yield return new WaitForSeconds(gun.fireDelay);
        canShoot = true;
    }
}

public class GunType
{
    public float damage;
    public float fireDelay;
    public int burstCount;
    public float burstShotTime;
    public int bulletCount;
    public float bulletSpreadDegrees;
    public float bulletVelocity;
    public Sprite gunSprite;

    public GunType(float damage, float fireDelay, int burstCount, float burstShotTime, int bulletCount, float bulletSpreadDegrees, float bulletVelocity, Sprite gunSprite)
    {
        this.damage = damage;
        this.fireDelay = fireDelay;
        this.burstCount = burstCount;
        this.burstShotTime = burstShotTime;
        this.bulletCount = bulletCount;
        this.bulletSpreadDegrees = bulletSpreadDegrees;
        this.bulletVelocity = bulletVelocity;
        this.gunSprite = gunSprite;
    }
}
