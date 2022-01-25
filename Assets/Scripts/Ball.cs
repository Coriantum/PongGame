using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    private float Hitposition(Vector2 ballPosition, Vector2 racketPosition, float racketHeight){
        return (ballPosition.y - racketPosition.y) / racketHeight;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
    }
}
