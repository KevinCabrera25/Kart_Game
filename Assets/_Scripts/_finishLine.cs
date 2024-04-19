using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;

public class _finishLine : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entró en el Trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Has terminado la carrera");
        }
    }
}
