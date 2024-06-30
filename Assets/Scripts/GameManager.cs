using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum directionEnum
{
    up = 0,
    down = 1,
    left = 2,
    right = 3,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<Floor> floors;

    [SerializeField] private List<directionEnum> directionLevel;
    [SerializeField] private int level;
    [SerializeField] private Vector3 directPos = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 safePos = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 startPos = new Vector3(0, 0, 0);

    private float explodeWaitTime = 2;
    private float explodeTime = 3;
    private int size = 2;
    private int count = 0;
    private int minPosition = -10;
    private int maxPosition = 10;
    private bool isExplode = false;
    private void Awake()
    {
        Instance = this;
        UserData.Instance.LoadPrefs(SetupFloor);
    }
    private void Start()
    {
        level = 0;
    }
    public void SetupFloor()
    {
        foreach (Floor floor in floors)
        {
            floor.LoadExplodeFloorPref();
        }
    }
    private void Update()
    {
        if (level > 0 && !isExplode && Input.GetKeyDown(KeyCode.R))
        {
            ContinueLevel();
        }
    }
    public void ContinueLevel()
    {
        FrontScreen.Instance.SetContinueButton(false);
        CreateDirectionLevel();
        isExplode = true;
    }
    private List<int> GetSafePosition()
    {
        List<int> possiblePos = new List<int>();
        possiblePos.Add((int)directionEnum.up);
        possiblePos.Add((int)directionEnum.down);
        possiblePos.Add((int)directionEnum.left);
        possiblePos.Add((int)directionEnum.right);

        if (directPos.x <= minPosition)
        {
            possiblePos.Remove((int)directionEnum.left);
        }
        else if (directPos.x >= maxPosition)
        {
            possiblePos.Remove((int)directionEnum.right);
        }
        if (directPos.z <= minPosition)
        {
            possiblePos.Remove((int)directionEnum.down);
        }
        else if (directPos.z >= maxPosition)
        {
            possiblePos.Remove((int)directionEnum.up);
        }

        return possiblePos;
    }
    public void CreateDirectionLevel()
    {
        level += 1;
        FrontScreen.Instance.ShowLevel(level);
        directionLevel.Clear();
        RandomPos();
    }
    private void RandomPos()
    {
        count += 1;
        List<int> possiblePos = GetSafePosition();
        directionEnum direction = (directionEnum)possiblePos[Random.Range(0, possiblePos.Count)];
        directionLevel.Add(direction);

        directPos = SafePos(directPos, direction);
        if (count < level)
        {
            RandomPos();
        }
        else
        {
            EndLevel();
        }
    }
    public Vector3 SafePos(Vector3 pos, directionEnum directionEnum)
    {
        switch (directionEnum)
        {
            case directionEnum.up:
                pos = new Vector3(pos.x, pos.y, pos.z + size);
                break;
            case directionEnum.down:
                pos = new Vector3(pos.x, pos.y, pos.z - size);
                break;
            case directionEnum.left:
                pos = new Vector3(pos.x - size, pos.y, pos.z);
                break;
            case directionEnum.right:
                pos = new Vector3(pos.x + size, pos.y, pos.z);
                break;
        }
        return pos;
    }
    private void EndLevel()
    {
        count = 0;
        FrontScreen.Instance.GetDirectionList(directionLevel);
    }
    public void Run()
    {
        safePos = SafePos(safePos, directionLevel[count]);
        StartCoroutine(WaitToExplore());
    }
    private IEnumerator WaitToExplore()
    {
        yield return new WaitForSeconds(explodeWaitTime);

        foreach (Floor floor in floors)
        {
            if (floor.transform.position == safePos)
            {
                Debug.Log(floor.transform.parent.gameObject.name + "  " + floor.transform.gameObject.name);
            }
            floor.CheckSafePos(floor.transform.position == safePos);
        }
        StartCoroutine(Explore());
    }
    private IEnumerator Explore()
    {
        yield return new WaitForSeconds(explodeTime);

        foreach (Floor floor in floors)
        {
            floor.CheckSafePos(true);
        }
        if (count < level - 1)
        {
            count += 1;
            Run();
        }
        else
        {
            FrontScreen.Instance.EndLevel();
            count = 0;
            isExplode = false;
        }
    }
}
