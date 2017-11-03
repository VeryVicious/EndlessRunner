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
        public int X;
        public int Y;
        public int Z;

        public int MapLength;

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
            {
                for (var j = 0; j < MapLength; j++)
                {               
                    var chunk = new TerrainChunk(settings, noiseProvider, i + X, j + Z, Y);
                    chunk.CreateTerrain();                  
                }
                   
            }         
        }
    }
}

