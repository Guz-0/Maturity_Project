using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public GameObject spawner;
    private Collider2D myCollider;
    private Vector2 x;
    private Vector2 y;

    void Start()
    {
        myCollider = spawner.GetComponent<Collider2D>();

        x = myCollider.bounds.max;
        y = myCollider.bounds.min;
        //Debug.Log("MAX -> " + x + "\nMIN -> " + y);
    }

    // Update is called once per frame
    void Update()
    {
        x = myCollider.bounds.max;
        y = myCollider.bounds.min;
        //Debug.Log("MAX -> " + x + "\nMIN -> " + y);
    }
}
