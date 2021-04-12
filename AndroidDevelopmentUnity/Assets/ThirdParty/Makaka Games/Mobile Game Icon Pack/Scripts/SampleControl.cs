using UnityEngine;

public class SampleControl : MonoBehaviour
{
	#region Public Fields

	public Animator gameOverMenuAnimator;

	#endregion

	#region Unity methods

	void Start()
	{
		if (gameOverMenuAnimator)
		{
			gameOverMenuAnimator.SetTrigger("Show");
			//Debug.Log("Game Over Menu Show");
		}
	}

	#endregion
}