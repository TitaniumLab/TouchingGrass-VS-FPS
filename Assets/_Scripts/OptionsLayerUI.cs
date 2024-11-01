using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GrassVsFps
{
    public class OptionsLayerUI : MonoBehaviour
    {
        [SerializeField] private Toggle _fpsToggle;
        [SerializeField] private TMP_Dropdown _displaysDropdown;
        [SerializeField] private TMP_Dropdown _screenModDropdown;

        #region Internal
        private void Start()
        {
            // FPS Toggle
            _fpsToggle.isOn = GameSettings.GetTargetFrameRateLock();
            _fpsToggle.onValueChanged.AddListener(OnFPSToggleChanged);

            // Displays
            SetDisplaysNames();
            _displaysDropdown.onValueChanged.AddListener(OnDisplayChanhed);

            // Screen mod
            SetDisplaysModsNames();
            _screenModDropdown.onValueChanged.AddListener(OnScreenModChanged);
        }


        private void OnDestroy()
        {
            _fpsToggle.onValueChanged.RemoveAllListeners();
            _displaysDropdown.onValueChanged.RemoveAllListeners();
            _screenModDropdown?.onValueChanged.RemoveAllListeners();
        }
        #endregion


        #region OnAction
        private void OnFPSToggleChanged(bool value)
        {
            GameSettings.EnableFrameRateCup(value);
        }


        private void OnDisplayChanhed(int value)
        {
            GameSettings.SetDisplay(value);
        }


        private void OnScreenModChanged(int value)
        {
            GameSettings.SetScreenMod(value);
            SetDisplaysModsNames();
        }
        #endregion
        //public void OnOptionsChangeCancel()
        //{
        //    gameObject.SetActive(false);
        //}




        private void SetDisplaysNames()
        {
            _displaysDropdown.ClearOptions();
            var displayLayout = GameSettings.DisplaysInfos;
            var options = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < displayLayout.Count; i++)
            {
                options.Add(new TMP_Dropdown.OptionData(displayLayout[i].name));
            }
            _displaysDropdown.AddOptions(options); // For some reason "_displaysDropdown.options.Add(...)" in loop doesn't set label
            var index = displayLayout.IndexOf(Screen.mainWindowDisplayInfo);
            _displaysDropdown.value = index;
        }


        private void SetDisplaysModsNames()
        {
            _screenModDropdown.ClearOptions();
            for (int i = 0; i < GameSettings.FullScreenModes.Length; i++)
            {
                var name = GameSettings.FullScreenModes[i].ToString();
                _screenModDropdown.options.Add(new TMP_Dropdown.OptionData(name));
            }
            var index = Array.FindIndex(GameSettings.FullScreenModes, mod => mod == Screen.fullScreenMode);
            _screenModDropdown.value = index;
        }
    }


    public struct SettingsData
    {
        public bool IsFPSLocked;
        public int DisplayIndex;
        public int ScreenModIndex;
    }
}
