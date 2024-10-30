using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrassVsFps
{
    public class MainMenuUI : MonoBehaviour
    {
        private int load;
        private void Start()
        {
            load = PlayerPrefs.GetInt("GameLoads");
            load++;
            Debug.Log(load);
            Debug.Log(++load);
            if (load == 0)
            {
                PlayerPrefs.SetInt("GameLoads", load + 1);
                PlayerPrefs.Save();
                Debug.Log(PlayerPrefs.GetInt("GameLoads"));
            }
            else
            {
                load++;
                PlayerPrefs.SetInt("GameLoads", load);
                Debug.Log(load);
                PlayerPrefs.Save();
                Application.Quit();
            }
        }

        public void LoadScene(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
        }
    }
}
