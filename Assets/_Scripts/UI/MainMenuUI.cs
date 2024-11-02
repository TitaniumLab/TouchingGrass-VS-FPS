using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace GrassVsFps
{
    public class MainMenuUI : MonoBehaviour
    {
        private GameObject _focusedLayer;

        #region Shortcuts
        public void OnFocusLayerClose(InputAction.CallbackContext callbackContext)
        {
            // Check button up
            if (callbackContext.canceled)
            {
                // If layer opened
                if (_focusedLayer != null && _focusedLayer.activeInHierarchy)
                {
                    CloseActiveLayer();
                }
                else
                {
                    ExitGame();
                }
            }
        }
        #endregion


        #region Methods
        public void ExitGame()
        {
            Application.Quit();
        }


        public void SetActive(GameObject uiElement)
        {
            _focusedLayer = uiElement;
            uiElement.SetActive(true);
        }

        public void CloseActiveLayer()
        {
            _focusedLayer.SetActive(false);
            _focusedLayer = null;
        }


        public void LoadScene(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
        }
        #endregion
    }
}
