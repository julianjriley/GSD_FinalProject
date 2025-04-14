using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public PlayerMovement movement;

    Camera _camera;
    public void IsLocalPlayer()
    {
        movement.enabled = true;
        _camera = Camera.main;
        _camera.gameObject.SetActive(true);

    public GameObject Camera;
    public void IsLocalPlayer()
    {
        movement.enabled = true;
        Camera.SetActive(true);
    }

}
