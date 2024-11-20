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
    private Dictionary<Renderer, Material> materialCache = new Dictionary<Renderer, Material>();
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
        // 트윈 정리
        foreach (var tween in activeTweens.Values)
        {
            tween?.Kill();
        }
        
        // 머터리얼 정리
        foreach (var material in materialCache.Values)
        {
            Destroy(material);
        }
        
        activeTweens.Clear();
        materialCache.Clear();
    }
    
    private Material GetMaterial(Renderer renderer)
    {
        if (!materialCache.TryGetValue(renderer, out Material material))
        {
            // 머터리얼 캐싱
            material = new Material(renderer.sharedMaterial);
            renderer.material = material;
            materialCache[renderer] = material;
        }
        return material;
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
        // 레이캐스트
        int hitCount = Physics.RaycastNonAlloc(
            mainCamera.transform.position,
            direction.normalized,
            hitResults,
            raycastDistance,
            wallLayer
        );
        // 감지된 렌더러 추가 및 투명화
        for (int i = 0; i < hitCount; i++)
        {
            if (hitResults[i].collider.TryGetComponent(out Renderer renderer))
            {
                hitRenderers.Add(renderer);
                FadeOut(renderer);
            }
        }
        // 투명화 중이지만 감지되지 않은 렌더러 복구목록에 추가
        foreach (var renderer in activeTweens.Keys)
        {
            if (!hitRenderers.Contains(renderer))
            {
                renderersToFadeIn.Add(renderer);
            }
        }
        // 복구목록에 있는 렌더러 투명도 복구
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
        
        Material material = GetMaterial(renderer);
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
        
        Material material = GetMaterial(renderer);
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