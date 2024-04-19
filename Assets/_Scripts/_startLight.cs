using FishNet.Object;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TrafficLightController : NetworkBehaviour
{
    public Renderer light1Renderer;
    public Renderer light2Renderer;
    public Renderer light3Renderer;

    [ServerRpc]
    private void Start()
    {
        if (base.NetworkManager.IsServer)
        {
            StartCoroutine(ChangeLights());
        }
    }

    private IEnumerator ChangeLights()
    {
        while (true)
        {
            // Cambiar a luz roja
            SetLightColor(Color.red);
            yield return new WaitForSeconds(5f);

            // Cambiar a luz amarilla
            SetLightColor(Color.yellow);
            yield return new WaitForSeconds(2f);

            // Cambiar a luz verde
            SetLightColor(Color.green);
            yield return new WaitForSeconds(5f);
        }
    }

    private void SetLightColor(Color color)
    {
        light1Renderer.material.color = color;
        light2Renderer.material.color = color;
        light3Renderer.material.color = color;
    }
}
