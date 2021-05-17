using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduction : MonoBehaviour
{
    public GameObject prefab;

    private Character character;
    private float startTime;
    public float minReproducitonRate = 15.0f;
    public float maxReproducitonRate = 20.0f;
    public float minAge = 15.0f;
    public float maxAge = 40.0f;

    void Start()
    {
        this.character = GetComponent<Character>();
        this.transform.localScale = Vector3.zero;
        this.startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        character.age += Time.deltaTime;
        character.nextChild -= Time.deltaTime;
        if (character.nextChild <= 0)
        {
            SpawnChild();
            character.nextChild = Random.Range(minReproducitonRate, maxReproducitonRate) * Mathf.Exp(Mathf.Pow(character.age / character.maxAge, 2));
        }

        if (character.age >= character.maxAge) {
            // TODO: Reuse for performace reasons
            Destroy(gameObject);
        } else if (character.age > character.maxAge - 1) {
            transform.localScale -= Vector3.one * Time.deltaTime;
        } else if (character.age < 1) {
            transform.localScale += Vector3.one * Time.deltaTime;
        }
    }

    public void SpawnChild() {
            var child = Instantiate(prefab);
            
            child.name = prefab.name;
            child.transform.parent = prefab.transform.parent;
            child.transform.position = transform.position;
            
            var childCharacter = child.AddComponent<Character>();
            childCharacter.age = 0.0f;
            childCharacter.maxAge = Random.Range(minAge, maxAge);
            childCharacter.nextChild = Random.Range(minReproducitonRate, maxReproducitonRate);

    
    }
}
