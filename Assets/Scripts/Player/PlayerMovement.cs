using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private SpriteRenderer _spriteRenderer;
	private Animator animator;
    private GameObject visualsContainer;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        visualsContainer = new GameObject("VisualContainer");
        visualsContainer.transform.SetParent(transform);
        visualsContainer.transform.localPosition = Vector3.zero;
        visualsContainer.transform.localRotation = Quaternion.identity;
        visualsContainer.transform.localScale = Vector3.one;
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
        
        if (_spriteRenderer)
        {
            var newSpriteRenderer = visualsContainer.AddComponent<SpriteRenderer>();
            newSpriteRenderer.sprite = _spriteRenderer.sprite;
            newSpriteRenderer.sortingOrder = _spriteRenderer.sortingOrder;
            Destroy(_spriteRenderer);
            _spriteRenderer = newSpriteRenderer;
        }

        if (!animator) return;
        var newAnimator = visualsContainer.AddComponent<Animator>();
        newAnimator.runtimeAnimatorController = animator.runtimeAnimatorController;
        Destroy(animator);
        animator = newAnimator;

    }

    // Rotation based on the mouse position
    private void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        var isWalking = _movement != Vector2.zero;
        animator.SetBool("isWalking", isWalking);

        
        if (_movement.sqrMagnitude > 1)
        {
            _movement = _movement.normalized;
        }

        var mousePosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        transform.rotation = Mathf.Abs(direction.x) >= Mathf.Abs(direction.y)
            ? Quaternion.Euler(0, 0, direction.x > 0 ? 0 : 180)
            : Quaternion.Euler(0, 0, direction.y > 0 ? 90 : -90);
        
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animator.SetInteger("Direction", 2);
            _spriteRenderer.flipX = direction.x < 0;
        }
        else if (direction.y > 0)
        {
            animator.SetInteger("Direction", 1);
        }
        else
        {
            animator.SetInteger("Direction", 0);
        }
    }

    private void LateUpdate()
    {
        visualsContainer.transform.rotation = Quaternion.identity;
    }
    
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * (Player.Player.Instance.MoveSpeed * Time.fixedDeltaTime));
    }
}
