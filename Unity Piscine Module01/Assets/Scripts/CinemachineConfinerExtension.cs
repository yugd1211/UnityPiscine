using UnityEngine;
using Cinemachine;

public class CinemachineConfinerExtension : CinemachineExtension
{
	public float minY;
	public float maxY;

	protected override void PostPipelineStageCallback(
		CinemachineVirtualCameraBase vcam,
		CinemachineCore.Stage stage,
		ref CameraState state,
		float deltaTime) 
	{
		if (stage == CinemachineCore.Stage.Body)
		{
			var pos = state.RawPosition;
			pos.y = Mathf.Clamp(pos.y, minY, maxY);
			state.RawPosition = pos;
		}
	}
}