using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace AmeSpeed
{
	class Timer : MonoBehaviour
	{
		public TextMeshProUGUI text;
		private void Start()
		{
			text = this.GetComponent<TextMeshProUGUI>();
		}
		private void Update()
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)Plugin.main.levelTime);
			this.text.text = string.Format("{0:00}:{1:00}.{2:000}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
		}
	}
}
