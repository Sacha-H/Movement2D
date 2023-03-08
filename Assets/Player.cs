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
    float smooth_time = 0.35f;


    float targetSpeed;
    float timePassed;

 
    [SerializeField] bool can_jump = false;
    bool downJumping = false;


    // FLY
    bool canfly = false;
    bool stopfly = false;
    float timeFly = 0;
     float flySpeed = 0.5f;





    [SerializeField] bool IsGrounded = false;
    [SerializeField] int CountJump = 2;
  


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //animController = GetComponent<Animator>();
        //Debug.Log(Mathf.Lerp(current, target, 0));
    }

    // Update is called once per frame
    void Update()
    {
        horizontal_value = Input.GetAxis("Horizontal");
        vertical_value = Input.GetAxis("Vertical");

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

        if (Input.GetButtonDown("Fly") && IsGrounded == false)
        { 
            canfly = true;
            Debug.Log("vol");

        }
        if (Input.GetButtonUp("Fly"))
        {

            stopfly = true;
            Debug.Log("pas vol");
        }
    }
    void FixedUpdate()
    {


        // FLY





        // MVT H
        if (IsGrounded == true)
        {
            smallDash();
        }
        else
        {
            moveSpeed_horizontal = 700f;
        }


        justgrouded();





        if (can_jump)
        {

            Jump();


        }

        if (downJumping || rb.velocity.y < 0 && canfly == false)
        {
            falling();
        }






        //Debug.Log(targetSpeed);
        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.05f);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 35);



        //Debug.Log(rb.velocity);
        //Debug.Log(target_velocity);

        if (canfly == true)
        {

            //if (vertical_value < 0)
            //{
            //    if (horizontal_value > 0.1)
            //    {
            //        horizontal_value = 1;
            //    Debug.Log("vrai");
            //    }
            //    if (horizontal_value < 0.1)
            //    {
            //        horizontal_value = -1;
            //    Debug.Log("vrai");

            //    }
            rb.gravityScale = 1; 

                Vector2 target_fly = new Vector2(horizontal_value * Time.fixedDeltaTime, 2 * Time.fixedDeltaTime);
                rb.velocity = Vector2.SmoothDamp(rb.velocity, target_fly, ref ref_velocity, 0.05f);
                rb.AddForce(rb.velocity * flySpeed, ForceMode2D.Impulse);
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, 20);








            Debug.Log(horizontal_value);
            Debug.Log(vertical_value);

            if (timeFly > 2)
            {
                stopfly = true;
            }

        }
        if (stopfly == true)
        {
            canfly = false;
            timeFly = 0;
            rb.gravityScale = 3;


        }
    }

    




    //    if (canfly == true)
    //    {
    //        Debug.Log("fonctionne");

    //        rb.velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, vertical_value * moveSpeed_horizontal * Time.fixedDeltaTime);

    //        timeFly += Time.fixedDeltaTime;

    //        if (timeFly > 2)
    //        {
    //            stopfly = true;
    //        }

    //    }

    //    if (stopfly == true)
    //    {
    //        canfly = false;
    //        timeFly = 0;


    //    }
    //}

    //void Fly()
    //{

        
    //}



    void Jump()
    {
        // le compteur de saut
       CountJump -= 1;
        stopfly = false;








        // premier saut


        //second 
        if (CountJump == 0 || IsGrounded == false)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce * 0.8f, ForceMode2D.Impulse);
            CountJump = 0;

        }

        if (CountJump == 1)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
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