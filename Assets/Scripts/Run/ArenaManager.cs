using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public Health playerHealth;
    public Health enemyHealth;

    public UIManager uiManager;

    void Start()
    {
        playerHealth.OnDeath += OnPlayerDied;
        enemyHealth.OnDeath += OnEnemyDied;

        StartFight();
    }

    void StartFight()
    {
        Time.timeScale = 1f;

        playerHealth.ResetHealth();
        enemyHealth.ResetHealth();

        Debug.Log("Fight Started");
    }

    void OnPlayerDied(Health dead)
    {
        Time.timeScale = 0f;

        uiManager.ShowGameOver();
    }

    void OnEnemyDied(Health dead)
    {
        Debug.Log("PLAYER WON");

        Time.timeScale = 0f;
    }
}