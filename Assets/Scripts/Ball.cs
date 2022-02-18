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

    [SerializeField] private Text temporizador;

    AudioSource fuenteDeAudio;
    [SerializeField] private AudioClip audioGol, audioRaqueta, audioRebote;

    //Variable para contabilizar el tiempo
    private float tiempo = 180;



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

        //Si aún no se ha acabado el tiempo, decremento su valor y lo muestro en la caja de texto
        if(tiempo >= 0){
            tiempo -= Time.deltaTime;
            temporizador.text = FormatearTiempo(tiempo);

        //Si se ha acabado el tiempo, compruebo quién ha ganado y se acaba el juego
        }else{
            temporizador.text = "00:00";
            if(golesDerecha > golesIzquierda){
                resultado.text = "Jugador Derecha gana!\n pulsa I para volver a Inicio\n pulsa P para volver a jugar";
            }else if(golesIzquierda > golesDerecha){
                resultado.text = "Jugador Izquierda gana!\n pulsa I para volver a Inicio\n pulsa P para volver a jugar";
            }else{
                resultado.text = "Empate!!\n pulsa I para volver a Inicio\n pulsa P para volver a jugar ";
            }
        
            resultado.enabled = true;
            Time.timeScale = 0;
        }
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

    private string FormatearTiempo(float tiempo){
        string minutos= Mathf.Floor(tiempo/60).ToString("00");
        string segundos = Mathf.Floor(tiempo % 60).ToString("00");

        return minutos + ":" + segundos;
    }
}
