using System.Collections;
using UnityEngine;

public class ObjSpawnMng : Singleton<ObjSpawnMng>
{
    public Spawner[] spawners;
    public Vector2 duration = new Vector2(4, 8);
    public float speedFly = 5;

    public IEnumerator SpawnInTutorial()
    {
        duration = new Vector2(2, 3);
        speedFly = 5;

        int[] itemToSpawn = new int[7] { 0, 2, 3, 4, 5, 6, 7 };
        int indexToSpawn = 0;
        while (!Standy.Instance.isSuccessed)
        {
            spawners[itemToSpawn[indexToSpawn]].Spawn(speedFly);
            yield return new WaitForSeconds(Random.Range(duration.x, duration.y));
            if (indexToSpawn < itemToSpawn.Length - 1)
                indexToSpawn++;
            else
                indexToSpawn = 0;
        }
    }
    public IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(1);

        while (GameCtrl.Instance.isPlaying)
        {
            if (!GameCtrl.Instance.isPaused)
            {
                int indexSpawner = Random.Range(0, spawners.Length);
                spawners[indexSpawner].Spawn(speedFly);
                yield return new WaitForSeconds(Random.Range(duration.x, duration.y));
            }
            else
                yield return null;
        }
    }
    public void Init(Vector2 dur, float speed)
    {
        duration = dur;
        speedFly = speed;
    }
}
