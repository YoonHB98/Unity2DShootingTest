using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Rendering;
using System;


public class SaveData : MonoBehaviour
{


    public static string ObjectToStringSerialize(object obj)
    {
        using(var memory = new MemoryStream())
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memory, obj);
            memory.Flush();
            memory.Position = 0;
            return System.Convert.ToBase64String(memory.ToArray());
        }
    }

    public T Deserialize<T>(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            byte[] b = Convert.FromBase64String(data);
            using (var stream = new MemoryStream(b))
            {
                var formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }else
        {
            return default(T);
        }
    }
}
