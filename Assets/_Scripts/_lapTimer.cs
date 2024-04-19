using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class _lapTimer : NetworkBehaviour
{
    public TMP_Text timeText;
    private float startTime;

    private void Start()
    {
        startTime = Time.time; // Guardar el tiempo de inicio
    }

    private void Update()
    {
        // Calcular el tiempo transcurrido desde el inicio
        float elapsedTime = Time.time - startTime;

        // Convertir el tiempo a minutos y segundos
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);

        // Actualizar el texto en el formato deseado
        timeText.text = string.Format("Lap Time: {0:00}:{1:00}", minutes, seconds);
    }
}
