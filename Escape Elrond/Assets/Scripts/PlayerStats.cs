﻿using System.Collections;
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
    public GameObject deathMenu;
    public SkillTree st;
    public GameObject tree;
    public bool treeIsUp;

    int attack = 20;
    public int defense = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        level = 1;
        st.points = 0;
        life = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        experience = 0;
        treeIsUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerCoins.SetCoin(coins);
        healthBar.SetHealth(life);
        progressBar.CurrentValue = experience;
        progressBar.lvl = level;
        //experience = progressBar.CurrentValue();
        if (life <= 0)
        {
            Time.timeScale = 0f;
            deathMenu.SetActive(true);
            //death screen
            healthBar.SetA(false);
            progressBar.SetA(false);
            //playerCoins.SetA(false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            life = life - 500;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (treeIsUp == false)
            {
                tree.SetActive(true);
                treeIsUp = true;
            }
            else
            {
                tree.SetActive(false);
                treeIsUp = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (treeIsUp)
            {
                tree.SetActive(false);
                treeIsUp = false;
            }
        }
            //Debug.Log("Current Hp : " + life + " MaxHp: " + maxHealth + " Experience: " + experience);
            // level up 100xp -> 2, 200xp -> 3, 300xp -> 4 and so on

            if (experience / level >= 100)
        {
            experience = experience - 100 * level;
            level++;
            st.points++;
            attack = attack + 2;
            life = life + 20;
            maxHealth = maxHealth + 20;
            if (life<= maxHealth)
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
