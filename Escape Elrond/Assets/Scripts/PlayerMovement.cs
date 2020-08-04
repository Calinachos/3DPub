using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemy; //StefanEpure

    public CharacterController2D controller;
    public Animator animator;
    public PauseMenu pause;
    public PlayerStats ps;
    [SerializeField] private AudioSource missAttackSound;
    [SerializeField] private AudioSource attackSound;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool attack = false;

    /***********************************************/
    /***********************************************/
    //Function for fighting an enemy - by Stefan Epure
    void Attack()
    {
        bool attackedSomebody = false;
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 1, whatIsEnemy);
        //Cel mai bine ar fi sa schimbati raza, eu am pus-o 1, dar vedeti voi care merge mai bine*****^****** Ar merge si parametrizata

        foreach (Collider2D collider in detectedObjects)
        {

            if (collider.gameObject.tag == "Enemy")
            {
                AttackDetails ad;
                ad.damageAmount = 10;
                ad.position = (Vector2)collider.gameObject.transform.position;
                ad.stunDamageAmount = 5;
                if (!collider.gameObject.transform.parent.GetComponent<Entity>().isDead)
                {
                    collider.gameObject.transform.parent.GetComponent<Entity>().Damage(ad);
                    attackedSomebody = true;
                }
            }

        }
        if (attackedSomebody)
        {
            attackSound.Play();
        } else
        {
            if (!missAttackSound.isPlaying)
            {
                missAttackSound.Play();
            }
        }
    }
    /**************************************************/
    /**************************************************/


    // Update is called once per frame
    void Update()
    {
        if (pause.GameIsPaused == false)
        {
            if (ps.treeIsUp == false)
            {
                if (Input.GetButtonDown("Attack1"))
                {
                    animator.SetTrigger("Attack1");
                    Attack();
                }
                attack = animator.GetBool("isAttacking");
            }
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump") && !attack)
            {
                animator.SetTrigger("Jump");
                jump = true;
            }
        }

    }

    private void FixedUpdate()
    {
        // Move our character
        if (!attack)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        } else
        {
            controller.Move(0, false, false);
        }
    }
}
