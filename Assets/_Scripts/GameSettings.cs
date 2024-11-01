using System;
using System.Collections.Generic;
using UnityEngine;


namespace GrassVsFps
{
    public class GameSettings : MonoBehaviour
    {
        private static bool _isGameBooted = false;
        public static FullScreenMode[] FullScreenModes
        {
            get
            {
                return new FullScreenMode[]
                {
                    FullScreenMode.FullScreenWindow,
                    FullScreenMode.MaximizedWindow,
                    FullScreenMode.Windowed
                };
            }
        }

        public static List<DisplayInfo> DisplaysInfos
        {
            get
            {
                var displayLayout = new List<DisplayInfo>();
                Screen.GetDisplayLayout(displayLayout);
                return displayLayout;
            }
        }

        private void Awake()
        {
            if (!_isGameBooted)
            {
                OnGameBoot();
            }
        }


        private void OnGameBoot()
        {
            var loads = PlayerPrefs.GetInt("GameLoads");

            if (loads == 0)
            {
                OnFirstGameLoad();
            }
            else
            {
                EnableFrameRateCup(Convert.ToBoolean(PlayerPrefs.GetInt("FPSlock")));
            }
            PlayerPrefs.SetInt("GameLoads", ++loads);
            _isGameBooted = true;
        }


        private void OnFirstGameLoad()
        {
            EnableFrameRateCup(false);
            Application.targetFrameRate = -1;
        }


        public static bool GetTargetFrameRateLock()
        {
            var count = QualitySettings.vSyncCount;
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public static void EnableFrameRateCup(bool isEnabled)
        {
            if (isEnabled)
            {
                QualitySettings.vSyncCount = 1;
                PlayerPrefs.SetInt("FPSlock", 1);
            }
            else
            {
                QualitySettings.vSyncCount = 0;
                PlayerPrefs.SetInt("FPSlock", 0);
            }
        }


        public static void SetDisplay(int index)
        {
            var displayLayout = new List<DisplayInfo>();
            Screen.GetDisplayLayout(displayLayout);
            Screen.MoveMainWindowTo(displayLayout[index], Vector2Int.zero);
        }


        public static void SetScreenMod(int modIndex)
        {
            if (Screen.fullScreenMode == FullScreenMode.Windowed)
            {
                Screen.SetResolution(Screen.mainWindowDisplayInfo.width, Screen.mainWindowDisplayInfo.height, FullScreenModes[modIndex]);
            }
            else
            {
                Screen.fullScreenMode = FullScreenModes[modIndex];
            }
        }


        public static void SetResolution(Vector2Int resolution)
        {
        }
    }
}
