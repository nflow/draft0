using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private const int EVASION_RADIUS = 5;
    private const int TURN_SPEED = 100;

    public delegate void DestinatinonReached(Vector3 dest);
    public event DestinatinonReached OnDestinationReached;

    [SerializeField]
    private bool _moving = true;

    private Character _character;
    [SerializeField]
    private Vector3 _destination;
    public Vector3 destination
    {
        get => _destination;
        set
        {
            _destination = value;
            _moving = true;
        }
    }

    public float speed = 5;

    private void Awake() {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        if (_moving)
        {
            if (Vector3.Distance(transform.position, destination) <= transform.localScale.z)
            {
                _moving = false;
                if (OnDestinationReached != null)
                {
                    OnDestinationReached(destination);
                }
            }
            else
            {
                speed = 5 + Mathf.Log(_character.carrierRating + 1) * 5;
                HandleMovement();
            }
        }
    }

    private void HandleMovement()
    {
        var direction = (destination - transform.position).normalized;

        var nearbyGameObjects = Physics.OverlapSphere(transform.position, EVASION_RADIUS);
        foreach (var nearbyObject in nearbyGameObjects)
        {
            var movement = nearbyObject.GetComponent<Movement>();
            if (movement && movement.enabled)
            {
                var weight = 1.25f - Vector3.Distance(transform.position, nearbyObject.transform.position) / EVASION_RADIUS;
                var directionOfNearbyObject = (transform.position - nearbyObject.transform.position).normalized;
                direction = (direction + directionOfNearbyObject * weight).normalized;
            }
        }

        var roataion = Quaternion.LookRotation(direction);
        var calcRotation = Quaternion.RotateTowards(transform.rotation, roataion, Time.deltaTime * speed * TURN_SPEED);
        transform.rotation = new Quaternion(0, calcRotation.y, 0, calcRotation.w);

        transform.position += direction * Time.deltaTime * speed;
    }
}
