using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;

    [SerializeField] float enemySpeed = 1f;
    float distance;
    public float desiredFollowDistance;
    public bool canFollowPlayer;

    private void Update()
    {
        if (canFollowPlayer)
        {
            if(distance < desiredFollowDistance)
            {
                distance = Vector2.Distance(transform.position, player.transform.position);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player.SetActive(false);
        }
    }
}
