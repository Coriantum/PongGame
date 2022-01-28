using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //Si pulsa la tecla P o hace clic izquierdo empieza el juego
        if(Input.GetKeyDown(KeyCode.P) || Input.GetMouseButtonDown(0)){
            SceneManager.LoadScene("Juego");
        }

        // Volver al inicio
        if(Input.GetKeyDown(KeyCode.I)){
            SceneManager.LoadScene("Inicio");
        }
    }
}
