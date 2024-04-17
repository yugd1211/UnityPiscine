using UnityEngine;

public class Switch : MonoBehaviour
{
	public enum ToggleType
	{ 
		MomentaryPress,
		Toggle,
	}
	
	public enum AvailableType
	{
		All,
		Thomas,
		John,
		Claire,
	}
	
	private readonly Color[] _characterColor = 
	{
		Color.red,
		Color.yellow,
		Color.blue,
	};


	public enum SwitchType
	{
		OpenDoor,
		ChangeColor,
		OpenAndChangeColor,
	}
	
	public ToggleType toggleType;
	public SwitchType switchType;
	public AvailableType availableType;

	public Door[] openDoors;
	
	private Transform _case;
	private Transform _switch;
	private readonly Material[] _bodyMaterials = new Material[2];
	
	private void Awake()
	{
		_case = transform.GetChild(0).GetChild(0);
		_switch = transform.GetChild(0).GetChild(1);
		_bodyMaterials[0] = _case.GetComponent<MeshRenderer>().material;
		_bodyMaterials[1] = _switch.GetComponent<MeshRenderer>().material;
		ChangeColor(availableType);
	}

	public void ChangeColor(Character character)
	{
		if (switchType == SwitchType.OpenDoor)
			return;
		if (availableType != AvailableType.All && availableType.ToString() != character.ToString())
			return;
		foreach (Material material in _bodyMaterials)
		{
			material.color = _characterColor[(int)character];
		}
	}
	
	public void ChangeColor(AvailableType type)
	{
		if (availableType == AvailableType.All)
			return;
		foreach (Material material in _bodyMaterials)
		{
			material.color = _characterColor[(int)(type - 1)];
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!other.transform.CompareTag("Player"))
			return;
		Character otherCharacter = other.transform.GetComponent<PlayerController>().me; 
		if (availableType != AvailableType.All && availableType.ToString() != otherCharacter.ToString())
			return;
		if (switchType != SwitchType.OpenDoor)
			ChangeColor(otherCharacter);
		foreach (Door door in openDoors)
		{
			if (switchType is SwitchType.OpenDoor or SwitchType.OpenAndChangeColor)
			{
				door.Open();
			}
			if (switchType is SwitchType.ChangeColor or SwitchType.OpenAndChangeColor)
			{
				door.ChangeType(other.transform.GetComponent<PlayerController>().me);
			}
		}
	}
	private void OnCollisionExit(Collision other)
	{
		if (!other.transform.CompareTag("Player"))
			return;
		if (toggleType == ToggleType.Toggle)
			return;
		if (switchType != SwitchType.OpenDoor)
			foreach (Material material in _bodyMaterials)
				material.color = Color.white;
		foreach (Door door in openDoors)
		{
			door.Close();
		}
	}
}
