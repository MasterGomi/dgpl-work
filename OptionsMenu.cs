using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject xAxisSlider, yAxisSlider;
    [SerializeField] private CinemachineFreeLook cam;
    [SerializeField] private float maxX, minX, maxY, minY;
    [SerializeField] private Text sprintToggleText;
    [SerializeField] private ThirdPersonMovement playerMove;

    private void Start()
    {
        Slider xSensitivity = xAxisSlider.GetComponent<Slider>();
        float startVal = cam.m_XAxis.m_MaxSpeed;
        xSensitivity.maxValue = maxX;
        xSensitivity.minValue = minX;
        xSensitivity.value = startVal;
        Slider ySensitivity = yAxisSlider.GetComponent<Slider>();
        startVal = cam.m_YAxis.m_MaxSpeed;
        ySensitivity.maxValue = maxY;
        ySensitivity.minValue = minY;
        ySensitivity.value = startVal;
    }

    public void ChangeXSensitivity(float newVal)
    {
        cam.m_XAxis.m_MaxSpeed = newVal;
    }

    public void ChangeYSensitivity(float newVal)
    {
        cam.m_YAxis.m_MaxSpeed = newVal;
    }

    public void ToggleSprintToggle()
    {
        playerMove.sprintToggle = !playerMove.sprintToggle;
        sprintToggleText.text = playerMove.sprintToggle ? "Toggle" : "Hold";
    }
}