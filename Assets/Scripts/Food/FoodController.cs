using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [SerializeField]
    private FoodType foodType;

    public FoodType getFoodType()
    {
        return foodType;
    }
}
