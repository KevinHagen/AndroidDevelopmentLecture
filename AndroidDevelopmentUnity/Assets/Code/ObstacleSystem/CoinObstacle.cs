using System;
using ObstacleSystem.Models;
using UnityEngine;

namespace ObstacleSystem
{
	/// <summary>
	/// 	Obstacle representing a collectible coin.
	/// </summary>
	public class CoinObstacle : BaseObstacle
	{
		#region Static Stuff

		/// <summary>
		/// 	Event being called when the coin hits the player.
		/// </summary>
		public static event Action<CoinObstacle> ObstacleHitPlayer;

		#endregion

		#region Properties

		private CoinObstacleModel _coinModel;
		public int Value => _coinModel.Value;
		/// <summary>
		/// 	If set to true the item will stop falling.
		/// </summary>
		public bool DrawIn { get; set; }

		#endregion

		#region Unity methods

		private void Awake()
		{
			_coinModel = (CoinObstacleModel) _model;
		}

		protected override void Update()
		{
			// coin is being drawn to the player - dont keep falling
			if (DrawIn)
			{
				return;
			}

			transform.position += Vector3.up * (Physics.gravity.y * _model.FallingSpeed * Time.deltaTime);
			transform.RotateAround(transform.position, Vector3.up, (IsSpinningLeft ? 1 : -1) * _model.RotationSpeed * Time.deltaTime);
		}

		#endregion

		#region Protected methods

		protected override void HitPlayer()
		{
			base.HitPlayer();

			ObstacleHitPlayer?.Invoke(this);
		}

		#endregion
	}
}