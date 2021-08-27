using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Image healthBar;
    private float currentHealth;
    private float maxHealth = 100f;

    Player player;

    void Start()
    {
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        currentHealth = player.health_P;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

}
