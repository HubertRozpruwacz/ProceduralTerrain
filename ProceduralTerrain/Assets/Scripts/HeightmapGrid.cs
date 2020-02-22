using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeightmapTerrain
{
    public class HeightmapGrid : MonoBehaviour
    {
        //Chunk settings

        public int sizeX = 5, sizeY = 5;
        HeightmapChunk[,] chunks;
        Transform chunkHolder;
        public Material terrainMaterial;
        public float chunkScale = 1f;

        public bool generateColliders = false;

        //Keep an eye out for changes

        bool changeMade = false;

        //Noise and terrain settings

        public float noiseScale = 1f;
        public float noiseHeight = 1f;
        [Range(2,10)]
        public int detail = 5;
        public int noiseInstances = 2;

        //public Mesh tempmesh; //Test Mesh

        private void Awake()
        {
            chunkHolder = new GameObject("ChunkHolder").transform;
            GenerateChunks();
            changeMade = true;
        }

        public void GenerateChunks()
        {
            chunks = new HeightmapChunk[sizeX, sizeY];
            UpdateChunks();
        }
        public void UpdateChunks()
        {
            if (changeMade)
            {
                foreach (HeightmapChunk chunk in chunks)
                {
                    Destroy(chunk.gameObject);
                }
            }

            chunks = new HeightmapChunk[sizeX, sizeY];

            for (int x = 0; x < chunks.GetLength(0); x++)
                for (int y = 0; y < chunks.GetLength(1); y++)
                {
                    GameObject obj = new GameObject("Chunk_" + x + "_" + y);
                    obj.transform.SetParent(chunkHolder);
                    obj.transform.position = new Vector3(x, 0f, y) * chunkScale;
                    obj.transform.rotation = Quaternion.identity;

                    chunks[x, y] = obj.AddComponent<HeightmapChunk>();
                    chunks[x, y].CreateComponents();
                    /*
                    // temporary stuff
                    chunks[x, y].Mesh = tempmesh;
                    chunks[x, y].RefreshChunk(terrainMaterial, false);
                    //temporary stuff
                    */
                    GenerateMesh(chunks[x, y], x, y);
                }
                
        }

        public float height(float x, float xi, float y, float yi)
        {
            float stepX = 0f;
            float stepY = 0f;
            float height = 0f;
            if (xi > 0)
                stepX = xi / (detail + 0f);

            if (yi > 0)
                stepY = yi / (detail + 0f);
            for (int i = 1; i < noiseInstances + 1; i++)
            {
                height += Mathf.PerlinNoise((stepX + x) * noiseScale * i, (stepY + y) * noiseScale * i) * (noiseHeight/i);
            }
            return height;
        }

        public void GenerateMesh(HeightmapChunk chunk, int x, int y)
        {
            Mesh m = new Mesh();
            m.name = "chunk_" + x + "_" + y;
            int[] triangles = new int[detail * detail * 6];
            Vector3[] vertices = new Vector3[(detail+1) * (detail+1)];
            Vector2[] uv = new Vector2[(detail + 1) * (detail + 1)];
            Vector3[] normals = new Vector3[(detail + 1) * (detail + 1)];



            for (int yi = 0, i = 0; yi < detail + 1; yi++)
                for (int xi = 0; xi < detail + 1; xi++, i++)
                {
                    float stepX = 0f;
                    float stepY = 0f;
                    if (xi > 0)
                        stepX = xi / (detail + 0f);

                    if (yi > 0)
                        stepY = yi / (detail + 0f);

                    vertices[i] = new Vector3(stepX * chunkScale, height(x,xi,y,yi), stepY * chunkScale);
                    uv[i] = new Vector2(stepX, stepY);

                    float normX = height(x, xi - 1f, y, yi) - height(x, xi + 1f, y, yi);
                    float normY = height(x, xi, y, yi - 1f) - height(x, xi, y, yi + 1f);
                    normals[i] = new Vector3(normX, 0.5f, normY);
                }

            for (int i = 0, xi = 0; xi < detail; xi++)
                for (int yi = 0; yi < detail; yi++, i += 6)
                {
                    triangles[i] = xi + (yi * (detail +1));
                    triangles[i + 3] = triangles[i + 2] = xi + 1 + (yi * (detail + 1));
                    triangles[i + 1] = triangles[i + 4] = xi + (detail) + 1 + (yi * (detail + 1));
                    triangles[i + 5] = xi + (detail + 1) + 1 + (yi * (detail + 1));
                }


            m.vertices = vertices;
            m.triangles = triangles;
            m.uv = uv;
            m.normals = normals;

            chunk.Mesh = m;
            chunk.RefreshChunk(terrainMaterial, generateColliders);
        }
    }
}
