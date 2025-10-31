using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Stats")]
    public int maxHealth = 5;
    private int currentHealth;
    private int collectedObjects = 0;

    [Header("UI References")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI objectsText;
    public TextMeshProUGUI keyText;

    [Header("Key Collection")]
    private bool hasKey = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // Inicializamos la vida
        currentHealth = maxHealth;
        // Inicializamos UI
        UpdateUI();
    }

    void Start()
    {
        UpdateUI();
    }

    public void CollectObject()
    {
        collectedObjects++;
        if (collectedObjects >= 5)
        {
            collectedObjects = 0;
            RestoreFullHealth();
        }
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateUI();
        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    private void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void CollectKey()
    {
        hasKey = true;
        UpdateUI();
    }

    public bool HasKey()
    {
        return hasKey;
    }

    private void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }

        if (objectsText != null)
        {
            objectsText.text = collectedObjects.ToString() + "/5";
        }

        if (keyText != null)
        {
            keyText.text = (hasKey ? "1" : "0") + "/1";
        }
    }
}