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

    [SerializeField] private Text resultado;

    AudioSource fuenteDeAudio;
    [SerializeField] private AudioClip audioGol, audioRaqueta, audioRebote;



    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();

        resultado.enabled = false;
        Time.timeScale = 1;

        //Recupero componente audio source
        fuenteDeAudio = GetComponent<AudioSource>();
    }


    private void Update() {
        speed += 2 * Time.deltaTime;
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
            //Sonido Raqueta
            fuenteDeAudio.clip= audioRaqueta;
            fuenteDeAudio.Play();
        }

        if(col.gameObject.name == "RaquetaDerecha"){
            float y = Hitposition(transform.position, col.transform.position, col.collider.bounds.size.y);
            
            //Vector direccion
            Vector2 dir = new Vector2(-1 , y).normalized;
            
            //Aplico velocidad
            GetComponent<Rigidbody2D>().velocity = dir * speed;

            //Sonido Raqueta
            fuenteDeAudio.clip= audioRaqueta;
            fuenteDeAudio.Play();
        }

        if(col.gameObject.name == "Arriba" || col.gameObject.name == "Abajo"){
            fuenteDeAudio.clip= audioRebote;
            fuenteDeAudio.Play();
        }
    }



      // Método que reinicia la bola
    public void reiniciarBola(string direccion){
        transform.position = Vector2.zero;
        speed = 30;

        //Velocidad y direcion
        if(direccion == "Derecha"){
            golesDerecha ++;
            contadorDerecha.text = golesDerecha.ToString();

            //reinicio bola
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

            if(!ComprobarFinal()){
                GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            }

        }else if(direccion == "Izquierda"){
            golesIzquierda ++;
            contadorIzquierda.text = golesIzquierda.ToString();

            //reinicio bola
            GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;

            if(!ComprobarFinal()){
                GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
            }
        }

        //Sonido Gol
        fuenteDeAudio.clip = audioGol;
        fuenteDeAudio.Play();
    }

    bool ComprobarFinal(){
        //Si el de la izquierda  ha llegado a 5
        if(golesIzquierda == 5){
            resultado.text = "Jugador Izquierda gana!\n pulsa I para volver a Inicio\n pulsa P para volver a jugar";
            resultado.enabled = true;
            Time.timeScale= 0;
            return true;
        }else if(golesDerecha == 5){
            resultado.text = "Jugador Derecha gana!\n pulsa I para volver a Inicio\n pulsa P para volver a jugar";
            resultado.enabled = true;
            Time.timeScale= 0;
            return true;
        }else{
            return false;
        }
    }
}
