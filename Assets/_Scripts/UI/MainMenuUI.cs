using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrassVsFps
{
    public class MainMenuUI : MonoBehaviour
    {
        public void ExitGame()
        {
            Application.Quit();
        }

        public void SetActive(GameObject uiElement)
        {
            uiElement.SetActive(true);
        }

        public void LoadScene(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
        }
    }
}
