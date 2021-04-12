using GameFlowSystem.View;
using UnityEngine;

namespace GameFlowSystem.States
{
	/// <summary>
	/// 	Game is in main menu.
	/// </summary>
	public class MainMenuState : MonoBehaviour, IGameState
	{
		#region Private Fields

		private bool _startGame;

		#endregion

		#region Private methods

		private void StartGame()
		{
			_startGame = true;
		}

		#endregion

		#region IGameState Members

		public void StateEnter()
		{
			_startGame = false;
			MainMenuUI.OnStartGame += StartGame;
		}

		public void StateExit()
		{
			_startGame = false;
			MainMenuUI.OnStartGame -= StartGame;
		}

		public IGameState StateUpdate()
		{
			if (_startGame)
			{
				return GameFlow.Instance.StartGameState;
			}

			return this;
		}

		#endregion
	}
}