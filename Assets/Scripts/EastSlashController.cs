using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EastSlashController : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Vector2 aux = new Vector2(player.position.x + 0.1f, player.position.y);
        transform.position = aux;
        StartCoroutine(DelayExecution(0.30f));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 aux = new Vector2(player.position.x + 0.1f, player.position.y);
        transform.position = aux;
    }

    void OnEnable()
    {
        Vector2 aux = new Vector2(player.position.x + 0.1f, player.position.y);
        transform.position = aux;
        StartCoroutine(DelayExecution(0.30f));
    }

    private IEnumerator DelayExecution(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(false);
    }
}
