using System.Collections;
using TigerForge;
using UnityEngine;

public class Standy : Singleton<Standy>
{
    public bool isSuccessed = false;
    public GameObject welcom, mission, hint1, hint2, fight, shop;

    bool isCrabditem = false;
    bool isGetScore = false;
    public bool isOpenShop = false;
    public bool isCloseShop = false;

    public bool autoPassed = false;
    private void Start()
    {
        EventManager.StartListening(EventKey.OnCrabItem.ToString(), OnCrabItem);
        EventManager.StartListening(EventKey.OnScoreChanged.ToString(), OnGetScore);
    }
    void OnCrabItem()
    {
        isCrabditem = true;
    }
    void OnGetScore()
    {
        isGetScore = true;
    }
    public IEnumerator StartProcess()
    {
        if (autoPassed)
        {
            isSuccessed = true;
            yield break;
        }

        welcom.SetActive(true);
        yield return new WaitForSeconds(3);
        welcom.SetActive(false);

        mission.SetActive(true);
        yield return new WaitForSeconds(5);
        mission.SetActive(false);

        hint1.SetActive(true);
        StartCoroutine(ObjSpawnMng.Instance.SpawnInTutorial());
        while (!isCrabditem) yield return null;
        hint1.SetActive(false);
        EventManager.StopListening(EventKey.OnCrabItem.ToString(), OnCrabItem);


        hint2.SetActive(true);
        while (!isGetScore) yield return null;
        hint2.SetActive(false);
        EventManager.StopListening(EventKey.OnScoreChanged.ToString(), OnGetScore);

        shop.SetActive(true);
        while (!isOpenShop)
        {
            if (OVRInputCtrl.ButtonTwoClick())
            {
                GameCtrl.Instance.OpenShop();
            }
            yield return null;
        }
        shop.SetActive(false);

        while (!isCloseShop)  yield return null;

        isSuccessed = true;

        fight.SetActive(true);
        yield return new WaitForSeconds(5);
        fight.SetActive(false);

        yield return new WaitForSeconds(2);
    }
}