using ItemSystem.Models;
using ObstacleSystem;
using UnityEngine;

namespace ItemSystem
{
	/// <summary>
	/// 	Shockwave item that destroys all hostile objects in a given radius.
	/// </summary>
	public class ShockWaveItem : Item
	{
		#region Private Fields

		private ShockWaveItemModel _shockWaveItemModel;
		private Collider[] _hits = new Collider[10];

		#endregion

		#region Unity methods

		private void Awake()
		{
			_shockWaveItemModel = (ShockWaveItemModel) _model;
			ApplyRadiusToScaleRecursively(transform);
		}

		#endregion

		#region Protected methods

		protected override void UseInternal()
		{
			// check for hostile objects within range
			int hits = Physics.OverlapSphereNonAlloc(transform.position, _model.EffectRadius, _hits, _shockWaveItemModel.ShockWaveAffectedLayers);
			if (hits <= 0)
			{
				return;
			}

			// loop over all hits and if theyre a damageObstacle, destroy them
			foreach (Collider col in _hits)
			{
				if (col == null)
				{
					return;
				}

				DamageObstacle damageObstacle = col.GetComponent<DamageObstacle>();
				if (damageObstacle)
				{
					Destroy(damageObstacle.gameObject);
				}
			}
		}

		#endregion
	}
}