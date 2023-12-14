using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] private bool isLevel1CorrectDoor = false;
    private Transform destination;

    void Awake() {
        destination = GetComponentsInChildren<Transform>().Skip(1).First();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        // Player layer check
        if (collider.gameObject.layer == 6) {
            collider.gameObject.transform.position = destination.position;
            EventBus.instance.TriggerOnDoorEnter(isLevel1CorrectDoor);
        }
    }
}
