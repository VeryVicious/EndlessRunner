using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ProceduralMapGeneration
{
    public class ProceduralMapGeneration : MonoBehaviour
    {
        public int HeightMapResolution;
        public int AlphaMapResolution;
        public int Length;
        public int Height;

        // Use this for initialization
        void Start()
        {
            Test();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {

        }

        void Test()
        {
            var settings = new TerrainChunkSettings
            {
                HeightMapResolution = HeightMapResolution,
                AlphamapResolution = AlphaMapResolution,
                Length = Length,
                Height = Height
            };

            var noiseProvider = new NoiseProvider();

            for (var i = 0; i < 4; i++)
                for (var j = 0; j < 4; j++)
                    new TerrainChunk(settings, noiseProvider, i, j).CreateTerrain();
        }
    }
}

