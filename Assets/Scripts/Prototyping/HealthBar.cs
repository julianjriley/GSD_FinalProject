using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class HealthBar : MonoBehaviour
{

    Image image;

   

    private void OnEnable()
    {
        PlayerHealth.HealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        PlayerHealth.HealthChanged -= UpdateHealthBar;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealthBar(float healthPercentage)
    {
        image.fillAmount = healthPercentage;
    }
}
