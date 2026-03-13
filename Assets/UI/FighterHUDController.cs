using UnityEngine;
using UnityEngine.UIElements;

public class FighterHUDController : MonoBehaviour
{
    public Health playerHealth;
    public Health enemyHealth;

    ProgressBar playerBar;
    ProgressBar enemyBar;

    void Start()
    {
        // Get UI document
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Find UI elements by name
        playerBar = root.Q<ProgressBar>("player-health");
        enemyBar = root.Q<ProgressBar>("enemy-health");

        // Subscribe to health change events
        playerHealth.OnHealthChanged += UpdatePlayerHealth;
        enemyHealth.OnHealthChanged += UpdateEnemyHealth;
    }

    void UpdatePlayerHealth(float current, float max)
    {
        playerBar.value = (current / max) * 100f;
    }

    void UpdateEnemyHealth(float current, float max)
    {
        enemyBar.value = (current / max) * 100f;
    }
}