using UnityEngine;

public class KeyCollectable : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.CollectKey();
            Destroy(gameObject);
        }
    }
}