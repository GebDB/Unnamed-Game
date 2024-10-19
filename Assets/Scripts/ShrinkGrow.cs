using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShrinkGrow : MonoBehaviour
{
    [SerializeField] float maxRadius = 200.0f;
    [SerializeField] float minRadius = 0.0f;
    [SerializeField] float changeRate = 0.1f;
    private bool grow;
    // Start is called before the first frame update
    void Start()
    {
        grow = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Intended for ring spell to shrink and grow repeatedly. WIP.
        if (grow) {
            if (transform.localScale.x < maxRadius)
            {
                transform.localScale += Vector3.one * changeRate;
            }
            else
            {
                grow = false;
            }
        }
        else
        {
            if (transform.localScale.x > minRadius)
            {
                transform.localScale -= Vector3.one * changeRate;
            }
            else
            {
                grow = true;
            }
        }

    }
}
