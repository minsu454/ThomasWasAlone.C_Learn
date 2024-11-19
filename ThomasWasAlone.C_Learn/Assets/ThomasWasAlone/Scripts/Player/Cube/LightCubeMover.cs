using UnityEngine;
using DG.Tweening;

public class LightCubeMover : MonoBehaviour
{
    private Rigidbody rb;
    private BaseCube cube;
    private Vector3 originalScale;
    private bool isJumping = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cube = GetComponent<BaseCube>();
        originalScale = transform.localScale;
    }

    public void Jump(float jumpForce)
    {
        if (!cube.IsGrounded || isJumping) return;
        
        isJumping = true;
        
        Sequence jumpSequence = DOTween.Sequence();
        
        jumpSequence.Append(transform.DOScaleY(originalScale.y * 0.7f, 0.08f)
            .SetEase(Ease.InQuad));
        
        jumpSequence.Append(transform.DOScaleY(originalScale.y, 0.12f)
            .SetEase(Ease.OutBack))
            .OnComplete(() => {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = false;
            });
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
} 