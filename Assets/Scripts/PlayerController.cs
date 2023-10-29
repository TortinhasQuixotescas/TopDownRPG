using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public int moveSpeed = 5;
    public Animator playerAnimator;
    public string areaTransitionName;
    public bool busy = false;

    public ObjectPool eastSlashPool;
    public GameObject eastSlash;
    public ObjectPool westSlashPool;
    public GameObject westSlash;
    public ObjectPool northSlashPool;
    public GameObject northSlash;
    public ObjectPool southSlashPool;
    public GameObject southSlash;

    private bool isDelaying = false;
    private float lastXValue = 0;
    private float lastYValue = -1;
    public int chickenMeatAmout = 0;

    public static PlayerController uniqueInstance;

    private void Awake()
    {
        if (uniqueInstance == null)
        {
            uniqueInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeSlashPools();
    }

    private void OnLevelWasLoaded()
    {
        InitializeSlashPools();
    }

    private void InitializeSlashPools()
    {
        eastSlashPool = new ObjectPool(eastSlash, 1);
        westSlashPool = new ObjectPool(westSlash, 1);
        northSlashPool = new ObjectPool(northSlash, 1);
        southSlashPool = new ObjectPool(southSlash, 1);
    }

    private void Update()
    {
        HandleMovementInput();
        HandleSwordAttack();
    }

    private void HandleMovementInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerRB.velocity = input * moveSpeed;
        playerAnimator.SetFloat("xMovement", input.x);
        playerAnimator.SetFloat("yMovement", input.y);

        if (input != Vector2.zero)
        {
            lastXValue = input.x;
            playerAnimator.SetFloat("lastXValue", input.x);
            lastYValue = input.y;
            playerAnimator.SetFloat("lastYValue", input.y);
        }
    }

    private void HandleSwordAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !busy)
        {
            busy = true;

            if (lastXValue > 0)
            {
                PerformSwordAttack("swordAttackEast", eastSlash, eastSlashPool);
            }
            else if (lastXValue < 0)
            {
                PerformSwordAttack("swordAttackWest", westSlash, westSlashPool);
            }
            else if (lastYValue > 0)
            {
                PerformSwordAttack("swordAttackNorth", northSlash, northSlashPool);
            }
            else if (lastYValue < 0)
            {
                PerformSwordAttack("swordAttackSouth", southSlash, southSlashPool);
            }

            StartCoroutine(DelayExecution(0.30f));
        }
    }

    private void PerformSwordAttack(string triggerName, GameObject slash, ObjectPool slashPool)
    {
        playerAnimator.SetTrigger(triggerName);
        GameObject slashObject = slashPool.GetFromPool();
        if (slashObject != null)
        {
            Vector2 pos = transform.position;
            slashObject.transform.position = pos;
            slashObject.SetActive(true);
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("ChickenMeat"))
            ++chickenMeatAmout;
    }
}
