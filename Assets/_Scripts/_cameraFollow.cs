using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    public Transform objToFollow;
    public Vector3 offset;
    public float rotationDampingFactor = 10f;
    public float tiltAngle = 30f; // Nuevo campo para la inclinaci�n de la c�mara

    void LateUpdate()
    {
        if (!objToFollow)
            return;

        // Calculamos la rotaci�n deseada de la c�mara basada en la rotaci�n del objeto a seguir
        Quaternion desiredRotation = objToFollow.rotation;

        // Ajustamos la rotaci�n deseada para que la c�mara mire hacia adelante
        desiredRotation *= Quaternion.Euler(-tiltAngle, 180, 0); // Giramos 180 grados para que la c�mara mire hacia adelante

        // Aplicamos un amortiguamiento para suavizar la rotaci�n de la c�mara
        float rotationDamping = rotationDampingFactor;
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationDamping);

        // Obtenemos la posici�n del objeto a seguir
        Vector3 targetPosition = objToFollow.position - objToFollow.forward * offset.z + objToFollow.up * (offset.y + tiltAngle) + objToFollow.right * offset.x; // Incluimos el nuevo offset de inclinaci�n

        // Aplicamos la posici�n al objeto a seguir teniendo en cuenta la rotaci�n y el offset
        transform.position = targetPosition;
    }

    public void SetObjectToFollow(Transform transformToFollow)
    {
        objToFollow = transformToFollow;
    }
}
