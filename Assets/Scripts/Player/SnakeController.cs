using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] 
    SnakeID snakeID;

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

    [SerializeField]
    private int ScoreMultiplier;

    private List<Transform> snakeSegmentList;
    public Transform snakeSegmentPrefab;

    [SerializeField]
    private GameUIManager gameUIManagerObject;

    public BoxCollider2D SpawnArea;

    [SerializeField]
    private GameOverUIController gameOverObject;

    [SerializeField]
    private GamePauseUIController gamePauseObject;

    [SerializeField]
    private GameManager gameManagerObject;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject gamePausePanel;

    private void Awake()
    {
        //moveDirection=Vector2.right;
        moveTimerMax = 0.1f;
        gameOverPanel.SetActive(false);
        gamePausePanel.SetActive(false);
    }

    private void Start()
    {
        snakeSegmentList = new List<Transform>();
        snakeSegmentList.Add(this.transform);
    }

    private void Update()
    {
        if(snakeID==SnakeID.SNAKE_P1)
        {
            if (Input.GetKeyDown(KeyCode.W) && moveDirection != Vector2.down)
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
        if (snakeID == SnakeID.SNAKE_P2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && moveDirection != Vector2.down)
            {
                moveDirection = Vector2.up;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && moveDirection != Vector2.up)
            {
                moveDirection = Vector2.down;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && moveDirection != Vector2.right)
            {
                moveDirection = Vector2.left;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && moveDirection != Vector2.left)
            {
                moveDirection = Vector2.right;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePausePanel.SetActive(true);
            Time.timeScale = 0f;
            gamePauseObject.RefreshHighScore();
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
        WrapSnakeInBounds();
    }

    protected void WrapSnakeInBounds()
    {
        Bounds bounds = SpawnArea.bounds;

        Vector3 snakeHeadPosition = this.transform.position;

        if (snakeHeadPosition.x > bounds.max.x)
        {
            snakeHeadPosition.x = bounds.min.x;
        }
        else if (snakeHeadPosition.x < bounds.min.x)
        {
            snakeHeadPosition.x = bounds.max.x;
        }

        if (snakeHeadPosition.y > bounds.max.y)
        {
            snakeHeadPosition.y = bounds.min.y;
        }
        else if (snakeHeadPosition.y < bounds.min.y)
        {
            snakeHeadPosition.y = bounds.max.y;
        }

        this.transform.position = snakeHeadPosition;
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
        else if (other.gameObject.CompareTag("PlayerBody") && isShieldActive==false)
        {
            Debug.Log("Player Dead");
            if(gameManagerObject.GetPlayerCount() == 2)
            {
                gameUIManagerObject.SetScoreToZero(snakeID);
            }

            OnPlayerDeath(snakeID, false);

        }
        else if (other.gameObject.CompareTag("Player") && isShieldActive == false)
        {
            Debug.Log("Player Dead");
            OnPlayerDeath(snakeID , true);

        }

    }

    private void HandleCollisionWithFood(Collider2D colliderFoodObject)
    {
        if (colliderFoodObject.gameObject.GetComponent<FoodController>() != null)
        {
            if(colliderFoodObject.gameObject.GetComponent<FoodController>().getFoodType()==FoodType.MassBurnerFood)
            {
                AudioManager.Instance.PlayFoodSFX(AudioTypeList.massBurnerFoodEaten);
                FoodItem foodItem = FoodSpawnManager.Instance.GetFoodItem(FoodType.MassBurnerFood);
                ReduceSnakeSize(foodItem.changeInLength);
                gameUIManagerObject.UpdateScore(snakeID, foodItem.pointsScored);
            }
            else if (colliderFoodObject.gameObject.GetComponent<FoodController>().getFoodType() == FoodType.MassGainerFood)
            {
                AudioManager.Instance.PlayFoodSFX(AudioTypeList.massGainerFoodEaten);
                FoodItem foodItem = FoodSpawnManager.Instance.GetFoodItem(FoodType.MassGainerFood);
                GrowSnakeSize(foodItem.changeInLength);

                if (isScoreMultiplierActive)
                {
                    gameUIManagerObject.UpdateScore(snakeID, foodItem.pointsScored * ScoreMultiplier);
                }
                else 
                {
                    gameUIManagerObject.UpdateScore(snakeID, foodItem.pointsScored);
                }
            
            }
        }

    }


    private void GrowSnakeSize(int length)
    {
        for (int i = 0; i < length; i++)
        {
            Transform newSnakeSegment = Instantiate(this.snakeSegmentPrefab,new Vector3(-100f, -100f, 0f), new Quaternion(0f, 0f, 0f, 0f));
           // newSnakeSegment.position = snakeSegmentList[snakeSegmentList.Count - 1].position;

            snakeSegmentList.Add(newSnakeSegment);
        }

    }

    private void ReduceSnakeSize(int length)
    {
        if (snakeSegmentList.Count > 1)
        {
            for (int i = 0; i < length; i++)
            {
                Transform lastBodyPart = snakeSegmentList[snakeSegmentList.Count - 1];
                snakeSegmentList.RemoveAt(snakeSegmentList.Count - 1);
                Destroy(lastBodyPart.gameObject);
            }
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
        AudioManager.Instance.PlayPowerupSFX(AudioTypeList.powerUpActivated);
        Debug.Log("Shield Activated");
        yield return new WaitForSeconds(powerupCooldownTimer);
        AudioManager.Instance.PlayPowerupSFX(AudioTypeList.powerupDeactivated);
        isShieldActive = false;
        Debug.Log("Shield Dectivated");
    }

    private IEnumerator ActivateScoreMultiplier()
    {
        isScoreMultiplierActive = true;
        AudioManager.Instance.PlayPowerupSFX(AudioTypeList.powerUpActivated);
        Debug.Log("Score Multiplier Activated");
        yield return new WaitForSeconds(powerupCooldownTimer);
        AudioManager.Instance.PlayPowerupSFX(AudioTypeList.powerupDeactivated);
        isScoreMultiplierActive = false;
        Debug.Log("Score Multiplier Dectivated");
    }
   
    private IEnumerator ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;
        moveTimerMax /= SpeedMultiplier;
        AudioManager.Instance.PlayPowerupSFX(AudioTypeList.powerUpActivated);
        Debug.Log("Speed Boost Activated");
        yield return new WaitForSeconds(powerupCooldownTimer);
        AudioManager.Instance.PlayPowerupSFX(AudioTypeList.powerupDeactivated);
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

    private void OnPlayerDeath(SnakeID snakeID, bool headToHeadCollision)
    {
        AudioManager.Instance.MuteAudioSource(AudioSourceList.audioSourceBGM, true);
        AudioManager.Instance.PlayMenuSFX(AudioTypeList.death);
        gameOverPanel.SetActive(true);
        gameOverObject.SetWinnerScore(snakeID, headToHeadCollision);
        Time.timeScale = 0f;
        gameManagerObject.KillAllPlayers();
    }

}
