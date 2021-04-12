using UnityEngine;

namespace Core.Extensions
{
	/// <summary>
	/// 	Utility class containing some extension methods for CanvasGroups
	/// </summary>
	public static class CanvasGroupExtensions
	{
		#region Static Stuff

		/// <summary>
		/// 	Effectively hides the canvas group and disables its functionality
		/// </summary>
		/// <param name="g">CanvasGroup to disable</param>
		public static void Disable(this CanvasGroup g)
		{
			g.alpha = 0;
			g.interactable = false;
			g.blocksRaycasts = false;
		}

		/// <summary>
		/// 	Effectively shows the canvas group and enables its functionality
		/// </summary>
		/// <param name="g">CanvasGroup to enable</param>
		public static void Enable(this CanvasGroup g)
		{
			g.alpha = 1;
			g.interactable = true;
			g.blocksRaycasts = true;
		}

		#endregion
	}
}