using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public float speed = 10f;
    public string axis;
    private void FixedUpdate() {
        float vertical = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0 , vertical) * speed;
    }

}
