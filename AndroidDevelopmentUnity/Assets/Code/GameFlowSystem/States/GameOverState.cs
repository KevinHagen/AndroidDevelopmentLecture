using System;
using GameFlowSystem.View;
using ScoreSystem;
using UnityEngine;
using UnityEngine.Playables;

namespace GameFlowSystem.States
{
	/// <summary>
	/// 	Player has lost, game is over
	/// </summary>
	public class GameOverState : MonoBehaviour, IGameState
	{
		#region Static Stuff

		/// <summary>
		/// 	Invoked when the player has lost
		/// </summary>
		public static event Action GameOverStateEnter;

		#endregion

		#region Serialize Fields

		[SerializeField] private GameOverScreen _gameOverScreen;
		[SerializeField] private PlayableDirector _gameOverTimeline;

		#endregion

		#region Properties

		public bool Continue { get; set; }

		#endregion

		#region IGameState Members

		public void StateEnter()
		{
			Continue = false;
			Time.timeScale = 0;

			_gameOverScreen.Show();
			_gameOverScreen.SetScore(ScoreManager.Instance.Score);
			_gameOverTimeline.Play();

			GameOverStateEnter?.Invoke();
		}

		public void StateExit()
		{
		}

		public IGameState StateUpdate()
		{
			if (Continue)
			{
				return GameFlow.Instance.SaveState;
			}

			return this;
		}

		#endregion
	}
}