using System;

namespace ObstacleSystem
{
	/// <summary>
	/// 	Class for all damaging obstacles like axes, swords, ...
	/// </summary>
	public class DamageObstacle : BaseObstacle
	{
		#region Static Stuff

		/// <summary>
		/// 	Event being called when the obstacle hits the ground.
		/// </summary>
		public static event Action<DamageObstacle> OnObstacleHitGround;

		#endregion

		#region Protected methods

		protected override void HitGround()
		{
			base.HitGround();

			OnObstacleHitGround?.Invoke(this);
		}

		#endregion
	}
}