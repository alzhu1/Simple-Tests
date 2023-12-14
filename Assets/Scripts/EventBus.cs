using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour {
    public static EventBus instance = null;

    // public event Action<int> OnLevelStart = delegate {};
    // public event Action OnLevelFail = delegate {};
    // public event Action OnLevelComplete = delegate {};

    // public event Action<int> OnJump = delegate {};

    private Counter counter;

    // Level 1 triggers
    public event Action OnGameStart = delegate {};
    public event Action<Counter, bool> OnDoorEnter = delegate {};

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    void Start() {
        counter = GetComponent<Counter>();
    }

    // private int t = 0;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            // t = (t + 1) % 5;
            TriggerOnGameStart();
        }
    }

    public void TriggerOnGameStart() {
        OnGameStart?.Invoke();
    }

    public void TriggerOnDoorEnter(bool isLevel1CorrectDoor) {
        OnDoorEnter?.Invoke(counter, isLevel1CorrectDoor);
    }

    // public void TriggerOnLevelFail() {
    //     Debug.Log("FAILED");
    //     OnLevelFail?.Invoke();
    // }

    // public void TriggerOnLevelComplete() {
    //     Debug.Log("COMPLETED");
    //     OnLevelComplete?.Invoke();
    // }

    // public void TriggerOnJump(int jumpsLeft) {
    //     OnJump?.Invoke(jumpsLeft);
    // }
}
