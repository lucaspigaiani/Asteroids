﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed;
    public float topSpeed;
    public float rotationSpeed;

    [SerializeField] private Transform player;
    [SerializeField] private Main_Controller main_Controller;
    [SerializeField] private Rigidbody2D rigidBody2D;

    private Vector2 movementAxisReceptor;

    private const string horizontalAxis = "Horizontal";
    private const string verticalAxis = "Vertical";

    void FixedUpdate()
    {
        LimitSpeed();
        if (!main_Controller.IsPlayerAlive())
        {
            ResetAxis();
            return;
        }
        ReceptMovementAxis();
        ApplyForce();
        ApplyRotation();
    }   

    private void LimitSpeed()
    {
        if (rigidBody2D.velocity.magnitude > topSpeed)
        {
            rigidBody2D.velocity = rigidBody2D.velocity.normalized * topSpeed;
        }   
    }

    private void ReceptMovementAxis()
    {
        movementAxisReceptor.x = Input.GetAxisRaw(horizontalAxis);
        movementAxisReceptor.y = Input.GetAxisRaw(verticalAxis);
    }

    private void ApplyForce()
    {
        if (Input.GetAxisRaw(verticalAxis) > 0)
        {
            rigidBody2D.AddForce(player.up * Time.deltaTime * moveSpeed * Input.GetAxis(verticalAxis));
        }
        if (Input.GetAxisRaw(verticalAxis) < 0)
        {
            rigidBody2D.AddForce(player.up * Time.deltaTime * moveSpeed * -0.5f);
        }
    }

    private void ApplyRotation()
    {
        rigidBody2D.MoveRotation(rigidBody2D.rotation + movementAxisReceptor.x * -1 * rotationSpeed * Time.deltaTime);
    }

    private void ResetAxis()
    {
        movementAxisReceptor.x = 0;
        movementAxisReceptor.y = 0;
    }

    public void ResetPlayerOrientation()
    {        
        player.position = new Vector2(0, -4);
        player.rotation = new Quaternion(0, 0, 0, 0);
        rigidBody2D.velocity = new Vector2(0, 0);
    }
}
