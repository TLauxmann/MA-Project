using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour

{

    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpSpeed = 5f;
    //[SerializeField] float fallMultiplier = 2.5f;
    //[SerializeField] float lowJumpMultiplier = 2f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] bool activePlayer = true;
    public GameObject camera;


    //private float timer = 0;
    //private float timerMax = 0;


    // State
    bool isAlive = true;
    float gravityScaleAtStart;
    bool facingRight = true;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    Joystick myjoystick;
    JumpButton myjumpbutton;
    SwitchPlayerButton switchPlayerButton;



    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        myjoystick = FindObjectOfType<Joystick>();
        myjumpbutton = FindObjectOfType<JumpButton>();
        switchPlayerButton = FindObjectOfType<SwitchPlayerButton>();

    }

    // Update is called once per frame
    void Update()
    {
        //switch Player for testing only
        if (switchPlayerButton.Pressed)
        {
            activePlayer = !activePlayer;
        }
        CameraHandling();

        if (!isAlive) { return; }

        //for offline testing
        if (activePlayer)
        {
            Run();
            Jump();
            Die();
        }
    }

    private void CameraHandling(){
        if (activePlayer == true) {
            camera.SetActive(true);
        }
        if (activePlayer == false) {
            camera.SetActive(false);
        }

    }


    private void Run()
    {


        float controlThrow = myjoystick.Horizontal; // value is betweeen -1 to +1

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;


        myAnimator.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));


        // If the input is moving the player right and the player is facing left...
        if (controlThrow > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (controlThrow < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

    }

    private void Flip()
    {

        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);

    }

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        else
        {
            myAnimator.SetBool("IsJumping", false);
        }



        if (myjumpbutton.Pressed == true)
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
            myAnimator.SetBool("IsJumping", true);
        }


        /*if (myRigidBody.velocity.y < 0)
        {
            myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (myRigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            myRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        */


    }

    private void Die() {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazard"))) {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidBody.velocity = deathKick;
        }
    }

}
