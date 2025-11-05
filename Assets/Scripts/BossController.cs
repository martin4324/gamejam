using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Referencias")]
    private Transform player;
    private Rigidbody2D rb;

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float chaseRange = 10f;

    [Header("Ataque Normal")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private int attackDamage = 10;
    private float lastAttackTime;

    [Header("Ataque Especial")]
    [SerializeField] private float specialAttackCooldown = 5f;
    [SerializeField] private float specialAttackChance = 0.3f; // 30% de probabilidad
    [SerializeField] private int specialAttackDamage = 25;
    [SerializeField] private float specialAttackRange = 4f;
    private float lastSpecialAttackTime;
    private bool isPerformingSpecialAttack = false;

    [Header("Animación (Opcional)")]
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Buscar al jugador por tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("No se encontró el jugador con tag 'Player'");
        }
    }

    void Update()
    {
        if (player == null || isPerformingSpecialAttack) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Perseguir al jugador si está en rango
        if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer();

            // Intentar ataque especial
            if (Time.time >= lastSpecialAttackTime + specialAttackCooldown)
            {
                if (Random.value <= specialAttackChance)
                {
                    StartSpecialAttack();
                    return;
                }
            }

            // Ataque normal si está cerca
            if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                NormalAttack();
            }
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // Voltear el sprite según la dirección
        if (spriteRenderer != null)
        {
            if (direction.x > 0)
                spriteRenderer.flipX = false;
            else if (direction.x < 0)
                spriteRenderer.flipX = true;
        }

        // Activar animación de caminar si existe
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
    }

    void NormalAttack()
    {
        lastAttackTime = Time.time;
        rb.velocity = Vector2.zero;

        Debug.Log("Boss realizó ataque normal");

        // Activar animación de ataque si existe
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Verificar si el jugador está en rango de ataque
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            DamagePlayer(attackDamage);
        }
    }

    void StartSpecialAttack()
    {
        lastSpecialAttackTime = Time.time;
        isPerformingSpecialAttack = true;
        rb.velocity = Vector2.zero;

        Debug.Log("Boss está preparando ataque especial");

        // Activar animación de ataque especial si existe
        if (animator != null)
        {
            animator.SetTrigger("SpecialAttack");
        }

        // Ejecutar el ataque especial después de un pequeño delay
        Invoke(nameof(ExecuteSpecialAttack), 0.5f);
    }

    void ExecuteSpecialAttack()
    {
        Debug.Log("Boss ejecutó ataque especial");

        // Ataque especial afecta en un área mayor
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= specialAttackRange)
        {
            DamagePlayer(specialAttackDamage);
        }

        // Terminar ataque especial
        Invoke(nameof(EndSpecialAttack), 0.5f);
    }

    void EndSpecialAttack()
    {
        isPerformingSpecialAttack = false;
    }

    void DamagePlayer(int damage)
    {
        // Verificar si el jugador puede recibir daño
        PlayerLife playerLife = player.GetComponent<PlayerLife>();
        if (playerLife != null && playerLife.canTakeDamage)
        {
            // Usar el GameManager para aplicar el daño
            if (GameManager.instance != null)
            {
                GameManager.instance.TakeDamage(damage);
                Debug.Log($"Boss hizo {damage} de daño al jugador");
            }
            else
            {
                Debug.LogError("No se encontró GameManager.instance");
            }
        }
    }

    // Método para recibir daño (opcional)
    public void TakeDamage(int damage)
    {
        Debug.Log($"Boss recibió {damage} de daño");
        // Aquí puedes agregar lógica de vida del boss
    }

    // Visualizar rangos en el editor
    void OnDrawGizmosSelected()
    {
        // Rango de persecución (azul)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Rango de ataque normal (amarillo)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Rango de ataque especial (rojo)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, specialAttackRange);
    }
}