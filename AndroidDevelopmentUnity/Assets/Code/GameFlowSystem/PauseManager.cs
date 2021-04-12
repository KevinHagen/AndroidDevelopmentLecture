using Core;
using UnityEngine;

namespace GameFlowSystem
{
	public class PauseManager : Singleton<PauseManager>
	{
		#region Public methods

		public void PauseGame()
		{
			Time.timeScale = 0f;
		}

		public void UnpauseGame()
		{
			Time.timeScale = 1f;
		}

		#endregion
	}
}