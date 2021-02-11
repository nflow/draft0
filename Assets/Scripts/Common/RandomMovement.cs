using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{

    public Vector3 dest;
    public float distance;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        newDestination();
    }

    // Update is called once per frame
    void Update()
    {
        var direction = (dest - transform.position).normalized;

        var others = Physics.OverlapSphere(transform.position, 5);
        foreach (var o in others) {
            var test = o.GetComponent<RandomMovement>();
            if (test) {
                var weight = 1.2f - Vector3.Distance(transform.position, o.transform.position) / 5;
                var directionOfTest = (transform.position - test.transform.position).normalized;
                direction =  (direction + directionOfTest * weight).normalized;
            }
        }

        var roataion =  Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, roataion, Time.deltaTime * speed * 100);

        transform.position += direction * Time.deltaTime * speed;
        
        distance = Mathf.Pow((dest.x - transform.position.x),2) + Mathf.Pow((dest.z - transform.position.z),2);
        if (distance < 0.5) {
            newDestination();
        }
    }

    void newDestination() {
       dest = new Vector3(Random.Range(-15,15),transform.position.y,Random.Range(-15,15));
    }
}
