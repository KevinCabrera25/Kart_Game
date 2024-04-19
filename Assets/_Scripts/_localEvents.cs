using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class _localEvents : MonoBehaviour
{ 
    // public Action<Transform> OnLocalPlayerSpawn; // Action es de C#

    public UnityEvent<Transform> OnLocalPlayerSpawn; // Propio de Unity, hace lo mismo

    public void EjecutarOnLocalPlayerSpawn(Transform _playerTransform)
    {
        Debug.Log("Jugador local spawneo");
        OnLocalPlayerSpawn.Invoke(_playerTransform);
    }
}
