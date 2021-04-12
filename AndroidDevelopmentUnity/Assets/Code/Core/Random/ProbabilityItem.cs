using System;
using UnityEngine;

namespace Core.Random
{
	/// <summary>
	/// 	Class to represent items with a certain probability.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class ProbabilityItem<T>
	{
		#region Serialize Fields

		[SerializeField] [Tooltip("The higher the amount, the more likely it is to be chosen")]
		private int _amount = 1;
		[SerializeField] [Tooltip("The entity associated with this probability item.")] 
		private T _item = default;
		[SerializeField] [Tooltip("Probability in %")] 
		private float _probability;

		#endregion

		#region Properties

		public int Amount => _amount;
		public T Item => _item;
		public float Probability => _probability;

		#endregion

		#region Public methods

#if UNITY_EDITOR
		/// <summary>
		/// 	Editor-only method to set the probability at runtime, in case amount is being changed. 
		/// </summary>
		/// <param name="p">Probability in %</param>
		public void SetProbability(float p)
		{
			_probability = p;
		}
#endif

		#endregion
	}
}