/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FishNet.Broadcast;
using FishNet;
using FishNet.Connection;

public class _winner : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _winnerIs;

    private void OnEnable()
    {
        InstanceFinder.ClientManager.RegisterBroadcast<MensajeBroadcast>(OnMensajeBroadcast);
        InstanceFinder.ServerManager.RegisterBroadcast<MensajeBroadcast>(OnMensajeBroadcastServidor);


    }

    private void OnDisable()
    {
        // Tambien el Server se puede suscribir
        InstanceFinder.ServerManager.UnregisterBroadcast<MensajeBroadcast>(OnMensajeBroadcastServidor);
        InstanceFinder.ClientManager.UnregisterBroadcast<MensajeBroadcast>(OnMensajeBroadcast);

    }
}*/
