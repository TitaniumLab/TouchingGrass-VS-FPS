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
        private bool _saveChanges; // If just set inactive -> don't save
        private SettingsData _oldSettings;


        #region Internal
        private void OnEnable()
        {
            // FPS Toggle
            _fpsToggle.isOn = GameSettings.GetTargetFrameRateLock();
            _fpsToggle.onValueChanged.AddListener(OnFPSToggleChanged);

            // Displays
            SetDisplaysDropdownOptions();
            SetDisplayesDropdownValue();
            _displaysDropdown.onValueChanged.AddListener(OnDisplayChanged);

            // Screen mod
            SetScreenModsDropdownNames();
            SetScreenModsDropdownValue();
            _screenModDropdown.onValueChanged.AddListener(OnScreenModChanged);

            _saveChanges = false;
            _oldSettings = new SettingsData
            {
                IsFPSLocked = _fpsToggle.isOn,
                DisplayIndex = _displaysDropdown.value,
                ScreenModIndex = _screenModDropdown.value
            };
        }


        private void OnDisable()
        {
            if (!_saveChanges)
            {
                _fpsToggle.isOn = _oldSettings.IsFPSLocked;
                _screenModDropdown.value = _oldSettings.ScreenModIndex;
                _displaysDropdown.value = _oldSettings.DisplayIndex;
            }

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


        public void OnDisplayChanged(int value)
        {
            GameSettings.SetDisplay(value);
        }


        public void OnScreenModChanged(int value)
        {
            GameSettings.SetScreenMod(value);
            SetDisplayesDropdownValue();
        }
        #endregion


        #region Methods
        private void SetDisplaysDropdownOptions()
        {
            _displaysDropdown.ClearOptions();
            var displayLayout = GameSettings.DisplaysInfos;
            var options = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < displayLayout.Count; i++)
            {
                options.Add(new TMP_Dropdown.OptionData(displayLayout[i].name));
            }
            _displaysDropdown.AddOptions(options); // Adding oprions in a loop sometimes doesn't work correctly
        }

        private void SetDisplayesDropdownValue()
        {
            var index = GameSettings.DisplaysInfos.IndexOf(Screen.mainWindowDisplayInfo);
            _displaysDropdown.value = index;
        }


        private void SetScreenModsDropdownNames()
        {
            _screenModDropdown.ClearOptions();
            var modes = GameSettings.FullScreenModes;
            var options = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < modes.Length; i++)
            {
                var name = modes[i].ToString();
                options.Add(new TMP_Dropdown.OptionData(modes[i].ToString()));
            }
            _screenModDropdown.AddOptions(options);
        }


        private void SetScreenModsDropdownValue()
        {
            var index = Array.FindIndex(GameSettings.FullScreenModes, mod => mod == Screen.fullScreenMode);
            _screenModDropdown.value = index;
        }


        public void Close(bool saveChanges)
        {
            _saveChanges = saveChanges;
            gameObject.SetActive(false);
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
