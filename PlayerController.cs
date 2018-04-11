using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float gravity;
    public float jumpHeight;
    public LayerMask ground;
    public Transform feet;
    public Text countText;
    public Text winText;
    public Text puText;
    private int count;
    private int pucount;

    private Vector3 direction;
    private Vector3 walkingVelocity;
    private Vector3 fallingVelocity;
    private CharacterController controller;

    // Use this for initialization
    void Start()
    {
        speed = 5.0f;
        gravity = 9.8f;
        //jumpHeight = 3.0f;
        direction = Vector3.zero;
        fallingVelocity = Vector3.zero;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = direction.normalized;
        walkingVelocity = direction * speed;
        controller.Move(walkingVelocity * Time.deltaTime);
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
            Debug.Log(direction);
        }
        bool isGrounded = Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore);
        if (isGrounded)
            fallingVelocity.y = 0f;
        else
            fallingVelocity.y -= gravity * Time.deltaTime;
        
        controller.Move(fallingVelocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            pucount++;
           
            
                other.gameObject.SetActive(false);
                count = count + 1;
                SetCountText();

                if (pucount == 7)
                {
                    SetFinalText();
                }
            

            
        }

        if (other.gameObject.CompareTag("Destructive"))
        {
            if (pucount == 7)
            {
                SetFinalText();
            }

            else
            {
                other.gameObject.SetActive(false);
                count = count - 1;
                SetCountText();
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        
        
    }

    void SetFinalText()
    {
        
        if (count >= 7)
        {
            winText.text = "Perfect Score!";
            speed = 0f;
        }

        if (count == 6 || count == 5)
        {
            winText.text = "You win!";
            speed = 0f;
        }

        if (count == 4 || count == 3)
        {
            winText.text = "Try Again!";
            speed = 0f;
        }

        if (count <=2)
        {
            winText.text = "Caught!";
            speed = 0f;
        }
    }
}



