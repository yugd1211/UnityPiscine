using UnityEngine;

public class Door : MonoBehaviour
{
	public Material[] materials;
	public bool isHide;
	public bool isOpen;

	private GameObject _plane;
	private MeshRenderer _meshRender;
	private void Awake()
	{
		isOpen = false;
		_plane = transform.GetChild(0).gameObject;
		_meshRender = GetComponent<MeshRenderer>();
	}
	
	public void ChangeType(Character character)
	{
		foreach (Material material in materials)
		{
			if (material.name == character.ToString())
				_meshRender.material = material;
		}
		int layer = character switch
		{
			Character.Thomas => 6,
			Character.Claire => 7,
			Character.John => 9,
			_ => 0,
		};
		gameObject.layer = layer;
		gameObject.transform.GetChild(0).gameObject.layer = layer;
	}

	public void Open()
	{
		Debug.Log("Open");
		if (isOpen)
			return;
		if (isHide)
			Reveal();
		else
			Up();
		isOpen = true;
	}

	public void Close()
	{
		if (!isOpen)
			return;
		if (isHide)
			Hide();
		else
			Down();
		isOpen = false;
	}

	private void Up()
	{
		transform.Translate(Vector3.up * 10);

	}
	
	private void Down()
	{
		transform.Translate(Vector3.down * 10);

	}

	private void Reveal()
	{
		gameObject.SetActive(true);
	}
	
	private void Hide()
	{
		gameObject.SetActive(false);
	}
}
