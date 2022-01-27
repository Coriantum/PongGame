using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Porteria : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D ball) {
        if(ball.name == "Ball"){
            if(this.name == "Izquierda"){
                //Gol y reinicio bola
                ball.GetComponent<Ball>().reiniciarBola("Derecha");
            } 
            else if(this.name == "Derecha"){
                ball.GetComponent<Ball>().reiniciarBola("Izquierda");
            }
        }
    }
  
}
