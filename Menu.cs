using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook GameplayCam;
    [SerializeField] private CinemachineVirtualCamera MenuCam;
    [SerializeField] private Canvas GameplayCanvas;
    [SerializeField] private Canvas MenuCanvas;
    [SerializeField] private Canvas OptionsCanvas;
    [SerializeField] private Text StartButtonText;
    
    private bool _paused = true;
    private bool _options = false;

    private void Start()
    {
        GameplayCam.gameObject.SetActive(false);
        MenuCam.gameObject.SetActive(true);
        MenuCanvas.enabled = true;
        OptionsCanvas.enabled = false;
        GameplayCanvas.enabled = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Pause()
    {
        GameplayCanvas.enabled = false;
        MenuCanvas.enabled = true;
        _paused = true;
        Time.timeScale = 0;
        GameplayCam.gameObject.SetActive(false);
        MenuCam.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        MenuCanvas.enabled = false;
        GameplayCanvas.enabled = true;
        _paused = false;
        Time.timeScale = 1;
        GameplayCam.gameObject.SetActive(true);
        MenuCam.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartButtonText.text = "Resume";
        TutorialManager.Instance.Notify("gamestart");
    }

    public void ToOptions()
    {
        MenuCanvas.enabled = false;
        OptionsCanvas.enabled = true;
        _options = true;
    }

    public void ToMenu()
    {
        MenuCanvas.enabled = true;
        OptionsCanvas.enabled = false;
        _options = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HandlePause(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        if(_options) {
            ToMenu();
            return;
        }
        if (_paused) Resume();
        else Pause();
    }
}