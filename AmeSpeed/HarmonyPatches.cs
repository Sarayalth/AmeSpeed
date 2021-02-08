using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;

namespace AmeSpeed
{
	class HarmonyPatches
	{
        public static bool scoreBool = false;
        [HarmonyPatch(typeof(PlayerScript), "Dead")]
        [HarmonyPrefix]
        static bool DeadPatch(PlayerScript __instance)
        {
            LevelLoader.loader.RestartLevel();
            return false;
        }

        [HarmonyPatch(typeof(PlayerScript), "Update")]
        [HarmonyPrefix]
        static void FastRestart(ref float ___restartTimer)
        {
            if (___restartTimer > 0)
                ___restartTimer = 2f;
        }

        [HarmonyPatch(typeof(LevelClearButton), "Update")]
        [HarmonyPrefix]
        static bool GetScore(ref bool ___victory, ref LevelClearButton __instance)
        {
            if(!___victory)
			{
                return true;
			}
            if(___victory && scoreBool && !MusicLooper.looper.MusicPlaying() && !MainScript.loading)
			{
                LevelLoader.loader.LoadLevel(__instance.level);
                UnityEngine.Object.Destroy(__instance);
            }
            if (___victory && !scoreBool)
			{
                scoreBool = true;
                TimeSpan timeSpan = TimeSpan.FromSeconds((double)Plugin.main.levelTime);
                var stringTime = string.Format("{0:00}:{1:00}.{2:000}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                if (Plugin.times.Count() == 0)
				{
                    Plugin.times.Add(timeSpan);
                    __instance.timeText.text = stringTime;
                }
				else
				{
                    if (timeSpan <= Plugin.times.Min())
					{
                        __instance.timeText.text = "NEW PB! " + stringTime;
                    }
					else
					{
                        __instance.timeText.text = stringTime;
                    }
                    Plugin.times.Add(timeSpan);
                }
            }
            return false;
        }
    }
}
