using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int life;
    public int maxHealth = 100;
    public int experience;
    public int coins;
    public int level = 1;

    public HealthBar healthBar;
    public ProgressBar progressBar;
    public Coins playerCoins;
    public GameObject deathMenu;
    public SkillTree st;
    public GameObject tree;
    public bool treeIsUp;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource takeDamageSound;
    public Image skillPointsNotUsed;

    public int attack = 10;
    public int defense = 0;
    private bool gameOver = false;
    private int formerLife = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetReferences();
        Time.timeScale = 1f;
        //st.points = 0;
        life = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        experience = 0;
        treeIsUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (experience / level >= 100)
        {
            experience = experience - 100 * level;
            level++;
            st.points++;
            st.UpdateTalentPointText();
            attack = attack + 2;
            maxHealth = maxHealth + 20;
            life = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(maxHealth);
        }
   
        playerCoins.SetCoin(coins);
        healthBar.SetHealth(life);
        progressBar.CurrentValue = experience;
        progressBar.lvl = level;
        //experience = progressBar.CurrentValue();
        if (formerLife != life)
        {
            if (formerLife > life)
            {
                takeDamageSound.Play();
            }
            formerLife = life;
        }
        if (life <= 0)
        {
            if (!gameOver)
            {
                gameOver = true;
                gameOverSound.Play();
                Time.timeScale = 0f;
                deathMenu.SetActive(true);
                //death screen
                healthBar.SetA(false);
                progressBar.SetA(false);
                //playerCoins.SetA(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            life = life - 500;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            experience += 1000;
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
        if (st.points > 0)
        {
            skillPointsNotUsed.enabled = true;
        } else
        {
            skillPointsNotUsed.enabled = false;
        }
            // level up 100xp -> 2, 200xp -> 3, 300xp -> 4 and so on

    }

    void TakeDamage(int damage)
    {
        life -= damage;
        healthBar.SetHealth(life);
    }

    void SetReferences()
    {
        tree = GameObject.Find("Skill_Tree_Canvas(Clone)").transform.GetChild(0).gameObject;
        st = tree.GetComponent<SkillTree>();
    }
}
