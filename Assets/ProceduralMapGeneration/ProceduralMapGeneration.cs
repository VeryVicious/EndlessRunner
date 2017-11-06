using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.ProceduralMapGeneration
{
    public class ProceduralMapGeneration : MonoBehaviour
    {
        public int HeightMapResolution;
        public int AlphaMapResolution;
        public int Length;
        public int Height;
        public int X;
        public int Y;
        public int Z;
        public int Width;
        public int MapLength;
        public bool SkipRandomizingTerrain;


        private bool isGenerationRunning = false;
        private int zPosition = 0;
        private Camera mainCamera;
        private System.Random rnd;
        private List<float[,]> heightMaps;
        private NoiseProvider noiseProvider;


        // Use this for initialization
        void Start()
        {
            mainCamera = Camera.main;
            GenerateTerrain(zPosition);
            zPosition = Length * MapLength;
            rnd = new System.Random();
            heightMaps = new List<float[,]>();
            noiseProvider = new NoiseProvider();
        }

        // Update is called once per frame
        void Update()
        {
            var terrainArray = FindObjectsOfType(typeof(Terrain));
            foreach (Terrain terrain in terrainArray)
            {
                if (terrain.transform.position.z < mainCamera.transform.position.z)
                {
                    if (SkipRandomizingTerrain)
                    {
                        MoveTerrain(terrain, zPosition, null);
                    }
                    else
                    {
                        if (heightMaps.Count != 0)
                        {
                            MoveTerrain(terrain, zPosition, heightMaps[0]);
                            heightMaps.RemoveAt(0);
                        }
                    }

                }
            }

            if (heightMaps.Count == 0 && !isGenerationRunning && !SkipRandomizingTerrain)
            {
                isGenerationRunning = true;
                Task.Factory.StartNew(() =>
                {
                    RandomizeHeightMaps();
                });
            }

            var oldCamPosition = mainCamera.transform.position;
            var newCamPosition = new Vector3(oldCamPosition.x, oldCamPosition.y, oldCamPosition.z + 1f);
            mainCamera.transform.position = newCamPosition;
        }


        private void RandomizeHeightMaps()
        {
            Debug.Log("Starting generating height maps..." + Task.CurrentId.Value);
            var settings = new TerrainChunkSettings
            {
                HeightMapResolution = HeightMapResolution,
                AlphamapResolution = AlphaMapResolution,
                Length = Length,
                Height = Height
            };

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < MapLength; j++)
                {
                    heightMaps.Add(new TerrainChunk(settings, noiseProvider, i + X, j + Z, Y).GetHeightmap());
                }
            }

            isGenerationRunning = false;
        }

        private void MoveTerrain(Terrain terrain, int zPosition, float[,] heightMap)
        {
            if (heightMap != null)
            {
                terrain.terrainData.SetHeights(0, 0, heightMap);
            }

            var newPos = new Vector3(terrain.transform.position.x, terrain.transform.position.y, terrain.transform.position.z + zPosition);
            terrain.transform.position = newPos;
        }

        void GenerateTerrain(int zPosition)
        {
            var settings = new TerrainChunkSettings
            {
                HeightMapResolution = HeightMapResolution,
                AlphamapResolution = AlphaMapResolution,
                Length = Length,
                Height = Height
            };

            var noiseProvider = new NoiseProvider();

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < MapLength; j++)
                {
                    new TerrainChunk(settings, noiseProvider, i + X, j + Z, Y).CreateTerrain();
                }
            }
        }
    }
}

