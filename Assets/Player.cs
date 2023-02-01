using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

     float moveSpeed_horizontal = 3000f;
     bool is_jumping = false;
    bool can_jump = false;
    float jumpForce = 10f;
    [Range(0, 1)] [SerializeField] float smooth_time = 0.05f;

    float startSpeed = 3000f;
    float targetSpeed;
    float timePassed;


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

        vertical_value = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump") && can_jump)
        {
            is_jumping = true;
            //animController.SetBool("jumping", true);
        }
    }
    void FixedUpdate()
    {
        if (is_jumping && can_jump)
        {
            is_jumping = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            can_jump = false;
        }


        timePassed += Time.fixedDeltaTime;
        targetSpeed = startSpeed * Mathf.Pow(0.01f, timePassed);
        Debug.Log(targetSpeed);
        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.05f);
     

        //Debug.Log(rb.velocity);
       Debug.Log(target_velocity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        can_jump = true;
        Debug.Log(collision.gameObject.tag);
        //animController.SetBool("jumping", false);
    }
}