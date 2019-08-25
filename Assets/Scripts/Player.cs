using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float excitability = 0;
    private BoxCollider2D box2D;
    private Rigidbody2D rb2D;
    public float moveSpeed = 20f;
    private bool moving = false;
    private Text excText;
    private int columns;
    private Animator animator;
    public bool canMove = true;

    void Start()
    {
        box2D = GetComponent<BoxCollider2D>();
        excText = GameObject.Find("excitedText").GetComponent<Text>();
        columns = GameObject.Find("Manager").GetComponent<BoardScript>().columns;
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    IEnumerator Move(int direction)
    {
        float remainingDis = 1f;
        Vector2 end = rb2D.position + new Vector2(direction, 0);
        while (remainingDis > float.Epsilon)
        {
            Vector3 newPos = Vector3.MoveTowards(rb2D.position, end, Time.deltaTime * moveSpeed);
            rb2D.MovePosition(newPos);
            remainingDis = (rb2D.position - end).sqrMagnitude;
            yield return null;
        }
        moving = false;
        yield return null;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        float currentEx = excitability + other.GetComponent<DownTiles>().refinement;
        if (currentEx > excitability)
        {
            if (currentEx >= 100)
            {
                animator.SetTrigger("boom");
                GameManager.instance.EndGame();
                enabled = false;
                
            }
            if (currentEx >= 80) animator.SetTrigger("excited4");
            else if (currentEx >= 60) animator.SetTrigger("excited3");
            else if (currentEx >= 40) animator.SetTrigger("excited2");
            else if (currentEx >= 20) animator.SetTrigger("excited1");
            else if (currentEx > 0) animator.SetTrigger("idle");
        }
        else if (currentEx < excitability)
        {
            if (currentEx < 0 ) animator.SetTrigger("upset");
            else if (currentEx < 20) animator.SetTrigger("calm1");
            else if (currentEx < 40) animator.SetTrigger("calm2");
            else if (currentEx < 60) animator.SetTrigger("calm3");
            else if (currentEx < 80) animator.SetTrigger("calm4");
        }

        excText.text = "Excitibility: " + currentEx;
        other.gameObject.SetActive(false);

        excitability = currentEx;
    }
    void Update()
    {
        if (canMove)
        {
            int direction = 0;
            if ((int)Input.GetAxisRaw("Horizontal") > 0) direction = 1;
            if ((int)Input.GetAxisRaw("Horizontal") < 0) direction = -1;
            if (direction != 0 && moving == false)
            {
                moving = true;
                if (direction == -1 && rb2D.position.x > 0) StartCoroutine(Move(direction));
                else if (direction == 1 && rb2D.position.x < columns) StartCoroutine(Move(direction));
                else { moving = false; }
            }
        }

    }
}
