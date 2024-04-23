using UnityEngine;

public class Scanner : MonoBehaviour
{
	public float scanRange;
	public LayerMask targetLayer;
	public Transform nearestTarget;

	private RaycastHit2D[] _targets;
	
	private void FixedUpdate()
	{
		_targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
		nearestTarget = GetNearest();
	}

	Transform GetNearest()
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