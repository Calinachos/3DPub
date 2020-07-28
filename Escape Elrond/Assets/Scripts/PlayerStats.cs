using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int life;
    public int maxHealth = 100;
    public int experience;
    public int coins;
    public int level;

    public HealthBar healthBar;
    public ProgressBar progressBar;
    public Coins playerCoins;

    int attack = 20;
    int defense = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        life = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        experience = 0;
        //progressBar.CurrentValue();
        coins = 100;
        playerCoins.SetCoin(coins);
    }

    // Update is called once per frame
    void Update()
    {
        //experience = progressBar.CurrentValue();
        if (life == 0)
        {
            //death screen
        }

        // level up 100xp -> 2, 200xp -> 3, 300xp -> 4 and so on
        if (experience / level >= 100)
        {
            experience = experience - 100 * level;
            level++;
            attack = attack + 2;
            life = life + 20;
            if(life<= maxHealth)
                healthBar.SetHealth(life);
            else
                healthBar.SetHealth(maxHealth);
           
        }

    }

    void TakeDamage(int damage)
    {
        life -= damage;
        healthBar.SetHealth(life);
    }
}
