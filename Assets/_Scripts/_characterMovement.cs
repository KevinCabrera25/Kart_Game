using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class _characterMovement : NetworkBehaviour
{

    [SerializeField] float _velocity = 100;
    [SerializeField] float _steeringMax = 35;

    [SerializeField] float _rotationSpeed;


    Renderer _renderer;

    [SyncVar(OnChange = nameof(MiColorActualizado))] // Solo sincroniza variables de tipo dato
    Color miColor; // Para que simcronice, el SERVER es el que tiene que modificar

    void MiColorActualizado(Color _colorViejo, Color _nuevoColor, bool _asServer)
    {
        //_renderer.material.color = _nuevoColor; // miColor

    }

    /*
    [Header("Projectile")]
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] float _projectileVelocity;
    [SerializeField] Transform _projectileSpawnPoint;

    [Header("Vida")]
    [SerializeField] int _vidaMax;
    [SerializeField] Transform barraVerdeTf;
    */

    /*
    [SyncVar(OnChange = nameof(VidaActualizada))]
    public int _vida; // Cambiar la variable desde el inspector no lo sincroniza

    void VidaActualizada(int _prev, int _vidaActual, bool _asServer)
    {
        Debug.Log($"{name} sufrio dano y su nueva vida es: {_vidaActual}");
        if (_vidaActual < 0)
            _vidaActual = 0;
  
        barraVerdeTf.localScale = new Vector3(_vidaActual / (float) _vidaMax, 1f, 1f);
    }
    */

    // Start is called before the first frame update
    void Awake()
    {
        //_vida = _vidaMax;
        //_renderer = GetComponent<Renderer>();
    }


    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        if (base.Owner.IsLocalClient) // IsOwner
        {
           // _renderer.material.color = Color.green;
            name += "- local";

            // Unica modificacion para los Local Events
            GameObject.Find("LocalEvents").GetComponent<_localEvents>().EjecutarOnLocalPlayerSpawn(transform);
        }

        else
        {
            GetComponent<Transform>().position += new Vector3(10.0f, 0.0f, 0.0f);
            
        }
              
    }


    // Update is called once per frame
    void Update()
    {
        // Si es el personaje que fue designado (A controlar) 
        if (base.IsOwner == false) // Este es mi personaje?
            return;

        if (Input.GetKeyDown(KeyCode.C))
        {
            Color newColor = Random.ColorHSV();
            CambiarColorServidorRPC(newColor);         
            
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DispararServerRPC();
        }
        */

        /*
        Vector3 inputDirection = Vector3.zero;
        inputDirection.z = Input.GetAxis("Vertical");
        inputDirection.x = Input.GetAxis("Horizontal");
        transform.Translate(inputDirection * _velocity * Time.deltaTime);
        

        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 worldInputDirection = transform.TransformDirection(inputDirection);
        transform.Translate(worldInputDirection * _velocity * Time.deltaTime);
        

        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotationInput * _rotationSpeed * Time.deltaTime);

        float forwardInput = Input.GetAxis("Vertical");
        transform.Translate(transform.forward * forwardInput * _velocity * Time.deltaTime);
        */


        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotationInput * _rotationSpeed * Time.deltaTime);

        float forwardInput = Input.GetAxis("Vertical");
        forwardInput *= -1f; // Invertir la entrada vertical
        Vector3 forwardDirection = transform.forward;
        GetComponent<Rigidbody>().AddForce(forwardDirection * forwardInput * _velocity * Time.deltaTime, ForceMode.Force);



    }

    /*
    [ServerRpc]
    void DispararServerRPC()
    {
        GameObject _newProjectile = Instantiate(_projectilePrefab, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
        _newProjectile.GetComponent<Rigidbody>().velocity = _projectileSpawnPoint.forward * _projectileVelocity;

        // Actualizar cualquier variable que sea SyncVar antes de llamar "Spawn"


        // Sincroniza el spawn del GO
        FishNet.InstanceFinder.ServerManager.Spawn(_newProjectile); // Solo funciona si el GO tiene un "Network Object"
    }

    */

    // ERROR
    // ServerRPC - Que no se puede ejecutar porque no eres Owner (No es tuyo o no tienes autoridad)
    // [ServerRpc(RequireOwnership = false)] // Permite ejecutar la funcion aunque no sea mi personaje


    [ServerRpc] // La funcion se ejecuta en el lado del server
    void CambiarColorServidorRPC(Color _color)
    {
        // Manejo por el SERVER SyncVar
        miColor = _color;


        // CambiarColorRPC(_color); // Ya no es necesario porque el servidor lo controla
        // _renderer.material.color = _color; // Solucion rapida repetir codigo en server
    }

    // RunLocally: Ejecuta el codigo en esta misma pc, ejecuta el codigo tambien en el server

    [ObserversRpc(RunLocally = true)] // La funcion se ejecuta en todos los clientes // BufferLast = true carga mensajes antiguos
    void CambiarColorRPC(Color _color)
    {
        //_renderer.material.color = _color;

    }

}
