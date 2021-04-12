using Core;
using ObstacleSystem.Models;
using UnityEngine;

namespace ObstacleSystem
{
	/// <summary>
	/// 	Base class for obstacles that fall from top to bottom.
	/// </summary>
	public abstract class BaseObstacle : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("Data representation of the obstacle in question")]
		protected ObstacleModel _model;

		#endregion

		#region Properties

		/// <summary>
		/// 	If set to true, the object spins the left way around, right if not.
		/// </summary>
		public bool IsSpinningLeft { get; set; }

		#endregion

		#region Unity methods

		protected virtual void Update()
		{
			// fall down and spin 
			transform.position += Vector3.up * (Physics.gravity.y * _model.FallingSpeed * Time.deltaTime);
			transform.RotateAround(transform.position, Vector3.forward, (IsSpinningLeft ? 1 : -1) * _model.RotationSpeed * Time.deltaTime);
		}

		private void OnTriggerEnter(Collider other)
		{
			// check whether ground/player was hit
			if (other.gameObject.CompareTag("Player"))
			{
				HitPlayer();
			}
			else if (other.gameObject.CompareTag("Ground"))
			{
				HitGround();
			}
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// 	Breaks the object and plays some according fx.
		/// </summary>
		protected virtual void HitGround()
		{
			AudioManager.Instance.PlaySFX(_model.ImpactSFX);
			Instantiate(_model.ImpactVFX, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

		/// <summary>
		/// 	Destroy the object on the player.
		/// </summary>
		protected virtual void HitPlayer()
		{
			Destroy(gameObject);
		}

		#endregion
	}
}