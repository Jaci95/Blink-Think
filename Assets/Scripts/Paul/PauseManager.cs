using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public static bool isPaused;

    public InputAction cancel;
    public GameObject VictoryScreen;
    [SerializeField] public GameObject VisionCanvas; // HIER NEU

    public void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);

        cancel = InputSystem.actions.FindAction("UI/Cancel");

        if (cancel == null)
        {
            Debug.Log("cancel action is null");
            return;
        }

        cancel.Enable();
    }

    public void toMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void Update()
    {
        if (cancel != null && cancel.WasPressedThisFrame())
        {
            if (isPaused)
                ContinueGame();
            else
                PauseGame();
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
    public void Victory()

        {
            VictoryScreen.SetActive(true);

            if (VisionCanvas != null)
            {
                Debug.Log("VisionCanvas wird ausgeblendet: " + VisionCanvas.name);
                VisionCanvas.SetActive(false);
            }
            else
            {
                Debug.Log("VisionCanvas ist NICHT zugewiesen!");
            }

        Time.timeScale = 0f;
    }
  
}