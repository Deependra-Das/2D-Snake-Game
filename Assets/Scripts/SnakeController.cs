using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
   
    private Vector2 moveDirection;
    private float moveTimer;
    private float moveTimerMax;

    [SerializeField]
    private bool isShieldActive;

    [SerializeField]
    private bool isScoreMultiplierActive;

    [SerializeField]
    private bool isSpeedBoostActive;

    [SerializeField]
    private float powerupCooldownTimer;

    [SerializeField]
    private float SpeedMultiplier;

    private List<Transform> snakeSegmentList;
    public Transform snakeSegmentPrefab;

    [SerializeField]
    private GameUIManager gameUIManagerObject;

    private void Awake()
    {
        //moveDirection=Vector2.right;
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
        else if (other.gameObject.CompareTag("Powerup"))
        {
            HandleCollisionWithPowerup(other);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Player") && isShieldActive==false)
        {
            Debug.Log("Player Dead");
        }

    }

    private void HandleCollisionWithFood(Collider2D colliderFoodObject)
    {
        if (colliderFoodObject.gameObject.GetComponent<FoodController>() != null)
        {
            if(colliderFoodObject.gameObject.GetComponent<FoodController>().getFoodType()==FoodType.MassBurnerFood)
            {
                ReduceSnakeSize();
                gameUIManagerObject.IncreaseScore(FoodSpawnManager.Instance.GetFoodPoints(FoodType.MassBurnerFood));
            }
            else if (colliderFoodObject.gameObject.GetComponent<FoodController>().getFoodType() == FoodType.MassGainerFood)
            {
                GrowSnakeSize();
                gameUIManagerObject.IncreaseScore(FoodSpawnManager.Instance.GetFoodPoints(FoodType.MassGainerFood));
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

    private void HandleCollisionWithPowerup(Collider2D colliderPowerupObject)
    {
        if (colliderPowerupObject.gameObject.GetComponent<PowerupController>() != null)
        {
            switch(colliderPowerupObject.gameObject.GetComponent<PowerupController>().getPowerupType())
            {
                case PowerupType.ScoreMultiplierPowerup:
                    StartCoroutine(ActivateScoreMultiplier());
                    break;
                case PowerupType.ShieldPowerup:
                    StartCoroutine(ActivateShield());
                    break;
                case PowerupType.SpeedBoostPowerup:
                    StartCoroutine(ActivateSpeedBoost());
                    break;
            }
        }

    }

    private IEnumerator ActivateShield()
    {
        isShieldActive = true;
        Debug.Log("Shield Activated");
        yield return new WaitForSeconds(powerupCooldownTimer);

        isShieldActive = false;
        Debug.Log("Shield Dectivated");
    }

    private IEnumerator ActivateScoreMultiplier()
    {
        isScoreMultiplierActive = true;
        Debug.Log("Score Multiplier Activated");
        yield return new WaitForSeconds(powerupCooldownTimer);

        isScoreMultiplierActive = false;
        Debug.Log("Score Multiplier Dectivated");
    }
   
    private IEnumerator ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;
        moveTimerMax /= SpeedMultiplier;
        Debug.Log("Speed Boost Activated");
        yield return new WaitForSeconds(powerupCooldownTimer);

        isSpeedBoostActive = false;
        moveTimerMax *= SpeedMultiplier;
        Debug.Log("Speed Boost Dectivated");
    }

    public bool checkShieldStatus()
    { 
        return isShieldActive;
    }

    public bool checkScoreMultiplierStatus()
    {
        return isScoreMultiplierActive;
    }

    public bool checkSpeedBoostStatus()
    {
        return isSpeedBoostActive;
    }
}
