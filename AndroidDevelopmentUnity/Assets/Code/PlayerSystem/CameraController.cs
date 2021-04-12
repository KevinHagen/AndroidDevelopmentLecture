using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// 	Controls the camera movement in the game.
	/// </summary>
	public class CameraController : MonoBehaviour
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("The target to follow with the camera.")]
		private Transform _followTarget;
		[SerializeField] [Tooltip("How aggressively the camera follows.")] [Range(0f, 1f)]
		private float _step = 0.75f;

		#endregion

		#region Private Fields

		private Vector3 _offset;

		#endregion

		#region Unity methods

		private void Awake()
		{
			_offset = transform.position - _followTarget.position;
		}

		// Camera using late update so we can ensure the player has already moved
		private void LateUpdate()
		{
			Vector3 targetPos = _followTarget.position + _offset;
			transform.position = Vector3.Slerp(transform.position, targetPos, _step);
		}

		#endregion
	}
}