using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class Emitter : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Welcome in TigerForge - Easy Event Manager, version 2.3!\n");
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("<color=green>I'mat the Emitter: I emitted <b>PRESSED_A</b> <list1>event</list1> with the integer value <b>100</b>.</color>\n");
            EventManager.SetData("PRESSED_A", 100);
            EventManager.EmitEvent("PRESSED_A");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("<color=green>I'mat the Emitter: I emitted <b>PRESSED_S</b> <list1>event</list1> <b>after 2 seconds</b>, but only the Objects with <b>'Player' tag</b> will listen to it.</color>\n");
            EventManager.EmitEvent("PRESSED_S", "tag:Player", 2);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log("<color=green>I'mat the Emitter: I emitted <b>PRESSED_D</b> <list1>event</list1>, but only the Objects in the <b>'Water' Layer</b> will listen to it. Moreover, I'll tell them who I am (sender).</color>\n");
            EventManager.EmitEvent("PRESSED_D", "layer:4", 0, gameObject);
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            Debug.Log("<color=green>I'mat the Emitter: I emitted <b>PRESSED_F</b> <list1>event</list1> with EmitEvenData method (passing 'HELLO!' string data).</color>\n");
            EventManager.EmitEventData("PRESSED_F", "HELLO!");
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            Debug.Log("<color=green>I'mat the Emitter: I emitted <b>PRESSED_G</b> <list1>event</list1>, but only the Objects with <b>name containing 'emy'</b> string and with <b>tag starting with 'Enemy'</b>, in the <b>'Default' layer</b> will listen to it.</color>\n");
            EventManager.EmitEvent("PRESSED_G", "name:*emy*;tag:Enemy*;layer:0");
        }
    }
}
