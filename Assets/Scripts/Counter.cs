using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {
    // Vars
    private int doorEnterCount;

    private bool moving;
    private float timeWaited;
    private float timeMoved;

    // Readonly field
    public int DoorEnterCount { get { return doorEnterCount; } }
    public float TimeWaited { get { return timeWaited; } }
    public float TimeMoved { get { return timeMoved; } }

    void Update() {
        if (moving) {
            timeMoved += Time.deltaTime;
        } else {
            timeWaited += Time.deltaTime;
        }
    }

    public void IncrementDoorEnterCount() { doorEnterCount++; }
    public void SetMoving(bool moving) { this.moving = moving; }
    public void ResetTimers() {
        timeWaited = timeMoved = 0f;
    }

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
