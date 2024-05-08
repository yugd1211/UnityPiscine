using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITurret : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Color sufficientColor;
	public Color inSufficientColor; 
	public SOTurret soTurret;
	public GameObject descriptionPanel;
	public bool isSufficient => _gameManager.Energy >= soTurret.energy;
	
	private GameManager _gameManager;
	private Image _image; 
	private Image _childImage;
	
	private GameObject _draggingGO; 
	private Transform _draggedOverTurretBox;
	private IPointerUpHandler _pointerUpHandlerImplementation;

	private void Awake()
	{
		_image = GetComponent<Image>();
		_childImage = transform.GetChild(0).GetComponent<Image>();
	}

	private void Start()
	{
		_gameManager = GameManager.Instance;
	}

	public void SufficientEnergy()
	{
		_image.color = sufficientColor;
		_childImage.color = sufficientColor;
	}

	public void InsufficientEnergy()
	{
		_image.color = inSufficientColor;
		_childImage.color = inSufficientColor;
	}

	private void FixedUpdate()
	{
		if (isSufficient)
			SufficientEnergy();
		else
			InsufficientEnergy();
	}

	public void DestroyDraggingGo()
	{
		Destroy(_draggingGO);
	}
	

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!isSufficient)
			return;
		_draggingGO = Instantiate(transform.GetChild(0).gameObject, transform);
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		if (!isSufficient)
			return;
		descriptionPanel.SetActive(false);
		_draggingGO.transform.position = eventData.position;
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("TurretBox"));
		if (hit.collider != null)
		{
			if (_draggedOverTurretBox)
				return;
			_draggedOverTurretBox = hit.transform;
			hit.transform.GetComponent<TurretBox>().DisplayTurret(soTurret.turretPrefab);
		}
		else
		{
			if (!_draggedOverTurretBox)
				return;
			_draggedOverTurretBox.GetComponent<TurretBox>().DestroyTurret();
			_draggedOverTurretBox = null;
		}
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		if (!isSufficient)
			return;
		_draggedOverTurretBox = null;
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("TurretBox"));
		if (hit.collider != null)
		{
			if (_gameManager.DecrementEnergy(soTurret.energy))
				hit.transform.GetComponent<TurretBox>().ConfirmTurretPlacement();
		}
		Destroy(_draggingGO);
	}
	
	public void OnPointerClick(PointerEventData eventData)
	{
		Destroy(_draggingGO);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		descriptionPanel.SetActive(true);
		descriptionPanel.transform.position = transform.position;
		descriptionPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "공격력 : " + soTurret.damage;
		descriptionPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "에너지 소모 : " + soTurret.energy;
		descriptionPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "쿨타임 : " + soTurret.rate;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		descriptionPanel.SetActive(false);
	}
}
