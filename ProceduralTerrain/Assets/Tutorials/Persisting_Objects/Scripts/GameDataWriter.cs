﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace PersistingObjects
{
    public class GameDataWriter : MonoBehaviour
    {
        BinaryWriter writer;

        public GameDataWriter (BinaryWriter writer)
        {
            this.writer = writer;
        }

        public void Write (float value)
        {
            writer.Write(value);
        }

        public void Write (int value)
        {
            writer.Write(value);
        }

        public void Write (Quaternion value)
        {
            writer.Write(value.x);
            writer.Write(value.y);
            writer.Write(value.z);
            writer.Write(value.w);
        }

        public void Write(Vector3 value)
        {
            writer.Write(value.x);
            writer.Write(value.y);
            writer.Write(value.z);
        }
    }
}
