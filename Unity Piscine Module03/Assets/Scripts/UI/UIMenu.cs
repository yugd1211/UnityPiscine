using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
	public void StartScene()
	{
		SceneManager.LoadScene("map01");
	}

	public void Exit()
	{
		#if UNITY_EDITOR
				EditorApplication.isPlaying = false;
		#else
		        Application.Quit();
		#endif
	}
}
