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
        private SettingsData _oldSettings;


        #region Internal
        private void OnEnable()
        {
            // FPS Toggle
            _fpsToggle.isOn = GameSettings.GetTargetFrameRateLock();
            _fpsToggle.onValueChanged.AddListener(OnFPSToggleChanged);

            // Displays
            SetDisplaysNames();
            _displaysDropdown.onValueChanged.AddListener(OnDisplayChanhed);

            // Screen mod
            SetScreenModsNames();
            _screenModDropdown.onValueChanged.AddListener(OnScreenModChanged);


            _oldSettings = new SettingsData
            {
                IsFPSLocked = _fpsToggle.isOn,
                DisplayIndex = _displaysDropdown.value,
                ScreenModIndex = _screenModDropdown.value
            };
        }


        private void OnDisable()
        {
            _fpsToggle.onValueChanged.RemoveAllListeners();
            _displaysDropdown.onValueChanged.RemoveAllListeners();
            _screenModDropdown.onValueChanged.RemoveAllListeners();
        }
        #endregion


        #region OnAction
        public void OnFPSToggleChanged(bool value)
        {
            GameSettings.EnableFrameRateCup(value);
        }


        public void OnDisplayChanhed(int value)
        {
            GameSettings.SetDisplay(value);
        }


        public void OnScreenModChanged(int value)
        {
            GameSettings.SetScreenMod(value);
            SetDisplaysNames();
        }


        public void OnOptionsChangeAccept()
        {
            gameObject.SetActive(false);
        }


        public void OnOptionsChangeCancel()
        {
            _fpsToggle.isOn = _oldSettings.IsFPSLocked;
            _screenModDropdown.value = _oldSettings.ScreenModIndex;
            _displaysDropdown.value = _oldSettings.DisplayIndex;

            gameObject.SetActive(false);
        }
        #endregion


        #region Methods
        private void SetDisplaysNames()
        {
            _displaysDropdown.ClearOptions();
            var displayLayout = GameSettings.DisplaysInfos;
            var options = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < displayLayout.Count; i++)
            {
                options.Add(new TMP_Dropdown.OptionData(displayLayout[i].name));
            }
            _displaysDropdown.AddOptions(options);
            var index = displayLayout.IndexOf(Screen.mainWindowDisplayInfo);
            _displaysDropdown.value = index;
        }


        private void SetScreenModsNames()
        {
            _screenModDropdown.ClearOptions();
            var options = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < GameSettings.FullScreenModes.Length; i++)
            {
                var name = GameSettings.FullScreenModes[i].ToString();
                options.Add(new TMP_Dropdown.OptionData(name));
            }
            _screenModDropdown.AddOptions(options);
            var index = Array.FindIndex(GameSettings.FullScreenModes, mod => mod == Screen.fullScreenMode);
            _screenModDropdown.value = index;
        }
        #endregion
    }


    internal struct SettingsData
    {
        public bool IsFPSLocked;
        public int DisplayIndex;
        public int ScreenModIndex;
    }
}
