using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeightmapTerrain
{
    public class HeightmapChunk : MonoBehaviour
    {
        private MeshFilter mf;
        private MeshRenderer mr;
        private MeshCollider mc;
        private Mesh mesh;

        public Mesh Mesh
        {
            get
            {
                return mesh;
            }
            set
            {
                mesh = value;
            }
        }
        public void CreateComponents()
        {
            mf = gameObject.AddComponent<MeshFilter>();
            mr = gameObject.AddComponent<MeshRenderer>();
            mc = gameObject.AddComponent<MeshCollider>();
        }

        public void RefreshChunk(Material mat, bool generateCollider)
        {
            if (generateCollider)
            {       
                mc.sharedMesh = mesh;
                mc.enabled = false;
                mc.enabled = true;
            }
            else
            {
                mc.sharedMesh = null;
                mc.enabled = false;
            }
            mf.sharedMesh = mesh;
            mr.material = mat;
        }
    }
}
