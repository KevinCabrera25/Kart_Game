using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using TMPro;
using FishNet.Object.Synchronizing;

public class _contadorTiempo : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI _contadorTxt;
    [SerializeField] GameObject _iniciarBtnGo;

    bool _isContadorRunning;
    uint _contadorInicioTick;

    [SyncObject]
    readonly SyncTimer _contadorSync = new SyncTimer(); // Necesita readonly por la variable propia de Fishnet

    void Start()
    {
        _contadorSync.OnChange += OnContadorSyncChange;
    }

    void Update()
    {
        if (_isContadorRunning == false)
            return;

        _contadorSync.Update(Time.deltaTime); // Actualizamos el tiempo
        float _contador = _contadorSync.Remaining;

        _contadorTxt.text = _contador.ToString("F2"); // F2 => 2 decimales maximo

    }

    public void IniciarContador()
    {
        _contadorSync.StartTimer(10f);
        /*
        _contadorSync.PauseTimer();
        _contadorSync.UnpauseTimer();
        _contadorSync.StopTimer();
        _contadorSync.StartTimer(5f); // Si se llama  StartTimer y aun no termina se llama un Stop seguido de un Start
        */
    }

    void OnContadorSyncChange(SyncTimerOperation _op, float _prev, float _next, bool _asServer)
    {
        switch (_op)
        {
            case SyncTimerOperation.Start:
                // next => Tiempo Inicial ejemplo actual 10f
                _isContadorRunning = true;
                break;
            case SyncTimerOperation.Pause: // Local, el tiempo se pauso (Server)
                break;
            case SyncTimerOperation.PauseUpdated: // Clientes: Se recibe una pausa, y el Server nos dijo cuanto queda exactamente
                // next => Cuanto tiempo queda
                break;
            case SyncTimerOperation.Unpause:
                break;
            case SyncTimerOperation.Stop: // Server: se llamo Stop
                break;
            case SyncTimerOperation.StopUpdated: // Cliente: Se recibe un Stop
                // next => Cuanto tiempoo faltaba cuando se detiene
                break;
            case SyncTimerOperation.Finished: // Tiempo llego a 0
                Debug.Log("Contador Acabado");
                _isContadorRunning = false;
                break;
            case SyncTimerOperation.Complete: // Se termino de procesar todos los eventos
                break;
        }
    }




    /*
    public void IniciarContador()
    {
        _contadorInicioTick = base.TimeManager.Tick;
        IniciarContadorRPC(_contadorInicioTick); // base.TimeManager.Tick => Tiempo en el que se encuentra en este momento el Server
        _isContadorRunning = true;
    }
    */

    [ObserversRpc]
    void IniciarContadorRPC(uint _serverTick)
    {
        _contadorInicioTick = _serverTick;
        _isContadorRunning = true;
    }

    /*
    void Update()
    {
        if(_isContadorRunning == false)
           return;

        float _passedTime = (float)base.TimeManager.TimePassed(_contadorInicioTick);
        float _contador = 10f - _passedTime;



        if (_contador <= 0f)
        {
            _contador = 0f;
            _isContadorRunning = false;
            Debug.Log("Contador ya acabo");
        }

        _contadorTxt.text = _contador.ToString("F2"); // F2 => 2 decimales maximo

    }
    */

    
    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        if(base.IsServer == false) // El boton solo es visible para el servidor, ocultamos para todos los clientes
            _iniciarBtnGo.SetActive(false);
    }
   
}
