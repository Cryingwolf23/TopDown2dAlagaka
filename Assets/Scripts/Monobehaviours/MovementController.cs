using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f; //Initialized movement speed
    Vector2 movement = new Vector2(); //storing the vector 2 values ( x and y) into movement

    Animator animator; //this variable will help us set the parameters in the animator later

    
    Rigidbody2D rb2D; //reference to the rigidbody component

   

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    
    private void Update()
    {
        UpdateState();
    }

    private void FixedUpdate()
    {
        MoveCharacter();   
    }

    private void MoveCharacter()
    {
        //Move with up, down, left and right arrowkeys or w,a,sd
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical"); 

        movement.Normalize();

        rb2D.velocity = movement * movementSpeed;
    }
    private void UpdateState() //Setting the animations in whichever direction the player goes to
    {
        if(Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isWalking", false);
        }

        else
        {
            animator.SetBool("isWalking", true);
        }

        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);
    }
}
