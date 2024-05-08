using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
	private string[] comments = new[]
	{
		"패배하셨습니다 ㅠㅠ",
		"축하합니다 승리하셨습니다!!",
	};
	
	public TextMeshProUGUI rank;
	public TextMeshProUGUI kill;
	public TextMeshProUGUI hp;
	public TextMeshProUGUI energy;
	public TextMeshProUGUI time;
	public TextMeshProUGUI comment;
	public GameObject victoryBackground;
	public GameObject defeatBackground;
	public GameObject allClearComment;
	public GameObject nextMap;
	public GameObject currMap;

	private void Start()
	{
		int rankNum = PlayerPrefs.GetInt("rank");
		rank.text = "랭크 : " + rankNum switch
		{
			0 => "F",
			1 => "D",
			2 => "C",
			3 => "B",
			4 => "A",
			_ => "S",
		};
		kill.text = "처치한 몬스터  : " + PlayerPrefs.GetInt("kill").ToString();
		hp.text = "남은 HP : " + PlayerPrefs.GetInt("hp").ToString();
		energy.text = "사용한 에너지 : " + PlayerPrefs.GetInt("energy").ToString();
		time.text = "버텨낸 시간 : " + (PlayerPrefs.GetInt("time") / 60) + ":" +
			(PlayerPrefs.GetInt("time") % 60).ToString("D2");
		comment.text = comments[rankNum == 0 ? 0 : 1]; 
		
		victoryBackground.SetActive(rankNum != 0);
		defeatBackground.SetActive(rankNum == 0);

		nextMap.SetActive(rankNum != 0);
		currMap.SetActive(rankNum == 0);
		
		if (rankNum != 0 && PlayerPrefs.GetString("currentScene") == "Map02")
		{
			allClearComment.SetActive(true);
			nextMap.SetActive(false);
		}
	}
}
