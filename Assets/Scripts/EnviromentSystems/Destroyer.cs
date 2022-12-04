using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField]private BoxCollider2D spawnOpposed;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            //Spawner.KillEnemy(collision.GetComponent<Enemy>());
            enemy.transform.position = SpawnPosition();
        }

       

        if (collision.GetComponent<Projectile>() != null)
        {
            collision.GetComponent<Projectile>().Dead();
        }
    }
    private Vector2 SpawnPosition()
    {
        float x = Random.Range(spawnOpposed.transform.position.x - spawnOpposed.size.x / 2, spawnOpposed.transform.position.x + spawnOpposed.size.x / 2);
        float y = Random.Range(spawnOpposed.transform.position.y - spawnOpposed.size.y / 2, spawnOpposed.transform.position.y + spawnOpposed.size.y / 2);

        return new Vector2(x, y);
    }
}
