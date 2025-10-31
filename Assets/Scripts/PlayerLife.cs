using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public float damageInvulnerabilityTime = 1.0f; // Tiempo de invulnerabilidad despu�s de recibir da�o
    private bool canTakeDamage = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy") && canTakeDamage)
        {
            // Aplicar da�o
            GameManager.instance.TakeDamage(1);

            // Iniciar per�odo de invulnerabilidad
            StartCoroutine(DamageInvulnerability());
        }
    }

    private IEnumerator DamageInvulnerability()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageInvulnerabilityTime);
        canTakeDamage = true;
    }
}