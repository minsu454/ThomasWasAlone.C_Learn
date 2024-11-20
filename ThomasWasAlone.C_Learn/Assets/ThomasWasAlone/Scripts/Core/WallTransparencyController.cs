using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class WallTransparencyController : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float targetAlpha = 0.3f;
    [SerializeField] private float raycastInterval = 0.2f;
    
    private Camera mainCamera;
    private Transform currentTarget;
    private Dictionary<Renderer, Tween> activeTweens = new Dictionary<Renderer, Tween>();
    private RaycastHit[] hitResults = new RaycastHit[3];
    private HashSet<Renderer> hitRenderers = new HashSet<Renderer>();
    private HashSet<Renderer> renderersToFadeIn = new HashSet<Renderer>();
    private Vector3 direction;
    private float nextRaycastTime;
    
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
        
        if (Time.time < nextRaycastTime) return;
        nextRaycastTime = Time.time + raycastInterval;
        
        CheckWallTransparency();
    }
    
    private void CheckWallTransparency()
    {
        hitRenderers.Clear();
        renderersToFadeIn.Clear();
        direction = currentTarget.position - mainCamera.transform.position;
        
        int hitCount = Physics.RaycastNonAlloc(
            mainCamera.transform.position,
            direction.normalized,
            hitResults,
            raycastDistance,
            wallLayer
        );
        
        for (int i = 0; i < hitCount; i++)
        {
            if (hitResults[i].collider.TryGetComponent(out Renderer renderer))
            {
                hitRenderers.Add(renderer);
                FadeOut(renderer);
            }
        }
        
        foreach (var renderer in activeTweens.Keys)
        {
            if (!hitRenderers.Contains(renderer))
            {
                renderersToFadeIn.Add(renderer);
            }
        }
        
        foreach (var renderer in renderersToFadeIn)
        {
            FadeIn(renderer);
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