using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed;
    public float topSpeed;
    public float rotationSpeed;

    private Main_Controller main_Controller;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        main_Controller = GameObject.FindGameObjectWithTag("Main_Controller").GetComponent<Main_Controller>();
    }

    void Update()
    {
        if (main_Controller.PlayerAlive() == true)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else 
        {
            movement.x = 0;
            movement.y = 0;
        }
        
    }
    
    void FixedUpdate()
    {
        if (main_Controller.PlayerAlive() == true)
        {
            if (rb.velocity.magnitude > topSpeed)
                rb.velocity = rb.velocity.normalized * topSpeed;
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                rb.AddForce(transform.up * Time.deltaTime * moveSpeed * Input.GetAxis("Vertical"));
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                rb.AddForce(transform.up * Time.deltaTime * moveSpeed * -0.5f);
            }
            rb.MoveRotation(rb.rotation + movement.x * -1 * rotationSpeed * Time.deltaTime);
        }
    }


    public void ResetGame()
    {
        
        this.transform.position = new Vector2(0, -4);
        this.transform.rotation = new Quaternion(0, 0, 0, 0);
        rb.velocity = new Vector2(0, 0);
    }


}
