namespace GameFlowSystem.States
{
	/// <summary>
	/// 	Interface for all game states
	/// </summary>
	public interface IGameState
	{
		#region Public methods

		void StateEnter();
		void StateExit();
		IGameState StateUpdate();

		#endregion
	}
}