using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameSever.Serializer
{
    public static class Serializer
    {
        public static byte[] Serialize<T>(T obj)
        {
            if(obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            
            return ms.ToArray();

            // return MessagePackSerializer.Serialize<T>(obj);
        }

        public static T Deserialize<T>(byte[] data)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(data, 0, data.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            T obj = (T) binForm.Deserialize(memStream);

            return obj;

            // return MessagePackSerializer.Deserialize<T>(data);
        }
    }
}