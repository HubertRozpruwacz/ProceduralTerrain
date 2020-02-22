using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace PersistingObjects
{
    public class GameDataReader : MonoBehaviour
    {
        BinaryReader reader;

        public GameDataReader (BinaryReader reader)
        {
            this.reader = reader;
        }

        public int ReadInt ()
        {
            return reader.ReadInt32();
        }

        public float ReadFloat()
        {
            return reader.ReadSingle();
        }

        public Quaternion ReadQuaternion()
        {
            Quaternion value;
            value.x = reader.ReadSingle();
            value.y = reader.ReadSingle();
            value.z = reader.ReadSingle();
            value.w = reader.ReadSingle();
            return value;
        }

        public Vector3 ReadVector3 ()
        {
            Vector3 value;
            value.x = reader.ReadSingle();
            value.y = reader.ReadSingle();
            value.z = reader.ReadSingle();
            return value;
        }
    }
}