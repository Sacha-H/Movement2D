using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animController;

    float horizontal_value;
    float vertical_value;
    float current = 110.0f;
    float target = 500.0f;
    Vector2 ref_velocity = Vector2.zero;

     float moveSpeed_horizontal = 1000f;

    bool cansmalldash = false;

    float jumpForce = 17f;
    [Range(0, 1)] [SerializeField] float smooth_time = 0.05f;


    float targetSpeed;
    float timePassed;

 
    [SerializeField] bool can_jump = false;
    bool downJumping = false;
    




    [SerializeField] bool IsGrounded = false;
    [SerializeField] int CountJump = 2;
  


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //animController = GetComponent<Animator>();
        Debug.Log(Mathf.Lerp(current, target, 0));
    }

    // Update is called once per frame
    void Update()
    {
        horizontal_value = Input.GetAxis("Horizontal");

        //animController.SetFloat("speed", Mathf.Abs(horizontal_value));
        //sr.flipX = horizontal_value < 0;
        //animController.SetFloat("fall", rb.velocity.y);

 

        if (Input.GetButtonDown("Jump") && CountJump > 0)
        {
            can_jump = true;


        }

        if ( Input.GetButtonUp("Jump"))  {

            downJumping = true;
        }
    }
    void FixedUpdate()
    {




        // MVT H
        if (IsGrounded == true)
        {
            smallDash();
        }else
        {
            moveSpeed_horizontal = 700f;
        }


        justgrouded();





        if (can_jump)
        {
            
            Jump();
           
            
        }

        if (downJumping || rb.velocity.y < 0 )
        {
            falling();
        }






        Debug.Log(targetSpeed);
        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.05f);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 35);



        //Debug.Log(rb.velocity);
        Debug.Log(target_velocity);
    }



    void Jump()
    {
        // Garantit que nous ne pouvons pas appeler Jump plusieurs fois à partir d'une seule pression
        //LastPressedJumpTime = 0;
        //LastOnGroundTime = 0;
       CountJump -= 1;

        // On augmente la force appliquée si on tombe
        // Cela signifie que nous aurons toujours l'impression de sauter le même montant

        
            rb.velocity = Vector2.zero;
            


        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        can_jump = false;
        downJumping = false;

    }

    void falling()
    {
        rb.AddForce(Vector2.down * jumpForce * 0.08f, ForceMode2D.Impulse);

    }

    void smallDash()
    {
        if (horizontal_value > 0.5 || horizontal_value < -0.5)


        {
            timePassed += Time.fixedDeltaTime;
            if (timePassed > 0.1)
            {
                moveSpeed_horizontal = 800f;

            }
            else
            {
                moveSpeed_horizontal = 1000f;
            }

        }
        else
        {
            timePassed = 0;
        }
    }



    void justgrouded()
    {
        if (cansmalldash == true){
            if (IsGrounded == true) {

                cansmalldash = false;
                timePassed = 0;
            }
            

        }
        if (can_jump == true)
        { 
                    cansmalldash = true;
                
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {

        //animController.SetBool("Jumping", false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //animController.SetBool("Jumping", false);
        IsGrounded = true;
        downJumping = false;
        CountJump = 2; //reset double saut quand on touche le sol


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //animController.SetBool("Jumping", true);
        IsGrounded = false;
        
    }
}