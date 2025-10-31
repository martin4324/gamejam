using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public float damageInvulnerabilityTime = 1.0f; // Tiempo de invulnerabilidad después de recibir daño
    private bool canTakeDamage = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy") && canTakeDamage)
        {
            // Aplicar daño
            GameManager.instance.TakeDamage(1);

            // Iniciar período de invulnerabilidad
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