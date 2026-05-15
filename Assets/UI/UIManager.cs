using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    UIDocument document;

    VisualElement gameOverMenu;

    Button restartButton;

    void Awake()
    {
        document = GetComponent<UIDocument>();

        var root = document.rootVisualElement;

        gameOverMenu =
            root.Q<VisualElement>("game-over-menu");

        restartButton =
            root.Q<Button>("restart-button");

        restartButton.clicked += RestartGame;
    }

    public void ShowGameOver()
    {
        gameOverMenu.style.display =
            DisplayStyle.Flex;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}