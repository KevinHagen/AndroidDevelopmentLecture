using SaveSystem;
using UnityEngine;

namespace GameFlowSystem.States
{
	/// <summary>
	/// 	Waits for all loadables and then loads the save game.
	/// </summary>
	public class LoadState : MonoBehaviour, IGameState
	{
		#region IGameState Members

		public void StateEnter()
		{
		}

		public void StateExit()
		{
		}

		public IGameState StateUpdate()
		{
			if (!SaveManager.HasLoadables)
			{
				return this;
			}

			SaveManager.Load();

			return GameFlow.Instance.MainMenuState;
		}

		#endregion
	}
}