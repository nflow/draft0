using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduction : MonoBehaviour
{
    public GameObject prefab;
    public float age;
    public float maxAge;
    public float reproducitonRate;
    public float nextChild;

    public float speed = 0.1F;
    private float startTime;

    void Start()
    {
        age = 0.0f;
        nextChild = reproducitonRate;
        maxAge = Random.Range(10,50);
        reproducitonRate = Random.Range(5,8);
        transform.localScale = Vector3.zero;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        nextChild -= Time.deltaTime;
        if (nextChild <= 0)
        {
            var child = Instantiate(prefab);
            child.name = prefab.name;
            child.transform.parent = prefab.transform.parent;
            nextChild = reproducitonRate * Mathf.Exp(Mathf.Pow(age / maxAge, 2));
            child.transform.position = transform.position;
        }

        if (age >= maxAge) {
            Destroy(gameObject);
        } else if (age > maxAge - 1) {
            transform.localScale -= Vector3.one * Time.deltaTime;
        } else if (age < 1) {
            transform.localScale += Vector3.one * Time.deltaTime;
        }
    }
}
