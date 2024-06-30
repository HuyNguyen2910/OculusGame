using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private GameObject explode;
    [SerializeField] private Collider trigger;
    [SerializeField] private MeshRenderer floor;

    [SerializeField] private List<GameObject> explodePref;
    [SerializeField] private List<Material> floorMaterials;
    public void CheckSafePos(bool isSafe)
    {
        trigger.enabled = !isSafe;
        explode.SetActive(!isSafe);
    }
    public void LoadExplodeFloorPref()
    {
        LoadExplode();
        LoadFloor();
    }
    private void LoadExplode()
    {
        foreach(Transform child in explode.transform)
        {
            Destroy(child.gameObject);
        }

        int explodeIndex = UserData.Instance.currentExplode < explodePref.Count
            ? UserData.Instance.currentExplode
            : 0;

        Instantiate(explodePref[explodeIndex], explode.transform);
    }
    private void LoadFloor()
    {
        int floorIndex = UserData.Instance.currentFloor < floorMaterials.Count
            ? UserData.Instance.currentFloor
            : 0;

        floor.material = floorMaterials[floorIndex];
    }
}
