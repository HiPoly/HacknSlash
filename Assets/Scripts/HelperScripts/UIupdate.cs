using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIupdate : MonoBehaviour
{
    //The Player
    public GameObject Player;
    public Slider HealthSlider;
    public Slider PowerSlider;

    void Update()
    {
        UpdateHealth();
        UpdatePower();
    }

    public void UpdateHealth(){
        HealthSlider.value = GameObject.Find("Player").GetComponent<PlayerStats>().CurrentHealth;
    }
    public void UpdatePower(){
        PowerSlider.value = GameObject.Find("Player").GetComponent<PlayerStats>().CurrentPower;
    }
}



