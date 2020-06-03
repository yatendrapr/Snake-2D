using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    #endregion

    #region Methods

    public void Play()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    #endregion
}
