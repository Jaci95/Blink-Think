using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void QuitGame()
    {
        // using unity editor und dieser if check sind nur da damit es auch im editor geht nicht nur im build
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        //Application.Quit();
        Debug.Log("Quit Button Pressed");
    }
}
