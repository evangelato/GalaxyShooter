using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Speed variable of 8
    private float _speed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * _speed);

        if (transform.position.y > 8.0f) 
        {
            Destroy(this.gameObject);
        }
    }
}
