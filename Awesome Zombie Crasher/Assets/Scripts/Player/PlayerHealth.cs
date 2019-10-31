using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    Slider healthSlider;
    GameObject UIHolder;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GameObject.Find("Health Bar").GetComponent<Slider>();
        healthSlider.value = health;
        UIHolder = GameObject.Find("UI Holder");
    }

    public void ApplyDamage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }

        healthSlider.value = health;
        if (health == 0)
        {
            UIHolder.SetActive(false);
            GameController.instance.GameOver();
        }
    }
}
