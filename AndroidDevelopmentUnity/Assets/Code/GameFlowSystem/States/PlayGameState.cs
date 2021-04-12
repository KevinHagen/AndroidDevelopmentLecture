using ScoreSystem;
using UnityEngine;

namespace GameFlowSystem.States
{
	/// <summary>
	/// 	Game is being played
	/// </summary>
	public class PlayGameState : MonoBehaviour, IGameState
	{
		#region Serialize Fields

		[SerializeField] private PlayerHUDController _hud;

		#endregion

		#region IGameState Members

		public void StateEnter()
		{
			_hud.Show();
		}

		public void StateExit()
		{
			_hud.Hide();
		}

		public IGameState StateUpdate()
		{
			// escape pauses the game
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				return GameFlow.Instance.PauseGameState;
			}

			// player died -> game over
			if (GameFlow.Instance.Player.IsDead)
			{
				return GameFlow.Instance.GameOverState;
			}

			return this;
		}

		#endregion
	}
}