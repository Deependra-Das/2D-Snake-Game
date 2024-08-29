using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnManager : MonoBehaviour
{
    public BoxCollider2D spawnArea;
    public FoodItem[] foodList;
    bool isSpawning = false;
    public int minSpawnTime = 2;
    public int maxSpawnTime = 5;

    void Update()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnObject(UnityEngine.Random.Range(minSpawnTime, maxSpawnTime)));
        }
    }

    IEnumerator SpawnObject(float seconds)
    {
        Debug.Log("Waiting for " + seconds + " seconds");

        yield return new WaitForSeconds(seconds);

        FoodType foodType = (FoodType)UnityEngine.Random.Range(0, 2);
        FoodItem foodPrefab = GetFoodItem(foodType);

        if (foodPrefab != null)
        {
            Vector3 spawnPosition = getRandomPosition();

            GameObject food = Instantiate(foodPrefab.foodPrefab, spawnPosition, Quaternion.identity, transform);

            Destroy(food, foodPrefab.foodLifeTime);
        }

        isSpawning = false;
    }

    private Vector3 getRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;

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
    public int changeinLength = 0;
    public int pointsScored = 0;
    public float foodLifeTime = 5f;
}

public enum FoodType
{
    MassBurnerFood,
    MassGainerFood,
}