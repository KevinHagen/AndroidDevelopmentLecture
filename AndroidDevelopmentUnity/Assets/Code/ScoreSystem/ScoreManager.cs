using Core;
using ObstacleSystem;
using SaveSystem;
using SaveSystem.DTOs;
using SaveSystem.Interfaces;
using TMPro;
using UnityEngine;

namespace ScoreSystem
{
	public class ScoreManager : Singleton<ScoreManager>, ISaveable, ILoadable
	{
		#region Serialize Fields

		[SerializeField] private TextMeshProUGUI _scoreText;

		#endregion

		#region Private Fields

		private int _highScore;
		private int _currentScore;

		#endregion

		#region Properties

		public int HighScore => _highScore;
		public int Score => _currentScore;

		#endregion

		#region Unity methods

		protected override void Awake()
		{
			base.Awake();

			DamageObstacle.OnObstacleHitGround += OnObstacleHitGround;
			_scoreText.text = "";
		}

		private void Start()
		{
			SaveManager.RegisterSaveable(this);
			SaveManager.RegisterLoadable(this);
		}

		private void OnDestroy()
		{
			DamageObstacle.OnObstacleHitGround -= OnObstacleHitGround;
			SaveManager.UnregisterSaveable(this);
			SaveManager.UnregisterLoadable(this);
		}

		#endregion

		#region Private methods

		private void OnObstacleHitGround(DamageObstacle obj)
		{
			if (obj == null)
			{
				return;
			}

			_currentScore++;
			_scoreText.text = _currentScore.ToString();
		}

		#endregion

		#region ILoadable Members

		public void Load()
		{
			ScoreDataDto dto = SaveManager.Get(SaveKeys.ScoreDataKey, new ScoreDataDto()) as ScoreDataDto;
			if (dto == null)
			{
				return;
			}

			_highScore = dto.Score;
		}

		#endregion

		#region ISaveable Members

		public void Save()
		{
			ScoreDataDto dto = new ScoreDataDto();
			dto.Score = _currentScore > _highScore ? _currentScore : _highScore;

			SaveManager.Put(SaveKeys.ScoreDataKey, dto);
		}

		#endregion
	}
}