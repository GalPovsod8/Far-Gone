using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anim;
    [SerializeField] private AudioSource runningSound;

    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpForce = 6f; 
    float moveHorizontal;
    bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        //Animacija za hojo
        anim.SetFloat("speed", Mathf.Abs(moveHorizontal));
    }

    void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            HorizontalMovement();

            if(moveHorizontal > -0.1f)
            {
                transform.localScale = new Vector2(3f, 3f);
            }
            else
            {
                transform.localScale = new Vector2(-3f, 3f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetBool("isJumping", true);
            Jump();
        }
    }


    //METODE ZA PREMIKANJE
    void HorizontalMovement()
    {
        transform.position += new Vector3(moveHorizontal, 0, 0) * Time.deltaTime * moveSpeed; 

        //NASTAVIMO DA SE OB PRITISKU NA SHIFT POVECA HITROST (LAHKO TECEMO)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 6f;
            anim.SetBool("isRunning", true);

            runningSound.Play(); //////////////TO NE DELAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        }
        else
        {
            moveSpeed = 2f;
            anim.SetBool("isRunning", false);
            
            runningSound.Stop(); //////////////TO NE DELAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0.0f, 2.0f, 0.0f) * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    //PREVERJANJE CE JE IGRALEC NA TLEH DA LAHKO NA PODLAGI TEGA OMOGOCIMO OZ. ONEMOGOCIMO SKOK
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
    }
}
