using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float moveSpeed = 2f;
    public float patrolDistance = 3f; // Distancia que se moverá a cada lado

    [Header("References")]
    private SpriteRenderer spriteRenderer;

    // Variables de control
    private Vector3 startPosition;
    private bool movingRight = true;
    private float leftLimit;
    private float rightLimit;

    void Start()
    {
        // Obtener referencias
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Guardar posición inicial y calcular límites
        startPosition = transform.position;
        leftLimit = startPosition.x - patrolDistance;
        rightLimit = startPosition.x + patrolDistance;

        // Asegurarse que el enemigo tenga el tag correcto
        gameObject.tag = "enemy";
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Determinar dirección de movimiento
        if (transform.position.x >= rightLimit)
        {
            movingRight = false;
        }
        else if (transform.position.x <= leftLimit)
        {
            movingRight = true;
        }

        // Mover el enemigo
        float movement = moveSpeed * Time.deltaTime;
        if (movingRight)
        {
            transform.Translate(Vector2.right * movement);
            spriteRenderer.flipX = false;
        }
        else
        {
            transform.Translate(Vector2.left * movement);
            spriteRenderer.flipX = true;
        }
    }

    // Visualizar el rango de patrulla en el editor
    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(leftLimit, transform.position.y, 0),
                           new Vector3(rightLimit, transform.position.y, 0));
        }
    }
}