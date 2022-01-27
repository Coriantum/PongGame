using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    // Contador de goles
    [SerializeField] private int golesIzquierda = 0;
    [SerializeField] private int golesDerecha = 0;

    //Cajas de texto de los contadores
    [SerializeField] private Text contadorIzquierda;
    [SerializeField] private Text contadorDerecha;



    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();
    }

    //Metodo para calcular la direccion Y (devuelve un numero)
    private float Hitposition(Vector2 ballPosition, Vector2 racketPosition, float racketHeight){
        return (ballPosition.y - racketPosition.y) / racketHeight;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.name == "RaquetaIzquierda"){
            float y = Hitposition(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 dir = new Vector2(1 , y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        if(col.gameObject.name == "RaquetaDerecha"){
            float y = Hitposition(transform.position, col.transform.position, col.collider.bounds.size.y);
            
            //Vector direccion
            Vector2 dir = new Vector2(-1 , y).normalized;
            
            //Aplico velocidad
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
    }



      // Metodo que reinicia la bola
    public void reiniciarBola(string direccion){
        transform.position = Vector2.zero;
        speed = 30;

        //Velocidad y direcion
        if(direccion == "Derecha"){
            golesDerecha ++;
            contadorDerecha.text = golesDerecha.ToString();

            //reinicio bola
            GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        }else if(direccion == "Izquierda"){
            golesIzquierda ++;
            contadorIzquierda.text = golesIzquierda.ToString();

            //reinicio bola
            GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        }

        
    }
}
