using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public const int SKILL_CAP = 100;

    [SerializeField]
    private int _carrierRating = 1;
    public int carrierRating
    {
        get => _carrierRating;
        set
        {
            if (value < SKILL_CAP)
            {
                _carrierRating = value;
            }
            else
            {
                _carrierRating = SKILL_CAP;
            }
        }
    }

    [SerializeField]
    private int _workerRating = 1;
    public int workerRating
    {
        get => _workerRating;
        set
        {
            if (value < SKILL_CAP)
            {
                _workerRating = value;
            }
            else
            {
                _workerRating = SKILL_CAP;
            }
        }
    }
}
