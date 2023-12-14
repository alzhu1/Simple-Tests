using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {
    // Vars
    private int doorEnterCount;

    // Readonly field
    public int DoorEnterCount { get { return doorEnterCount; } }

    public void IncrementDoorEnterCount() { doorEnterCount++; }

    // void Start() {
    //     EventBus.instance.OnDoorEnter += ReceiveDoorEnterEvent;
    // }

    // void OnDestroy() {
    //     EventBus.instance.OnDoorEnter -= ReceiveDoorEnterEvent;
    // }

    // void ReceiveDoorEnterEvent(Counter c, bool isLevel1CorrectDoor) {
    //     doorEnterCount++;
    // }
}
