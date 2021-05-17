using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public const int SKILL_CAP = 100;

    public float age;
    public float maxAge;
    public float nextChild;

    [SerializeField]
    private float _carrierRating;
    public float carrierRating
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
    private float _workerRating;
    public float workerRating
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
