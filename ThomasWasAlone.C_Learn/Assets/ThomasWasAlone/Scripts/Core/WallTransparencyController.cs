using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class WallTransparencyController : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float raycastDistance = 100f;
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float targetAlpha = 0.3f;
    
    private Camera mainCamera;
    private Transform currentTarget;
    private Dictionary<Renderer, Tween> activeTweens = new Dictionary<Renderer, Tween>();
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    
    private void OnDestroy()
    {
        foreach (var tween in activeTweens.Values)
        {
            tween?.Kill();
        }
        activeTweens.Clear();
    }
    
    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }
    
    private void LateUpdate()
    {
        if (!currentTarget) return;
        
        // 카메라에서 플레이어로 레이캐스트
        Vector3 direction = currentTarget.position - mainCamera.transform.position;
        RaycastHit[] hits = Physics.RaycastAll(
            mainCamera.transform.position,
            direction,
            raycastDistance,
            wallLayer
        );
        
        // 현재 레이캐스트에 걸린 벽들 처리
        HashSet<Renderer> hitRenderers = new HashSet<Renderer>();
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent(out Renderer renderer))
            {
                hitRenderers.Add(renderer);
                FadeOut(renderer);
            }
        }
        
        // 레이캐스트에 걸리지 않은 벽들은 원래대로 복구
        foreach (var kvp in new Dictionary<Renderer, Tween>(activeTweens))
        {
            if (!hitRenderers.Contains(kvp.Key))
            {
                FadeIn(kvp.Key);
            }
        }
    }
    
    private void FadeOut(Renderer renderer)
    {
        if (activeTweens.TryGetValue(renderer, out Tween currentTween))
        {
            currentTween.Kill();
        }
        
        Material material = renderer.material;
        activeTweens[renderer] = DOTween.To(
            () => material.color.a,
            (float alpha) => {
                Color color = material.color;
                color.a = alpha;
                material.color = color;
            },
            targetAlpha,
            fadeDuration
        );
    }
    
    private void FadeIn(Renderer renderer)
    {
        if (activeTweens.TryGetValue(renderer, out Tween currentTween))
        {
            currentTween.Kill();
        }
        
        Material material = renderer.material;
        activeTweens[renderer] = DOTween.To(
            () => material.color.a,
            (float alpha) => {
                Color color = material.color;
                color.a = alpha;
                material.color = color;
            },
            1f,
            fadeDuration
        ).OnComplete(() => activeTweens.Remove(renderer));
    }
} 