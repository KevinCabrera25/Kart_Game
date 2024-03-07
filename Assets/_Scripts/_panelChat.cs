using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FishNet.Broadcast;
using FishNet;
using FishNet.Connection;

public class _panelChat : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputChat;
    [SerializeField] TextMeshProUGUI _chatGlobalTexto;

    List<string> _historialChat = new List<string>(10);

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

    void OnMensajeBroadcast(MensajeBroadcast msg)
    {
        _historialChat.Add($"Player {msg._usuario}: {msg._mensaje}");
        if (_historialChat.Count >= 11) // Ya llegamos a nuestro limite
        {
            _historialChat.RemoveAt(0); // Borramos el mensaje mas viejo
        }

        ImprimirHistorialChat();
    }

    void OnMensajeBroadcastServidor(NetworkConnection conn, MensajeBroadcast msg)
    {
        msg._usuario = conn.ClientId.ToString();

        InstanceFinder.ServerManager.Broadcast(msg); // Enviamos el mensaje a TODOS los clientes
        // InstanceFinder.ServerManager.Broadcast(conn, msg); // Se puede usar el conn para enviar el mensaje solo al que nos mando mensaje

    }


    public void EnviarMensaje()
    {
        string _mensaje = _inputChat.text;

        // Si esta vacio no tiene caso agregarlo
        if (string.IsNullOrWhiteSpace(_mensaje))
            return;


        _inputChat.text = "";

        MensajeBroadcast msg = new MensajeBroadcast() // Generamos el paquete a enviar por red
        {
            _mensaje = _mensaje,
        };

        InstanceFinder.ClientManager.Broadcast(msg); // Envia este mensaje
    }


    void ImprimirHistorialChat()
    {
        _chatGlobalTexto.text = "";

        for (int i = 0; i < _historialChat.Count; i++)
        {
            _chatGlobalTexto.text += _historialChat[i];
            _chatGlobalTexto.text += "\n";
        }
    }

    public struct MensajeBroadcast : IBroadcast
    {
        public string _usuario;
        public string _mensaje;
    }


}
