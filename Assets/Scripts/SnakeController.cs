using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
   
    private Vector2 moveDirection;
    private float moveSpeed;
    private float moveDistance;
    private float moveTimer;
    private float moveTimerMax;

    private List<Transform> snakeSegmentList;
    public Transform snakeSegmentPrefab;

    private void Awake()
    {
        //moveDirection=Vector2.right;
        moveSpeed = 1.0f;
        moveDistance = 1.0f;
        moveTimerMax = 0.1f;
    }

    private void Start()
    {
        snakeSegmentList = new List<Transform>();
        snakeSegmentList.Add(this.transform);
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
            for (int i = snakeSegmentList.Count-1; i > 0; i--)
            {
                snakeSegmentList[i].position = snakeSegmentList[i-1].position;
            }

            this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + moveDirection.x,
            Mathf.Round(this.transform.position.y) + moveDirection.y,
            0.0f);

            moveTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            HandleCollisionWithFood(other);

            Destroy(other.gameObject);
        }
    }

    private void HandleCollisionWithFood(Collider2D colliderFoodObject)
    {
        if (colliderFoodObject.gameObject.GetComponent<FoodController>() != null)
        {
            if(colliderFoodObject.gameObject.GetComponent<FoodController>().getFoodType()==FoodType.MassBurnerFood)
            {
                ReduceSnakeSize();
            }
            else if (colliderFoodObject.gameObject.GetComponent<FoodController>().getFoodType() == FoodType.MassGainerFood)
            {
                GrowSnakeSize();
            }
        }

    }


    private void GrowSnakeSize()
    {
    Transform newSnakeSegment = Instantiate(this.snakeSegmentPrefab);
    newSnakeSegment.position = snakeSegmentList[snakeSegmentList.Count - 1].position;

    snakeSegmentList.Add(newSnakeSegment);

    }

    private void ReduceSnakeSize()
        {
        if (snakeSegmentList.Count > 1)
        { 
        Transform lastBodyPart = snakeSegmentList[snakeSegmentList.Count - 1];
        snakeSegmentList.RemoveAt(snakeSegmentList.Count - 1);

        Destroy(lastBodyPart.gameObject);
        }

    }

}
