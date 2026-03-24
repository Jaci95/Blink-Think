using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
<<<<<<< HEAD

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public static bool isPaused;

    public InputAction cancel;

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
=======
public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenuUI;
    private bool isPaused;

    public void Start()
    {
        PauseMenuUI.SetActive(false);
>>>>>>> origin/Paul
    }

    public void toMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
<<<<<<< HEAD

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

=======
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
>>>>>>> origin/Paul
    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        PauseMenuUI.SetActive(true);
    }
<<<<<<< HEAD

=======
>>>>>>> origin/Paul
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        PauseMenuUI.SetActive(false);
    }
<<<<<<< HEAD
}
=======

}
>>>>>>> origin/Paul
