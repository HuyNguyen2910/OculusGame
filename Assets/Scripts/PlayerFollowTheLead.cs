using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFollowTheLead : MonoBehaviour
{
    private string triggerString = "Explore";
    private bool isLoose = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == triggerString && !isLoose)
        {
            Debug.Log("Boom!");

            LoadingScene.Instance.ReStartNewGame();
            isLoose = true;
        }
    }

    private void Update()
    {
        print(OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp));
        print(OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown));
        print(OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft));
        print(OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight));

        //if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
        //{
        //    gameObject.transform.Translate(0, 0, .1f);
        //}
        //if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        //{
        //    gameObject.transform.Translate(0, 0, -.1f);
        //}
        //if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
        //{
        //    gameObject.transform.Translate(-.1f, 0, 0);
        //}
        //if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
        //{
        //    gameObject.transform.Translate(.1f, 0, 0);
        //}
    }
}
