using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //The Player
    public GameObject Player;
    public Slider HealthSlider;
    public Slider PowerSlider;

    void Update()
    {
        UpdateHealth();
        UpdateForce();
    }

    public void UpdateHealth(){
        HealthSlider.minValue = 0;
        HealthSlider.value = Player.GetComponent<PlayerStats>().CurrentHealth;
        HealthSlider.maxValue = Player.GetComponent<PlayerStats>().StartingHealth;

    }
    public void UpdateForce(){
        PowerSlider.value = GameObject.Find("Player").GetComponent<PlayerStats>().CurrentForce;
    }
}



