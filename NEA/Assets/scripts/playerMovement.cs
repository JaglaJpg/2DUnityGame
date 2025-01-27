using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //code for the player movement and changing animations
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D box;
    [SerializeField] private LayerMask ground;
    bool canDouble;
    bool facingRight = true;

    private float dirX = 0f;
    [SerializeField] private float speed = 14f;
    [SerializeField] private float jumpForce = 14f;
    private bool canRoll = true;
    private bool isRolling;
    private float rollForce = 25f;
    private float rollTime = 0.35f;
    private float rollCool = 1f;
    //different states of animation
    private enum MovementState { idle, running, jump, slide }

    //to start, I grab components i need such as the sprite renderer, animator and collider box.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    //Update happens every frame
    private void Update()
    {
        if (isRolling)
        {
            return;
        }
        //stores horizontal imput in a varaible and makes that my characters velocity in order to make him walk
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

        //checks for space press
        if (Input.GetButtonDown("Jump"))
        {
            //checks if character is grounded for the double jump feature
            if(IsGrounded())
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce);
                canDouble = true;
            }
            //if character isn't grounded but canDouble is true so that the character can double jump but no mroe that double
            else if (canDouble)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce);
                canDouble=false;
            }
            
        }
        //if the character isn't rolling and shift is pressed and character is moving you can roll
        if (!isRolling)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canRoll)
            {
                if (rb.velocity.x > .1f || rb.velocity.x < -.1f)
                {
                    StartCoroutine(Roll());
                }
                    
            }

        }
        
        //updates animations every frame
        UpdateAnimationState();

    }

    private void FixedUpdate()
    {
        if (isRolling)
        {
            return;
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        //switches which way animation is facing based on direction you're moving
        if (dirX > 0f && facingRight)
        {
            state = MovementState.running;
            Flip();
        }
        else if (dirX < 0f && !facingRight)
        {
            state = MovementState.running;
            Flip();
        }

        //changes the animation based on action e.g. if the character has horizontal velocity
        //change to run animation or if character has vertical velocity change to jump animation
        if (dirX > 0f)
        {
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f || rb.velocity.y < -.1f)
        {
            state = MovementState.jump;
        }

        if (isRolling == true)
        {
            state = MovementState.slide;
        }

        anim.SetInteger("state", (int)state);
        

    }

    //method for flipping gameobject depending on movement direction
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    //check if player is grounded
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, .5f, ground);
    }

    //coroutine for rolling
    private IEnumerator Roll()
    {
        canRoll = false;
        isRolling = true;
        rb.velocity = new Vector2(dirX * rollForce, rb.velocity.y);
        box.size = new Vector2(box.size.x, box.size.y / 3);
        yield return new WaitForSeconds(rollTime);
        isRolling = false;
        box.size = new Vector2(box.size.x, box.size.y * 3);
        yield return new WaitForSeconds(rollCool);
        canRoll = true;
    }
}
