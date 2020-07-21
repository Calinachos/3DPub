using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool attack = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack1"))
        {
            animator.SetTrigger("Attack1");
        }

        attack = animator.GetBool("isAttacking");

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    
        if (Input.GetButtonDown("Jump") && !attack)
        {
            jump = true;
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
