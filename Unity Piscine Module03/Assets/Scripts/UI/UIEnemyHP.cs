using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHP : MonoBehaviour
{
	public EnemyController target;
	public Vector3 offset;
	
	private Slider _slider;

	private void Awake()
	{
		_slider = GetComponent<Slider>();
	}

	private void FixedUpdate()
	{
		if (!target)
			return;
		float maxHP = target.HP.maxHP;
		float HP = target.HP.currentHp;
		if (HP == 0f)
			Destroy(this.gameObject);
		_slider.value = HP / maxHP;
		
		transform.position = Camera.main.WorldToScreenPoint(target.transform.position + offset);
	}
}
