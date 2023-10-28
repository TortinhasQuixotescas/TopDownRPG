using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController uniqueInstance;
    public Rigidbody2D playerRB;
    public int moveSpeed = 5;
    public Animator playerAnimator;
    public string areaTransitionName;
    public bool busy = false;
    private bool isDelaying = false;
    private float lastXValue = 0;
    private float lastYValue = -1;
    public ObjectPool eastSlashPool;
    public GameObject eastSlash;
    public ObjectPool westSlashPool;
    public GameObject westSlash;
    public ObjectPool northSlashPool;
    public GameObject northSlash;
    public ObjectPool southSlashPool;
    public GameObject southSlash;
    // Start is called before the first frame update
    void Start()

    {
        if (uniqueInstance == null)
            uniqueInstance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        eastSlashPool = new ObjectPool(eastSlash, 1);
        westSlashPool = new ObjectPool(westSlash, 1);
        northSlashPool = new ObjectPool(northSlash, 1);
        southSlashPool = new ObjectPool(southSlash, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!busy)
        {
            playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
            playerAnimator.SetFloat("xMovement", playerRB.velocity.x);
            playerAnimator.SetFloat("yMovement", playerRB.velocity.y);
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                lastXValue = Input.GetAxisRaw("Horizontal");
                playerAnimator.SetFloat("lastXValue", Input.GetAxisRaw("Horizontal"));
                lastYValue = Input.GetAxisRaw("Vertical");
                playerAnimator.SetFloat("lastYValue", Input.GetAxisRaw("Vertical"));
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                busy = true;
                if (lastXValue > 0)
                {
                    playerAnimator.SetTrigger("swordAttackEast");
                    GameObject eastSlash = eastSlashPool.GetFromPool();
                    if (eastSlash != null)
                    {
                        Vector2 pos = transform.position;
                        eastSlash.transform.position = pos;
                        eastSlash.SetActive(true);
                    }
                }
                else if (lastXValue < 0)
                {
                    playerAnimator.SetTrigger("swordAttackWest");
                    GameObject westSlash = westSlashPool.GetFromPool();
                    if (westSlash != null)
                    {
                        Vector2 pos = transform.position;
                        westSlash.transform.position = pos;
                        westSlash.SetActive(true);
                    }
                }
                else if (lastYValue > 0)
                {
                    playerAnimator.SetTrigger("swordAttackNorth");
                    GameObject northSlash = northSlashPool.GetFromPool();
                    if (northSlash != null)
                    {
                        Vector2 pos = transform.position;
                        northSlash.transform.position = pos;
                        northSlash.SetActive(true);
                    }
                }
                else if (lastYValue < 0)
                {
                    playerAnimator.SetTrigger("swordAttackSouth");
                    GameObject southSlash = southSlashPool.GetFromPool();
                    if (southSlash != null)
                    {
                        Vector2 pos = transform.position;
                        southSlash.transform.position = pos;
                        southSlash.SetActive(true);
                    }
                }
                StartCoroutine(DelayExecution(0.30f));
            }
        }


    }

    private IEnumerator DelayExecution(float delayTime)
    {
        isDelaying = true;
        yield return new WaitForSeconds(delayTime);
        isDelaying = false;
        busy = false;
        playerAnimator.SetTrigger("notBusy");
    }
}
