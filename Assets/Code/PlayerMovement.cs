using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator; 
    public float moveSpeed = 5f; 
    private Vector2 movement;

    void Update()
    {
       
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");   

       
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude); 

        
        MoveCharacter();
    }

    void MoveCharacter()
    {
        
        Vector3 moveDirection = new Vector3(movement.x, movement.y).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
