using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AmeSpeed
{
	[BepInPlugin("sarayalth.AmeDoBeFast", "AmeDoBeFast", "1.0.0.0")]
	public class Plugin : BaseUnityPlugin
	{
		public static MainScript main;
		public static List<TimeSpan> times = new List<TimeSpan>();
		void Awake()
		{
			var main2 = GameObject.Find("Main");
			main = main2.GetComponent<MainScript>();

			Harmony.CreateAndPatchAll(typeof(HarmonyPatches));
			AddTimer();

			SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
		}

		private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
		{
			HarmonyPatches.scoreBool = false;
		}

		void AddTimer()
		{
			var framerateGO = GameObject.Find("Framerate");
			var newTimerPos = new Vector3(framerateGO.transform.position.x, framerateGO.transform.position.y + 30, 0);
			var newTimerGO = Instantiate(framerateGO, newTimerPos, framerateGO.transform.rotation, framerateGO.transform.parent);
			newTimerGO.name = "TimerUI";
			var framerateObj = newTimerGO.GetComponent<FramerateScript>();
			DestroyImmediate(framerateObj);
			newTimerGO.AddComponent<Timer>();
		}
	}
}
