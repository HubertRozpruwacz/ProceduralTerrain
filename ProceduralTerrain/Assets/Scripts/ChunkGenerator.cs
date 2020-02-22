using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarchingCubes
{
    [ExecuteInEditMode]
    public class ChunkGenerator : MonoBehaviour
    {
        List<List<Chunk>> chunks = new List<List<Chunk>>(); // first list - y, second - x
        public int sizeX = 5;
        public int sizeY = 5;

        public void SetChunks()
        {
            for (int x = 0; x < sizeX; x++)
            {
                if (chunks.Count < x) // x = 2, count = 3
                    chunks.Add(new List<Chunk>());
                else
                    chunks[x] = new List<Chunk>();

                for (int y = 0; y < sizeY; y++)
                {
                    if (chunks[x].Count < y)
                        chunks[y].Add(new GameObject("Chunk_" + x + ", " + y).gameObject.AddComponent<Chunk>());
        }
            }
        }

        public void Generate (Vector3Int p)
        {
            
        }
    }
}