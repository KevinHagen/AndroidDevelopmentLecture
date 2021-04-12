using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFlowSystem.States
{
	/// <summary>
	/// 	Saves the games current state.
	/// </summary>
	public class SaveState : MonoBehaviour, IGameState
	{
		#region IGameState Members

		public void StateEnter()
		{
			SaveManager.Save();
			// reload the scene to finish game loop.
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void StateExit()
		{
		}

		public IGameState StateUpdate()
		{
			return this;
		}

		#endregion
	}
}