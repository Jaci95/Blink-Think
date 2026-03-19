using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenuUI;
    private bool isPaused;

    public void Start()
    {
        PauseMenuUI.SetActive(false);
    }

    public void toMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
    public void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
    }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        PauseMenuUI.SetActive(true);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        PauseMenuUI.SetActive(false);
    }

}
