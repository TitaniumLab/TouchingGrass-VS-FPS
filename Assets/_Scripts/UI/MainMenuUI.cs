using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrassVsFps
{
    public class MainMenuUI : MonoBehaviour
    {
        public void LoadScene(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
        }
    }
}
