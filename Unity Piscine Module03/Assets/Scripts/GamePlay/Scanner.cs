using System;
using UnityEngine;

public class Scanner : MonoBehaviour
{
	[Header("### Inspector Assign")]
	public float scanRange;
	public GameObject scanRangeImage;
	public LayerMask targetLayer;
	
	[Header("### Display Info")]
	public Transform nearestTarget;

	private RaycastHit2D[] _targets;

	private void Awake()
	{

		Vector3 localScale = transform.localScale;
		scanRangeImage.transform.localScale = new Vector3(
			(1 / localScale.x) * (scanRange * 2), 
			(1 / localScale.y) * (scanRange * 2), 
			(1 / localScale.z) * (scanRange * 2));
	}

	private void FixedUpdate()
	{
		_targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
		nearestTarget = GetNearest();
	}
	
	public void DisplayScanRange()
	{
		transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
	}

	public void HideScanRange()
	{
		transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
	}
	

	private Transform GetNearest()
	{
		Transform result = null;
		float diff = -1;

		foreach (RaycastHit2D target in _targets)
		{
			Vector3 myPos = transform.position;
			Vector3 targetPos = target.transform.position;
			float curDiff = Vector3.Distance(myPos, targetPos);

			if (diff == -1 || curDiff < diff)
			{
				diff = curDiff;
				result = target.transform;
			}
		}
		return result;
	}
}