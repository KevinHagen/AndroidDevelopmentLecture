using UnityEngine;

[ExecuteInEditMode]
public class LightImprove : MonoBehaviour
{
	#region Public Fields

	public float setBias = -1f;

	#endregion

	#region Unity methods

	// Update is called once per frame
	void Update()
	{
		GetComponent<Light>().shadowBias = setBias;
	}

	#endregion
}