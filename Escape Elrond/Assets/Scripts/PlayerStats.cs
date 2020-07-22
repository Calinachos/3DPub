using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int life;
    public int experience;
    public int coins;
    public int level;

    int attack = 20;
    int defense = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        life = 100;
        experience = 0;
        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            //death screen
        }

        // level up 100xp -> 2, 200xp -> 3, 300xp -> 4 and so on
        if (experience / level >= 100)
        {
            experience = experience - 100 * level;
            level++;
            life = life + 20;
            attack = attack + 2;
        }

    }
}
