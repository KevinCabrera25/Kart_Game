using FishNet.Example.Scened;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _playerProjectile : NetworkBehaviour
{
    [SerializeField] float _tiempoDeVida = 5f;

    // [Client]
    [Server] // Solo se ejecuta en el servidor
    void Start()
    {
        if(base.IsServer == false)// Si no soy el servidor no hago nada
            return;

        Invoke(nameof(Destruir), _tiempoDeVida);
    }

    // Update is called once per frame
    void Destruir()
    {
        // Despawn funciona si tiene un "Network Object"
        Despawn(gameObject); // Despawn = Destroy de Multiplayer => FishNet.InstanceFinder.ServerManager.Despawn(gameObject)
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((base.IsServer == false))
            return;

        if (other.CompareTag("Player"))
        {
            /*
            if ((base.IsServer == false))
            {
                // Reproduce el sonido de impacto
            }*/

            /*
            other.GetComponent<_characterMovement>()._vida--;
            Despawn(gameObject);*/
        }
    }
}
