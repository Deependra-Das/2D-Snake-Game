using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawnManager : MonoBehaviour
{
    public BoxCollider2D powerupSpawnArea;
    bool isSpawning = false;
    public int minSpawnTime = 5;
    public int maxSpawnTime = 10;
    public PowerupItem[] powerupList;

    //private static PowerupSpawnManager instance;
    //public static PowerupSpawnManager Instance { get { return instance; } }

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    void Update()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnPowerup(UnityEngine.Random.Range(minSpawnTime, maxSpawnTime)));
        }
    }

    IEnumerator SpawnPowerup(float seconds)
    {

        yield return new WaitForSeconds(seconds);

        PowerupType powerupType = (PowerupType)UnityEngine.Random.Range(0, 3);
        PowerupItem powerupItem = GetPowerupItem(powerupType);

        if (powerupItem != null)
        {
            Vector3 spawnPosition = getRandomPosition();

            GameObject food = Instantiate(powerupItem.powerupPrefab, spawnPosition, Quaternion.identity, transform);

            Destroy(food, powerupItem.powerupLifeTime);
        }

        isSpawning = false;
    }

    private Vector3 getRandomPosition()
    {
        Bounds bounds = powerupSpawnArea.bounds;

        float xPos = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float yPos = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(Mathf.Round(xPos), Mathf.Round(yPos), 0.0f);
    }

    public PowerupItem GetPowerupItem(PowerupType powerupType)
    {
        PowerupItem item = Array.Find(powerupList, item => item.powerupType == powerupType);
        if (item != null)
        {
            return item;
        }
        return null;
    }

}

[Serializable]
public class PowerupItem
{
    public PowerupType powerupType;
    public GameObject powerupPrefab;
    public float powerupLifeTime = 5f;
}

public enum PowerupType
{
    ScoreMultiplierPowerup,
    ShieldPowerup,
    SpeedBoostPowerup,
}
