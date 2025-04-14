using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public PlayerMovement movement;
    PlayerGun playerAiming;
    

    Camera _camera;
    public void IsLocalPlayer()
    {
        playerAiming = GetComponent<PlayerGun>();
        playerAiming.enabled = true;
        movement.enabled = true;
        _camera = Camera.main;
        _camera.gameObject.SetActive(true);

    }
}
