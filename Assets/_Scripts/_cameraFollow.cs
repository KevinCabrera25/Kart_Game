using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    public Transform objToFollow;
    public Vector3 offset;
    public float rotationDampingFactor = 10f;
    public float tiltAngle = 30f; // Nuevo campo para la inclinación de la cámara

    void LateUpdate()
    {
        if (!objToFollow)
            return;

        // Calculamos la rotación deseada de la cámara basada en la rotación del objeto a seguir
        Quaternion desiredRotation = objToFollow.rotation;

        // Ajustamos la rotación deseada para que la cámara mire hacia adelante
        desiredRotation *= Quaternion.Euler(-tiltAngle, 180, 0); // Giramos 180 grados para que la cámara mire hacia adelante

        // Aplicamos un amortiguamiento para suavizar la rotación de la cámara
        float rotationDamping = rotationDampingFactor;
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationDamping);

        // Obtenemos la posición del objeto a seguir
        Vector3 targetPosition = objToFollow.position - objToFollow.forward * offset.z + objToFollow.up * (offset.y + tiltAngle) + objToFollow.right * offset.x; // Incluimos el nuevo offset de inclinación

        // Aplicamos la posición al objeto a seguir teniendo en cuenta la rotación y el offset
        transform.position = targetPosition;
    }

    public void SetObjectToFollow(Transform transformToFollow)
    {
        objToFollow = transformToFollow;
    }
}
