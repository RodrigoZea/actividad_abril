using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform start;
    public Transform finish;
    public float speed = 4f;
    private int pos = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime * pos);

        if (transform.position.x <= start.position.x) {
            pos = 1;
        }
        else if (transform.position.x >= finish.position.x)
        {
            pos = -1;
        }
    }
}
