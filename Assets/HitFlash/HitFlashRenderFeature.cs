using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HitFlashRenderFeature : ScriptableRendererFeature
{
    private class HitflashInfo
    {
        public GameObject target;
        public IRenderTarget[] renderTargets;
        public float startTime;
        public HitflashInfo(GameObject target)
        {
            this.target = target;
            var renderers = target.GetComponentsInChildren<Renderer>();
            var renderTargetList = new List<IRenderTarget>();
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i] is MeshRenderer) renderTargetList.Add(new MeshRendererRenderTarget((MeshRenderer)renderers[i]));
                if (renderers[i] is SkinnedMeshRenderer) renderTargetList.Add(new SkinnedMeshRendererRenderTarget((SkinnedMeshRenderer)renderers[i]));
            }
            this.renderTargets = renderTargetList.ToArray();
            this.startTime = Time.time;
        }

        private class SkinnedMeshRendererRenderTarget : IRenderTarget
        {
            public SkinnedMeshRendererRenderTarget(SkinnedMeshRenderer renderer) => smr = renderer;
            public Renderer Renderer => smr;
            private SkinnedMeshRenderer smr;
            public Mesh Mesh => smr.sharedMesh;
            public Material[] Materials => smr.sharedMaterials;
        }
        private class MeshRendererRenderTarget : IRenderTarget
        {
            public MeshRendererRenderTarget(MeshRenderer renderer) => mf = (mr = renderer).GetComponent<MeshFilter>();
            public Renderer Renderer => mr;
            private MeshFilter mf;
            private MeshRenderer mr;
            public Mesh Mesh => mf.sharedMesh;
            public Material[] Materials => mr.sharedMaterials;
        }
        public interface IRenderTarget
        {
            public Renderer Renderer { get; }
            public Mesh Mesh { get; }
            public Material[] Materials { get; }
        }
    }

    private static readonly List<HitflashInfo> activeHitFlashes = new();
    public static void Flash(GameObject target)
    {
        activeHitFlashes.Add(new(target));
    }

    [System.Serializable]
    private class HitFlashSettings
    {
        public Color32 color = (Color32)Color.white;
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        public float duration = .2f;
    }

    [SerializeField] private HitFlashSettings settings;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }

    private HitFlashRenderPass pass;
    public override void Create()
    {
        pass = new HitFlashRenderPass(settings);
        pass.renderPassEvent = settings.renderPassEvent;
    }

    private class HitFlashRenderPass : ScriptableRenderPass, IDisposable
    {
        private HitFlashSettings settings;
        private Material hitflashBlitMaterial;
        public HitFlashRenderPass(HitFlashSettings settings)
        {
            this.settings = settings ?? new();
            hitflashBlitMaterial = new Material(Shader.Find("Hitflash/Blit"));
            hitflashBlitMaterial.SetColor("color", settings.color);
        }

        public void Dispose()
        {
            DestroyImmediate(hitflashBlitMaterial);
            FlashMaskRT?.Release();
        }

        private RTHandle FlashMaskRT;
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var camTargetDesc = renderingData.cameraData.cameraTargetDescriptor;
            if (FlashMaskRT == null || FlashMaskRT.rt.width != camTargetDesc.width || FlashMaskRT.rt.height != camTargetDesc.height)
            {
                if (FlashMaskRT != null) FlashMaskRT.Release();
                FlashMaskRT = RTHandles.Alloc(width: camTargetDesc.width, height: camTargetDesc.height, name: "hitFlashMask", colorFormat: UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm);
            }

            //remove old entries
            var minValidStartTime = Time.time - settings.duration;
            while (activeHitFlashes.Count > 0 && activeHitFlashes[0].startTime < minValidStartTime)
                activeHitFlashes.RemoveAt(0);

            //render active entries
            var cmd = CommandBufferPool.Get();
            cmd.name = "DrawHitFlash";
            for (int i = 0; i < activeHitFlashes.Count; i++)
            {
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();
                var hitflash = activeHitFlashes[i];
                hitflashBlitMaterial.SetFloat("t", (hitflash.startTime - Time.time) / settings.duration);
                for (int r = 0; r < hitflash.renderTargets.Length; r++)
                {
                    var renderTarget = hitflash.renderTargets[r];
                    var mesh = renderTarget.Mesh;
                    var materials = renderTarget.Materials;
                    for (int m = 0; m < mesh.subMeshCount; m++)
                    {
                        var shaderPass = materials[m].FindPass("ForwardLit");
                        cmd.SetRenderTarget(FlashMaskRT, depth: renderingData.cameraData.renderer.cameraDepthTarget);
                        cmd.ClearRenderTarget(RTClearFlags.Color, new Color(0, 0, 0, 0), 1f, 0);
                        cmd.DrawRenderer(renderTarget.Renderer, materials[m], m, shaderPass);
                    }
                    cmd.Blit(FlashMaskRT, renderingData.cameraData.renderer.cameraColorTarget, mat: hitflashBlitMaterial);
                    context.ExecuteCommandBuffer(cmd);
                    cmd.Clear();
                }
            }
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }
    }
}
