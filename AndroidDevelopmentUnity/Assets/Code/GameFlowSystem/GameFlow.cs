using Core;
using GameFlowSystem.States;
using PlayerSystem;
using UnityEngine;

namespace GameFlowSystem
{
	/// <summary>
	/// 	State Machine controlling the game flow. Implements state pattern.
	/// </summary>
	public class GameFlow : Singleton<GameFlow>
	{
		#region Serialize Fields

		[SerializeField] private MainMenuState _mainMenuState;
		[SerializeField] private PauseGameState _pauseGameState;
		[SerializeField] private StartGameState _startGameState;
		[SerializeField] private PlayGameState _playGameState;
		[SerializeField] private GameOverState _gameOverState;
		[SerializeField] private SaveState _saveState;
		[SerializeField] private LoadState _loadState;

		#endregion

		#region Private Fields

		private IGameState _currentState;
		private PlayerController _player;

		#endregion

		#region Properties

		public GameOverState GameOverState => _gameOverState;
		public bool IsGameRunning => _currentState.Equals(PlayGameState);
		public LoadState LoadState => _loadState;
		public MainMenuState MainMenuState => _mainMenuState;
		public PauseGameState PauseGameState => _pauseGameState;
		public PlayerController Player => _player;
		public PlayGameState PlayGameState => _playGameState;
		public SaveState SaveState => _saveState;
		public StartGameState StartGameState => _startGameState;

		#endregion

		#region Unity methods

		protected override void Awake()
		{
			base.Awake();

			_player = FindObjectOfType<PlayerController>();

			_currentState = LoadState;
			_currentState.StateEnter();

			Time.timeScale = 0f;
		}
		
		private void Update()
		{
			if (_currentState == null)
			{
				return;
			}

			// state pattern - update current state, hceck if it needs to be switched. Exit current one and enter new one if required.
			IGameState nextState = _currentState.StateUpdate();
			if (nextState != _currentState)
			{
				_currentState.StateExit();
				_currentState = nextState;
				_currentState.StateEnter();
			}
		}

		#endregion
	}
}