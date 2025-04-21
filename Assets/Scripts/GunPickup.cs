using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;
using UnityEngine;

public class GunPickup : MonoBehaviourPun
{

    public static event Action<string> NearWeapon;
    public static event Action LeaveWeapon;


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

    [Tooltip("Name of weapon")]
    public string gunName;

    [Tooltip("How much this weapon costs")]
    public int gunValue;

    [Tooltip("The text showing the gun's price")]
    public TextMeshProUGUI gunPriceText;

    GunType gun;
    PlayerGun playerGun;

    void Start()
    {
        gun = new GunType(defaultDamage, defaultFireDelay, defaultBurstCount, defaultBurstShotTime, defaultBulletCount, defaultBulletSpreadDegrees, defaultBulletVelocity, defaultGunSprite);
        playerGun = GameObject.FindFirstObjectByType<PlayerGun>();
        SpriteRenderer gunRenderer;
        gunRenderer = gameObject.GetComponent<SpriteRenderer>();
        gunRenderer.sprite = defaultGunSprite;
        gunPriceText.text = "$" + gunValue;
    }

    public void NetworkDestory()
    {
        photonView.RPC("NetworkDestroyInternal", RpcTarget.All);
    }

    [PunRPC]
    void NetworkDestroyInternal()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    public GunType Gun { get { return gun; } }

    
    /*
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            NearWeapon.Invoke(gunName);
            col.gameObject.GetComponent<PlayerMovement>().SetClosestGun(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            LeaveWeapon.Invoke();
            other.gameObject.GetComponent<PlayerMovement>().SetClosestGun(null)()
        }
    }
    */

}
