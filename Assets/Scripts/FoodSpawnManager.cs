using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnManager : MonoBehaviour
{
    public BoxCollider2D foodSpawnArea;
    bool isSpawning = false;
    public int minSpawnTime = 2;
    public int maxSpawnTime = 5;
    public FoodItem[] foodList;

    void Update()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnFood(UnityEngine.Random.Range(minSpawnTime, maxSpawnTime)));
        }
    }

    IEnumerator SpawnFood(float seconds)
    {
        Debug.Log("Waiting for " + seconds + " seconds");

        yield return new WaitForSeconds(seconds);

        FoodType foodType = (FoodType)UnityEngine.Random.Range(0, 2);
        FoodItem foodItem = GetFoodItem(foodType);

        if (foodItem != null)
        {
            Vector3 spawnPosition = getRandomPosition();

            GameObject food = Instantiate(foodItem.foodPrefab, spawnPosition, Quaternion.identity, transform);

            Destroy(food, foodItem.foodLifeTime);
        }

        isSpawning = false;
    }

    private Vector3 getRandomPosition()
    {
        Bounds bounds = foodSpawnArea.bounds;

        float xPos = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float yPos = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(Mathf.Round(xPos),Mathf.Round(yPos), 0.0f);
    }

    private FoodItem GetFoodItem(FoodType foodType)
    {
        FoodItem item = Array.Find(foodList, item => item.foodType == foodType);
        if(item!=null)
        {
            return item;
        }
        return null;
    }

}

[Serializable]
public class FoodItem
{
    public FoodType foodType;
    public GameObject foodPrefab;
    public int changeInLength = 0;
    public int pointsScored = 0;
    public float foodLifeTime = 5f;
}

public enum FoodType
{
    MassBurnerFood,
    MassGainerFood,
}