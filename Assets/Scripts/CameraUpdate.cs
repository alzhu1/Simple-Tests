using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraUpdate : MonoBehaviour {

    private CinemachineVirtualCamera[] vCams;

    void Awake() {
        vCams = GetComponentsInChildren<CinemachineVirtualCamera>();
    }

    void Start() {
        EventBus.instance.OnDoorEnter += ReceiveDoorEnterEvent;
    }

    void OnDestroy() {
        EventBus.instance.OnDoorEnter -= ReceiveDoorEnterEvent;
    }

    void ReceiveDoorEnterEvent(Counter c, bool isLevel1CorrectDoor) {
        if (isLevel1CorrectDoor) {
            // Beat level 1, switch priority
            vCams[0].m_Priority = 0;
            vCams[1].m_Priority = 10;
        }
    }
}
