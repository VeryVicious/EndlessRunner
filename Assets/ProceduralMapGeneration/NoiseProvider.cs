﻿using LibNoise.Generator;

namespace Assets.ProceduralMapGeneration
{
    public class NoiseProvider
    {
        private Perlin PerlinNoiseGenerator;

        public NoiseProvider()
        {
            PerlinNoiseGenerator = new Perlin();
        }

        public float GetValue(float x, float z)
        {
            return (float)(PerlinNoiseGenerator.GetValue(x, 0, z) / 2f) + 0.5f;
        }
    }
}
