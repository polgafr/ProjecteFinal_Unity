using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sora : MonoBehaviour {

	private Rigidbody2D soraRigidBody;

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool attack;

    private bool facingLeft;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private bool isGrounded;

    private bool jump;

    private bool stillLanding;

    [SerializeField]
    private float jumpForce;

    // Use this for initialization
    void Start (){ 
        facingLeft = true;
		soraRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();

        HandleInput();

		HandleMovement(horizontal);

        Flip(horizontal);

        HandleAttacks();

        HandleLayers();

        ResetValues();
	}

    private void HandleAttacks()
    {
        if (attack 
            && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Sora_Run")
            && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Sora_Strike_Raid"))
        {
            myAnimator.SetTrigger("strikeRaid");
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            attack = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
        }
    }

	private void HandleMovement(float horizontal){
        if (soraRigidBody.velocity.y < 0)
        {
            myAnimator.SetBool("fall", true);
        }
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Sora_Strike_Raid"))
        {
            soraRigidBody.velocity = new Vector2(horizontal * movementSpeed, soraRigidBody.velocity.y);
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            soraRigidBody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
            if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Default"))
            {
                stillLanding = true;
            }
            else
            {
                stillLanding = false;
            }

        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
        myAnimator.SetBool("isGrounded", isGrounded);
    }

    private void Flip(float horizontal)
    {
        if (horizontal < 0 && !facingLeft || horizontal > 0 && facingLeft)
        {
            facingLeft = !facingLeft;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    private bool IsGrounded()
    {
        if (soraRigidBody.velocity.y == 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        myAnimator.SetBool("fall", false);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            if (!stillLanding)
            {
                myAnimator.SetLayerWeight(1, 0);
            }
        }
    }

    private void ResetValues()
    {
        attack = false;
        jump = false;
    }

}
