using UnityEngine;

public class EnemySpawnController : Singleton<EnemySpawnController>
{
    Vector3 checkPosition;
    Vector3 spawnPosition;
    float raycastHeight = 20.0f;

    Vector3 raycastFrom;

    public void SpawnEnemy(Unit unitToSpawn)
    {
        if (unitToSpawn != null)
        {
            spawnPosition = CheckPositionToSpawn();
            if (spawnPosition != Vector3.zero)
            {
                Unit spawnedUnit = Instantiate(unitToSpawn, spawnPosition, Quaternion.identity);
                GameController.Instance.enemiesOnMap.Add(spawnedUnit);
            }

        }
    }

    public Vector3 CheckPositionToSpawn()
    {
        Vector3 pointDifference = GameController.Instance.arena.pointTopRight.position - GameController.Instance.arena.pointBotLeft.position;
        checkPosition = GameController.Instance.arena.pointBotLeft.position;
        checkPosition.x += pointDifference.x * Random.Range(0.0f, 1.0f);
        checkPosition.z += pointDifference.z * Random.Range(0.0f, 1.0f);
        checkPosition.y += raycastHeight;

        Ray ray = new Ray(checkPosition, Vector3.down);
        Debug.DrawRay(checkPosition, Vector3.down * 100.0f, Color.white, 10f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                return hit.point;
                //Debug.Log("Hit");
                //SpawnEnemy(GameController.Instance.waveEnemy, hit.point);
            }
            else
            {
                return Vector3.zero;
            }

        }
        else
        {
            return Vector3.zero;
        }
    }
}
