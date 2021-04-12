using UnityEngine;

namespace GameFlowSystem.States
{
	/// <summary>
	///  	Game is being paused
	/// </summary>
	public class PauseGameState : MonoBehaviour, IGameState
	{
		#region IGameState Members

		public void StateEnter()
		{
			PauseManager.Instance.PauseGame();
		}

		public void StateExit()
		{
			PauseManager.Instance.UnpauseGame();
		}

		public IGameState StateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				return GameFlow.Instance.PlayGameState;
			}

			return this;
		}

		#endregion
	}
}