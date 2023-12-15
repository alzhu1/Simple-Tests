using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueKey {
    START,
    L1_WRONG_DOOR_1,
    L1_WRONG_DOOR_2,
    L1_WRONG_DOOR_3,
    L1_WRONG_DOOR_4,
    L1_WRONG_DOOR_5,
    L1_WRONG_DOOR_LESS_10,
    L1_WRONG_DOOR_LESS_15,
    L1_WRONG_DOOR_LESS_20,
    L1_WRONG_DOOR_20_PLUS,
    L2_START,
    L2_MOVE_1_SEC,
    L2_MOVE_5_SEC,
    L2_MOVE_10_SEC,
    WIN
}

[System.Serializable]
public struct Dialogue {
    // Descriptor
    [SerializeField] private string description;
    public DialogueKey key;
    [TextArea(3, 5)] public string value;
}

public class Narrator : MonoBehaviour {
    [SerializeField] private Dialogue[] dialogues;

    private Text narratorText;
    private Dialogue currDialogue;

    private bool beatLevel1;

    void Awake() {
        narratorText = GetComponent<Text>();
    }

    void Start() {
        EventBus.instance.OnGameStart += ReceiveGameStartEvent;
        EventBus.instance.OnDoorEnter += ReceiveDoorEnterEvent;
        EventBus.instance.OnMove += ReceiveMoveEvent;
    }

    void OnDestroy() {
        EventBus.instance.OnGameStart -= ReceiveGameStartEvent;
        EventBus.instance.OnDoorEnter -= ReceiveDoorEnterEvent;
        EventBus.instance.OnMove -= ReceiveMoveEvent;
    }

    void SetCurrDialogue(DialogueKey key, params object[] metadata) {
        currDialogue = dialogues.First(d => d.key == key);

        narratorText.text = string.Format(currDialogue.value, metadata);
    }

    void ReceiveGameStartEvent() { SetCurrDialogue(DialogueKey.START); }
    void ReceiveDoorEnterEvent(Counter c, bool isLevel1CorrectDoor) {
        c.IncrementDoorEnterCount();
        int doorEnterCount = c.DoorEnterCount;

        // Display correct door text
        if (isLevel1CorrectDoor) {
            SetCurrDialogue(DialogueKey.L2_START);
            c.ResetTimers();
            beatLevel1 = true;
            return;
        }

        // Incorrect door has many options
        switch (doorEnterCount) {
            case 1: { SetCurrDialogue(DialogueKey.L1_WRONG_DOOR_1); return; }
            case 2: { SetCurrDialogue(DialogueKey.L1_WRONG_DOOR_2); return; }
            case 3: { SetCurrDialogue(DialogueKey.L1_WRONG_DOOR_3); return; }
            case 4: { SetCurrDialogue(DialogueKey.L1_WRONG_DOOR_4); return; }
            case 5: { SetCurrDialogue(DialogueKey.L1_WRONG_DOOR_5); return; }
        }

        if (doorEnterCount < 10) { SetCurrDialogue(DialogueKey.L1_WRONG_DOOR_LESS_10, doorEnterCount); }
        else if (doorEnterCount < 15) { SetCurrDialogue(DialogueKey.L1_WRONG_DOOR_LESS_15, doorEnterCount); }
        else if (doorEnterCount < 20) { SetCurrDialogue(DialogueKey.L1_WRONG_DOOR_LESS_20, doorEnterCount); }
        else { SetCurrDialogue(DialogueKey.L1_WRONG_DOOR_20_PLUS, doorEnterCount); }
    }

    void ReceiveMoveEvent(Counter c, bool moving) {
        if (beatLevel1) {
            float moveTime = c.TimeMoved;
            float waitTime = c.TimeWaited;

            if (waitTime >= 20) {
                SetCurrDialogue(DialogueKey.WIN);
                beatLevel1 = false;
                return;
            }

            if (moveTime < 5) {
                SetCurrDialogue(DialogueKey.L2_MOVE_1_SEC);
            } else if (moveTime < 10) {
                SetCurrDialogue(DialogueKey.L2_MOVE_5_SEC);
            } else {
                SetCurrDialogue(DialogueKey.L2_MOVE_10_SEC, moveTime, waitTime);
            }
        }
    }
}
