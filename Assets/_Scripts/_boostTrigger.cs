using FishNet.Object;
using System.Collections;
using UnityEngine;

public class BoostTrigger : NetworkBehaviour
{
    public float boostForce = 1000f; // Fuerza del boost
    public float boostDuration = 3f; // Duración del boost en segundos
    public float decelerationRate = 5f; // Tasa de desaceleración después del boost
    private bool boosting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ApplyBoost(other.GetComponent<Rigidbody>()));
        }
    }

    private IEnumerator ApplyBoost(Rigidbody rb)
    {
        if (!boosting)
        {
            boosting = true;
            Vector3 boostDirection = rb.velocity.normalized; // Usamos la velocidad actual del carro como dirección del boost
            rb.AddForce(boostDirection * boostForce, ForceMode.Impulse);

            yield return new WaitForSeconds(boostDuration);

            boosting = false;
            StartCoroutine(Decelerate(rb));
        }
    }

    private IEnumerator Decelerate(Rigidbody rb)
    {
        while (rb.velocity.magnitude > 0.1f)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * decelerationRate);
            yield return null;
        }

        rb.velocity = Vector3.zero;
    }
}
