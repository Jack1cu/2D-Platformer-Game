using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;

public class PlayerC : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoxCollider2D boxCol;

    private Vector2 boxColInitSize;
    private Vector2 boxColInitOffset;

    private void Start()
    {
        boxColInitSize = boxCol.size;
        boxColInitOffset = boxCol.offset;
    }

    private void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        playerAnimator.SetFloat("Speed",Mathf.Abs(speed));

        Vector3 scale = transform.localScale;
        if (speed < 0)
        {
            scale.x =-1f * Mathf.Abs(scale.x);
        }
        else if (speed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;


        float verticalInput = Input.GetAxis("Vertical");
        
        PlayJumpAnimation(verticalInput);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
        }
    }

    public void Crouch(bool crouch)
    {
        if (crouch == true)
        {
            float offX = -0.1374913f;
            float offY = 0.5796958f;
            float sizeX = 0.8832627f;
            float sizeY = 1.30027f;

            boxCol.offset = new Vector2(offX, offY);
            boxCol.size = new Vector2(sizeX, sizeY);
        }
        else
        {
            boxCol.size = boxColInitSize;
            boxCol.offset = boxColInitOffset;
        }
        playerAnimator.SetBool("Crouch", crouch);
    }

    public void PlayJumpAnimation(float vertical)
    {
        if(vertical > 0)
        {
            playerAnimator.SetTrigger("Jump");
        }
    }
}
