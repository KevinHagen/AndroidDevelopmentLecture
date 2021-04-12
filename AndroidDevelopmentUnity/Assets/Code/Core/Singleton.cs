using UnityEngine;

namespace Core
{
	/// <summary>
	/// 	Implementation of the singleton pattern for a MonoBehaviour. Gives static access and ensures there is only one instance present.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		#region Static Stuff

		private static Singleton<T> _instance;
		public static T Instance => (T) _instance;

		#endregion

		#region Unity methods

		protected virtual void Awake()
		{
			if ((_instance != null) && (_instance != this))
			{
				Destroy(gameObject);
			}
			else
			{
				_instance = this;
			}
		}

		#endregion
	}
}