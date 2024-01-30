using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float shootInterval = 2f;
    public GameObject projectilePrefab;
    public float projectileForce = 5f;

    private Transform player;
    private bool playerInRange = false;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ShootBalls());

 
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            playerInRange = true;
         
        }
        else
        {
            playerInRange = false;
        }
    }

    private IEnumerator ShootBalls()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);

            if (playerInRange)
            {
                ShootInAllDirections();
            }
        }
    }

    private void ShootInAllDirections()
    {
        for (int i = 0; i < 360; i += 45)
        {
            // Add an additional 90 degrees rotation
            Quaternion rotation = Quaternion.Euler(0f, 0f, i + 90);
            GameObject projectile = Instantiate(projectilePrefab, transform.position, rotation);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply force to the projectile
                rb.AddForce(projectile.transform.right * projectileForce, ForceMode2D.Impulse);
            }
        }
    }


}
