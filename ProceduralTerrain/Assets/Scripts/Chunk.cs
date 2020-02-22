using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarchingCubes
{
    public class Chunk : MonoBehaviour
    {
        public Mesh mesh;
        public Vector3Int coord;

        MeshFilter mf;
        MeshRenderer mr;
        MeshCollider mc;

        bool generateCollider;

        public void Delete()
        {
            if (Application.isPlaying)
            {
                mesh.Clear();
                gameObject.SetActive(false);
            }
            else
                DestroyImmediate(gameObject, false);
        }

        public void Setup(bool generateCollider, Material m)
        {
            this.generateCollider = generateCollider;

            mf = GetComponent<MeshFilter>();
            if (mf == null)
                mf = gameObject.AddComponent<MeshFilter>();

            mr = GetComponent<MeshRenderer>();
            if (mr == null)
                mr = gameObject.AddComponent<MeshRenderer>();

            mesh = mf.sharedMesh;
            if (mesh == null)
            {
                mesh = new Mesh();
                mf.sharedMesh = mesh;
            }

            if (generateCollider) // if not then don't even bother with GetComponent();
            {
                mc = GetComponent<MeshCollider>();
                if (mc == null)
                    mc = gameObject.AddComponent<MeshCollider>();

                if (mc.sharedMesh == null)
                    mc.sharedMesh = mesh;

                mc.enabled = false; // update
                mc.enabled = true;
            }
            if (!generateCollider && mc != null)
                DestroyImmediate(mc);

            mr.material = m;
        }
    }
}
