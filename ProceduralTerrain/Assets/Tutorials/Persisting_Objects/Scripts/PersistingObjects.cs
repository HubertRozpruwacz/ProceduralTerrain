using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PersistingObjects
{


    public class PersistingObjects : MonoBehaviour
    {
        public Transform prefab;
        public KeyCode createKey = KeyCode.C;
        public KeyCode newGame = KeyCode.N;
        public KeyCode saveGame = KeyCode.S;
        public KeyCode loadGame = KeyCode.L;
        List<Transform> objects;
        string savePath;

        private void Awake()
        {
            objects = new List<Transform>();
            savePath = Path.Combine(Application.persistentDataPath, "saveFile");
        }
        private void Update()
        {
            if (Input.GetKeyDown(createKey))
            {
                CreateObject();
            }
            else if (Input.GetKeyDown(newGame))
            {
                BeginNewGame();
            }
            else if (Input.GetKeyDown(saveGame))
            {
                Save();
            }
            else if (Input.GetKeyDown(loadGame))
            {
                Load();
            }
        }

        void CreateObject()
        {
            Transform t = Instantiate(prefab);
            t.localPosition = Random.insideUnitSphere * 5f;
            t.localRotation = Random.rotation;
            t.localScale = Vector3.one * Random.Range(0.1f, 1f);
            objects.Add(t);
        }

        void BeginNewGame()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                Destroy(objects[i].gameObject);
            }
            objects.Clear();
        }

        void Save()
        {
            using (var writer = new BinaryWriter(File.Open(savePath, FileMode.Create)))
            {
                writer.Write(objects.Count);
                for (int i = 0; i < objects.Count; i++)
                {
                    Transform t = objects[i];
                    writer.Write(t.localPosition.x);
                    writer.Write(t.localPosition.y);
                    writer.Write(t.localPosition.z);
                }
            }
        }
        void Load()
        {
            BeginNewGame();
            using (var reader = new BinaryReader(File.Open(savePath, FileMode.Open)))
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    Vector3 p;
                    p.x = reader.ReadSingle();
                    p.y = reader.ReadSingle();
                    p.z = reader.ReadSingle();
                    Transform t = Instantiate(prefab);
                    t.localPosition = p;
                    objects.Add(t);
                }
            }
        }
    }
}
