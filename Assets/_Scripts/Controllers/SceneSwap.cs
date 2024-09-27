using UnityEngine.SceneManagement;

namespace GrassVsFps
{
    public class SceneSwap
    {
        public static void ToNextScene()
        {
            var curIndex = SceneManager.GetActiveScene().buildIndex;
            // 0 scene always Main Menu
            var nextIndex = curIndex % (SceneManager.sceneCountInBuildSettings - 1) + 1;
            SceneManager.LoadScene(nextIndex);
        }
    }
}
