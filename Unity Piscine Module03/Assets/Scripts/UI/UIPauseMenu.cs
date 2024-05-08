using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    public GameObject validatePanel;
    public UITurret[] UITurrets;
    
    private GameObject _background;
    private GameObject _panel;
    private int _numberToValidate;

    private void Awake()
    {
        _background = transform.GetChild(0).gameObject;
        _panel = transform.GetChild(1).gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisplayMenu();
            foreach (UITurret turret in UITurrets)
            {
                turret.DestroyDraggingGo();
                turret.DestroyDraggedOverTurretBoxGo();
            }
        }
    }

    public void DisplayMenu()
    {
        GameManager.Instance.timer.GamePause();
        _background.SetActive(true);
        _panel.SetActive(true);
    }
    
    public void CloseMenu()
    {
        GameManager.Instance.timer.GameResume();
        _background.SetActive(false);
        _panel.SetActive(false);
    }
    
    public void ValidateAgain(int number)
    {
        _numberToValidate = number;
        validatePanel.SetActive(true);
    }

    public void ValidateClose()
    {
        validatePanel.SetActive(false);
    }

    public void Proof()
    {
        if (_numberToValidate == 0)
        {
            SceneManager.LoadScene("Menu");
        }
        if (_numberToValidate == 1)
        {
            SceneManager.LoadScene("Map01");
        }
        else if (_numberToValidate == 2)
        {
            SceneManager.LoadScene("Map02");
        }
        else if (_numberToValidate == 4)
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("currentScene"));
        }
    }
}
