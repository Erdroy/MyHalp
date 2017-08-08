// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// MyProfiler - runtime visual debuggning tool.
    /// </summary>
    public class MyProfiler
    {
        private class MyProfilerHandler : MyComponent
        {
            public struct Frame
            {
                public double FrameTime; // in ms
                //public double[] Profiled; // in ms
            }
            
            public List<Frame> Frames;
            public int Height;

            private DateTime _lastFrameTime;
            private Material _material;
            private Mesh _mesh;

            private Canvas _canvas;
            private CanvasRenderer _renderer;
            private RectTransform _rect;

            // override `OnInit`
            protected override void OnStart()
            {
                _material = new Material(Shader.Find("Unlit/Color"));

                // create needed components
                _canvas = gameObject.AddComponent<Canvas>();
                _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                
                var profiler = new GameObject("profiler");
                profiler.transform.parent = transform;
                profiler.transform.localPosition = Vector3.zero;
                _rect = profiler.AddComponent<RectTransform>();
                _renderer = profiler.AddComponent<CanvasRenderer>();

                StartCoroutine(FrameCounter());
            }

            // private
            private void LateUpdate()
            {
                // switch cameraas
                _canvas.worldCamera = Camera.main;

                // build mesh for frames setup
                BuildMesh();

                // update position
                _rect.pivot = Vector2.zero;
                _rect.anchoredPosition = Vector2.zero;
                _rect.anchorMin = Vector2.zero;

                // update canvas renderer
                _renderer.materialCount = 1;
                _renderer.SetMesh(_mesh);
                _renderer.SetMaterial(_material, 0);
            }

            // private
            private void BuildMesh()
            {
                if (Frames.Count == 0)
                    return;

                if (_mesh)
                    _mesh.Clear();
                else
                    _mesh = new Mesh();

                var verts = new List<Vector3>();
                var colors = new List<Color>();
                var idx = new List<int>();

                var minTime = Frames.Min(x => x.FrameTime);
                var maxTime = Frames.Max(x => x.FrameTime);

                var ctrFps = 1.0f / (maxTime * 0.5f);
                var minFps = 1.0f / maxTime;
                
                var minHeight = Height * 0.2f;
                var maxHeight = Height;

                var heightScale = maxHeight - minHeight;
                var scale = maxTime - minTime;
                
                var xOffset = 0.0f;
                // build profiling bars
                foreach (var frame in Frames)
                {
                    var index = verts.Count;
                    var time = frame.FrameTime - minTime;
                    var height = heightScale * (float)(time / scale);

                    // TODO: build bar including profiled profiles

                    verts.Add(new Vector3((xOffset + 0.0f), 0.0f, 0.0f));
                    verts.Add(new Vector3((xOffset + 0.0f), height, 0.0f));
                    verts.Add(new Vector3((xOffset + 2.0f), height, 0.0f));
                    verts.Add(new Vector3((xOffset + 2.0f), 0.0f, 0.0f));

                    colors.Add(Color.red);
                    colors.Add(Color.red);
                    colors.Add(Color.red);
                    colors.Add(Color.red);

                    idx.Add(index + 0);
                    idx.Add(index + 1);
                    idx.Add(index + 2);

                    idx.Add(index + 2);
                    idx.Add(index + 3);
                    idx.Add(index + 0);
                    
                    xOffset += 3.0f;
                }
                
                // TODO: draw fps count(ctr and min)

                _mesh.SetVertices(verts);
                _mesh.SetColors(colors);
                _mesh.SetIndices(idx.ToArray(), MeshTopology.Triangles, 0);
                _mesh.UploadMeshData(false);
            }

            // private
            private Frame GetFrameInfo()
            {
                var frame = new Frame();
                var currentTime = DateTime.Now;

                var delta = currentTime - _lastFrameTime;
                frame.FrameTime = delta.TotalMilliseconds;
                
                _lastFrameTime = currentTime;
                return frame;
            }

            // private
            private void PushFrame()
            {
                // make sure that this will not run out of space
                if (Frames.Count + 1 >= Frames.Capacity)
                    Frames.RemoveAt(0);
                
                // get frame info and add frame
                Frames.Add(GetFrameInfo());
            }

            // private
            private IEnumerator FrameCounter()
            {
                while (true)
                {
                    yield return new WaitForEndOfFrame();

                    PushFrame();
                }
            }

            // internal
            internal void ShowCanvas()
            {
                _canvas.gameObject.SetActive(true);
            }

            // internal
            internal void HideCanvas()
            {
                _canvas.gameObject.SetActive(false);
            }
        }

        private static MyProfilerHandler _handlerInstance;

        /// <summary>
        /// Initialize MyProfiler.
        /// </summary>
        /// <param name="frameCount">The buffered frame count.</param>
        /// <param name="height">The max height of the panel.</param>
        public static void Init(int frameCount = 60 * 4, int height = 100)
        {
            if (_handlerInstance)
            {
                Debug.LogError("MyProfiler can be initialized only once!");
                return;
            }

            _handlerInstance = MyInstancer.Create<MyProfilerHandler>();
            _handlerInstance.Frames = new List<MyProfilerHandler.Frame>(frameCount);
            _handlerInstance.Height = height;

            for (var i = 0; i < frameCount; i++)
            {
                _handlerInstance.Frames.Add(new MyProfilerHandler.Frame
                {
                    FrameTime = 0.0f
                });
            }
        }

        /// <summary>
        /// Show panel when hidden.
        /// </summary>
        public static void Show()
        {
            _handlerInstance.ShowCanvas();
        }

        /// <summary>
        /// Hide the panel.
        /// </summary>
        public static void Hide()
        {
            _handlerInstance.HideCanvas();
        }

        /// <summary>
        /// Begin new profiling.
        /// </summary>
        /// <param name="title">The profile title.</param>
        /// <param name="color">The profile color.</param>
        public static void Begin(string title, Color color)
        {
            // TODO: push new profile
        }

        /// <summary>
        /// End profiling.
        /// </summary>
        public static void End()
        {
            // TODO: end current profile and add to the current frame
        }
    }
}

