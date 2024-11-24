using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
<<<<<<< Updated upstream
    public void PlayGame()
=======
    public void StartGame()
>>>>>>> Stashed changes
    {
        SceneManager.LoadSceneAsync(0);
    }

<<<<<<< Updated upstream
    // Update is called once per frame
=======
>>>>>>> Stashed changes
    public void QuitGame()
    {
        Application.Quit();
    }
}
