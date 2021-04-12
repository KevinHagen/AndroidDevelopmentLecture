using UnityEngine;
using UnityEngine.Playables;

namespace GameFlowSystem.States
{
	/// <summary>
	/// 	Starts the game and prepares everything to be ready for play.
	/// </summary>
	public class StartGameState : MonoBehaviour, IGameState
	{
		#region Serialize Fields

		[SerializeField] private PlayableDirector _stateDirector;

		#endregion

		#region Private methods

		private void StartGame()
		{
			Time.timeScale = 1.0f;
			_stateDirector.Play();
		}

		#endregion

		#region IGameState Members

		public void StateEnter()
		{
			StartGame();
		}

		public void StateExit()
		{
		}

		public IGameState StateUpdate()
		{
			// Wait until time line has finished
			if (_stateDirector.state != PlayState.Playing)
			{
				return GameFlow.Instance.PlayGameState;
			}

			return this;
		}

		#endregion
	}
}