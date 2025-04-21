using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI theText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.NearWeapon += TurnOnText;
        PlayerMovement.LeaveWeapon += TurnOffText;
    }

    private void OnDisable()
    {
        PlayerMovement.NearWeapon -= TurnOnText;
        PlayerMovement.LeaveWeapon -= TurnOffText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TurnOnText(string gunName)
    {
        theText.enabled = true;
        theText.text = "Press E to buy " + gunName;
    }

    void TurnOffText()
    {
        theText.enabled = false;
        theText.text = "";
    }
}
