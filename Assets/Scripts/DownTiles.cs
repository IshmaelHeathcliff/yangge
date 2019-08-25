using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownTiles : MonoBehaviour
{
    public float downSpeed = 0.1f;
    public float refinement;

    private BoxCollider2D boxCollider;
    private bool moving = false;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    IEnumerator Movement()
    {
        while (true)
        {
            transform.Translate(Vector3.down * Time.deltaTime * downSpeed);
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.1) Destroy(gameObject);
        if (moving == false)
        {
            moving = true;
            StartCoroutine(Movement());
        }
    }
}
