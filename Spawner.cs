using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject knife;
    private float minX = -2.7f, maxX = 2.7f;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning() {
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        GameObject k = Instantiate(knife);
        float x = Random.Range(minX, maxX);
        k.transform.position = new Vector2(x, transform.position.y);
        StartCoroutine(StartSpawning());
    }
} // class
