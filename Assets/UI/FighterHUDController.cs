using UnityEngine;
using UnityEngine.UIElements;

public class FighterHUDController : MonoBehaviour
{
    [Header("References")]
    public Health playerHealth;
    public Health enemyHealth;

    ProgressBar playerBar;
    ProgressBar enemyBar;

    Label playerText;
    Label enemyText;

    void Start()
    {
        var root =
            GetComponent<UIDocument>().rootVisualElement;

        playerBar =
            root.Q<ProgressBar>("player-health");

        enemyBar =
            root.Q<ProgressBar>("enemy-health");

        playerText =
            root.Q<Label>("player-health-text");

        enemyText =
            root.Q<Label>("enemy-health-text");

        playerHealth.OnHealthChanged += UpdatePlayerHealth;
        enemyHealth.OnHealthChanged += UpdateEnemyHealth;

        // Initialize immediately
        UpdatePlayerHealth(
            playerHealth.maxHealth,
            playerHealth.maxHealth
        );

        UpdateEnemyHealth(
            enemyHealth.maxHealth,
            enemyHealth.maxHealth
        );
    }

    void UpdatePlayerHealth(float current, float max)
    {
        float percent = (current / max) * 100f;

        playerBar.value = percent;

        if (playerText != null)
        {
            playerText.text =
                $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";
        }
    }

    void UpdateEnemyHealth(float current, float max)
    {
        float percent = (current / max) * 100f;

        enemyBar.value = percent;

        if (enemyText != null)
        {
            enemyText.text =
                $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";
        }
    }

    void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= UpdatePlayerHealth;

        if (enemyHealth != null)
            enemyHealth.OnHealthChanged -= UpdateEnemyHealth;
    }
}