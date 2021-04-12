using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Random
{
	/// <summary>
	/// 	Lists an array of Probability Items for a given generic type. Can be used to create "bags" of random items.
	/// </summary>
	/// <typeparam name="T">The data type of which we want a random list</typeparam>
	[Serializable]
	public class ProbabilityItemList<T>
	{
		#region Serialize Fields

		[SerializeField] private List<ProbabilityItem<T>> _items = new List<ProbabilityItem<T>>();

		#endregion

		#region Public methods

		/// <summary>
		/// 	Updates the probabilities on all <see cref="ProbabilityItem{T}"/> present in the list, always according to their amount
		/// </summary>
		public void UpdateProbabilities()
		{
			// count how many items are there in total
			float totalCount = 0;
			foreach (ProbabilityItem<T> probabilityItem in _items)
			{
				totalCount += probabilityItem.Amount;
			}

			// calc probability in % based on individual item count & totalCount
			foreach (ProbabilityItem<T> probabilityItem in _items)
			{
				probabilityItem.SetProbability(probabilityItem.Amount / totalCount);
			}
		}

		/// <summary>
		/// 	Returns a random item from the list, matching its probability with the given one.
		/// </summary>
		/// <param name="probability">probability in %</param>
		/// <returns>The selection</returns>
		public T ChooseRandom(float probability)
		{
			return Choose(probability).Item;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// 	Returns a random item from the list, matching its probability with the given one.
		/// </summary>
		/// <param name="probability">probability in %</param>
		/// <returns>The selection</returns>
		private ProbabilityItem<T> Choose(float probability)
		{
			ProbabilityItem<T>[] probabilityItems = _items as ProbabilityItem<T>[] ?? _items.ToArray();
			// sum all item probability to see how theyre weighted against another
			float sum = probabilityItems.Sum(l => l.Probability);
			// calc the random roll to the matching sum
			float random = probability * sum;

			// search for item fitting the probability
			foreach (ProbabilityItem<T> item in probabilityItems)
			{
				if (random <= item.Probability)
				{
					return item;
				}

				random -= item.Probability;
			}

			// safety net, in case nothing was found
			return new ProbabilityItem<T>();
		}

		#endregion
	}
}