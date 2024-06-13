using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using UnityEngine.SceneManagement;
using FishNet.Managing.Scened;


public class _finishLine : NetworkBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entró en el Trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Has terminado la carrera");
            //SceneManager.LoadGlobalScenes("Win");

            SceneLoadData sceneLoadData = new SceneLoadData("Win");
            InstanceFinder.SceneManager.LoadGlobalScenes(sceneLoadData);
        }
    }
     /*
    [Server]
    private void LoadWinScene()
    {
        // Cambiar de escena
        SceneManager.LoadGlobalScenes("Win");
    }*/
}
