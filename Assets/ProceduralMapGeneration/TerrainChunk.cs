using UnityEngine;

namespace Assets.ProceduralMapGeneration
{
    public class TerrainChunk
    {
        public TerrainChunk(TerrainChunkSettings settings, NoiseProvider noiseProvider, int x, int z, int y)
        {
            Settings = settings;
            NoiseProvider = noiseProvider;
            X = x;
            Z = z;
            Y = y;
        }

        public int X { get; private set; }

        public int Z { get; private set; }
        public int Y { get; set; }

        private Terrain Terrain { get; set; }

        private TerrainChunkSettings Settings { get; set; }

        private NoiseProvider NoiseProvider { get; set; }

        public void CreateTerrain()
        {
            var terrainData = new TerrainData
            {
                heightmapResolution = Settings.HeightMapResolution,
                alphamapResolution = Settings.AlphamapResolution
            };

            var heightmap = GetHeightmap();
            terrainData.SetHeights(0, 0, heightmap);
            terrainData.size = new Vector3(Settings.Length, Settings.Height, Settings.Length);

            var newTerrainGameObject = Terrain.CreateTerrainGameObject(terrainData);
            newTerrainGameObject.transform.position = new Vector3(X * Settings.Length, Y, Z * Settings.Length);

            Terrain = newTerrainGameObject.GetComponent<Terrain>();
            Terrain.Flush();
        }

        public float[,] GetHeightmap()
        {
            var heightmap = new float[Settings.HeightMapResolution, Settings.HeightMapResolution];

            for (var zRes = 0; zRes < Settings.HeightMapResolution; zRes++)
            {
                for (var xRes = 0; xRes < Settings.HeightMapResolution; xRes++)
                {
                    var xCoordinate = X + (float)xRes / (Settings.HeightMapResolution - 1);
                    var zCoordinate = Z + (float)zRes / (Settings.HeightMapResolution - 1);

                    heightmap[zRes, xRes] = NoiseProvider.GetValue(xCoordinate, zCoordinate);
                }
            }

            return heightmap;
        }
    }
}
