using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
   
    private Vector2 moveDirection;
    private float moveSpeed;
    private float moveDistance;
    private float moveTimer;
    private float moveTimerMax;

    private void Awake()
    {
        //moveDirection=Vector2.right;
        moveSpeed = 1.0f;
        moveDistance = 1.0f;
        moveTimerMax = 0.1f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && moveDirection != Vector2.down)
        {
            moveDirection = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.S) && moveDirection != Vector2.up)
        {
            moveDirection = Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.A) && moveDirection != Vector2.right) 
        {
            moveDirection = Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.D) && moveDirection != Vector2.left)
        {
            moveDirection = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        moveTimer += Time.fixedDeltaTime;
        if (moveTimer >= moveTimerMax)
        {
            this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + moveDirection.x,
            Mathf.Round(this.transform.position.y) + moveDirection.y,
            0.0f);

            moveTimer = 0f;
        }
    }

}
