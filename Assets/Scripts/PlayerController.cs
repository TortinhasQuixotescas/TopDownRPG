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
    // Start is called before the first frame update
    void Start()

    {
        if (uniqueInstance == null)
            uniqueInstance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        playerAnimator.SetFloat("xMovement", playerRB.velocity.x);
        playerAnimator.SetFloat("yMovement", playerRB.velocity.y);
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            playerAnimator.SetFloat("lastXValue", Input.GetAxisRaw("Horizontal"));
            playerAnimator.SetFloat("lastYValue", Input.GetAxisRaw("Vertical"));
        }
    }
}
