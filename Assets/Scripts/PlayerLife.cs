using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public float damageInvulnerabilityTime = 1.0f;
    public bool canTakeDamage = true; // Cambiar de private a public

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy") && canTakeDamage)
        {
            GameManager.instance.TakeDamage(1);
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