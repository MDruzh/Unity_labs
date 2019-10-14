using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public float speed = 6f;
    private Rigidbody rb;
    public bool isGrounded;
    public bool froze = false;


    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (froze)
        {
            moveHorizontal = 0;
            moveVertical = 0;
            rb.velocity = new Vector3(0, 0, 0);


        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Paperbox")
        {
            StartCoroutine("Freeze");
        } else if(other.tag == "Fire") {
            gameObject.SetActive(false);
        }
    }

    IEnumerator Freeze()
    {
        froze = true;
        yield return new WaitForSeconds(3);
        froze = false;
    }
}
