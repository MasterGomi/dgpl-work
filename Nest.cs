using System;
using UnityEngine;

public class Nest : MonoBehaviour
{
    [SerializeField] private EnviroColourChange EnviroColourChangeObj;
    [SerializeField] private Eagle EagleObj;

    private InteractableMoveable _egg = null;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Egg")) return;

        if (_egg == null) _egg = other.GetComponent<InteractableMoveable>();

        if (_egg.Held) return;
        
        EnviroColourChangeObj.ChangeColour();
        EagleObj.ChangeColour();
        TutorialManager.Instance.Notify("gameover");
        Destroy(GetComponent<Collider>());
    }
}