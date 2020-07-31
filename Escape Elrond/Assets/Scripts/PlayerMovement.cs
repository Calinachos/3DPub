using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemy; //StefanEpure

    public CharacterController2D controller;
    public Animator animator;
    public PauseMenu pause;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool attack = false;

    /***********************************************/
    /***********************************************/
    //Function for fighting an enemy - by Stefan Epure
    void Attack()
    {
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
                collider.gameObject.transform.parent.GetComponent<Entity>().Damage(ad);
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
            if (Input.GetButtonDown("Attack1"))
            {
                animator.SetTrigger("Attack1");
                Attack();
            }

            attack = animator.GetBool("isAttacking");

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump") && !attack)
            {
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
