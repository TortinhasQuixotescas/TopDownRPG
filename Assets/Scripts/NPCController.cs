using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Rigidbody2D NPC_RB;
    public Animator NPCAnimator;
    public int moveSpeed = 5;

    public float minTimeSameDirection = 1.0f;
    public float maxTimeSameDirection = 3.0f;
    private float currentDirectionTime = 0;
    private bool isChangingDirection = false;

    public float minTimeIdle = 1.0f;
    public float maxTimeIdle = 3.0f;
    private bool isIdle = false;

    private float lastXValue = 0;
    private float lastYValue = -1;
    private float randomX = 0;
    private float randomY = 0;

    // Start is called before the first frame update
    void Start()
    {
        HandleRandomMovement();
    }

    // Update is called once per frame
    void Update()
    {
        HandleDirectionChange();
        HandleIdleAnimation();
    }

    private void HandleDirectionChange()
    {
        if (!isChangingDirection)
        {
            currentDirectionTime += Time.deltaTime;

            if (currentDirectionTime >= minTimeSameDirection)
            {
                isChangingDirection = true;
                currentDirectionTime = 0;

                float delayTime = Random.Range(minTimeSameDirection, maxTimeSameDirection);

                if (!isIdle)
                {
                    NPCAnimator.SetTrigger("idle");
                    isIdle = true;
                }

                StartCoroutine(ChangeDirectionAfterDelay(delayTime));
            }
        }
    }

    private void HandleIdleAnimation()
    {
        if (isIdle && NPC_RB.velocity != Vector2.zero)
        {
            NPCAnimator.ResetTrigger("idle");
            isIdle = false;
        }
    }

    private void HandleRandomMovement()
    {
        float shouldMove = Random.Range(0.0f, 1.0f);

        if (shouldMove < 0.8f)
        {
            randomX = Random.Range(-1.0f, 1.0f);
            randomY = Random.Range(-1.0f, 1.0f);
        }
        else
        {
            randomX = 0;
            randomY = 0;
        }

        Vector2 input = new Vector2(randomX, randomY);
        NPC_RB.velocity = input * moveSpeed;
        NPCAnimator.SetFloat("xMovement", input.x);
        NPCAnimator.SetFloat("yMovement", input.y);

        if (input != Vector2.zero)
        {
            lastXValue = input.x;
            NPCAnimator.SetFloat("lastXValue", input.x);
            lastYValue = input.y;
            NPCAnimator.SetFloat("lastYValue", input.y);
        }
    }

    private IEnumerator ChangeDirectionAfterDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        if (Random.Range(0, 2) == 0)
        {
            float idleTime = Random.Range(minTimeIdle, maxTimeIdle);
            Vector2 input = Vector2.zero;
            NPC_RB.velocity = input;
            isIdle = true;
            yield return new WaitForSeconds(idleTime);
        }

        isChangingDirection = false;
        HandleRandomMovement();
    }
}
