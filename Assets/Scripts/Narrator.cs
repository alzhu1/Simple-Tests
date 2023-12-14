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
    L1_CORRECT_DOOR
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

    void Awake() {
        narratorText = GetComponent<Text>();
    }

    void Start() {
        EventBus.instance.OnGameStart += ReceiveGameStartEvent;
        EventBus.instance.OnDoorEnter += ReceiveDoorEnterEvent;
    }

    void OnDestroy() {
        EventBus.instance.OnGameStart -= ReceiveGameStartEvent;
        EventBus.instance.OnDoorEnter -= ReceiveDoorEnterEvent;
    }

    // void Update() {
    //     narratorText.text = currDialogue.value;
    // }

    void SetCurrDiagloue(DialogueKey key, params object[] metadata) {
        currDialogue = dialogues.First(d => d.key == key);


        // narratorText.text = currDialogue.value;
        narratorText.text = string.Format(currDialogue.value, metadata);
    }

    void ReceiveGameStartEvent() { SetCurrDiagloue(DialogueKey.START); }
    void ReceiveDoorEnterEvent(Counter c, bool isLevel1CorrectDoor) {
        c.IncrementDoorEnterCount();
        int doorEnterCount = c.DoorEnterCount;

        // Display correct door text
        if (isLevel1CorrectDoor) {
            SetCurrDiagloue(DialogueKey.L1_CORRECT_DOOR);
            return;
        }

        // Incorrect door has many options
        switch (doorEnterCount) {
            case 1: { SetCurrDiagloue(DialogueKey.L1_WRONG_DOOR_1); return; }
            case 2: { SetCurrDiagloue(DialogueKey.L1_WRONG_DOOR_2); return; }
            case 3: { SetCurrDiagloue(DialogueKey.L1_WRONG_DOOR_3); return; }
            case 4: { SetCurrDiagloue(DialogueKey.L1_WRONG_DOOR_4); return; }
            case 5: { SetCurrDiagloue(DialogueKey.L1_WRONG_DOOR_5); return; }
        }

        if (doorEnterCount < 10) { SetCurrDiagloue(DialogueKey.L1_WRONG_DOOR_LESS_10, doorEnterCount); }
        else if (doorEnterCount < 15) { SetCurrDiagloue(DialogueKey.L1_WRONG_DOOR_LESS_15, doorEnterCount); }
        else if (doorEnterCount < 20) { SetCurrDiagloue(DialogueKey.L1_WRONG_DOOR_LESS_20, doorEnterCount); }
        else { SetCurrDiagloue(DialogueKey.L1_WRONG_DOOR_20_PLUS, doorEnterCount); }
    }
}
