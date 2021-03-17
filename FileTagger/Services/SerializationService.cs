using FileTagger.Interfaces;
using Newtonsoft.Json;
using System;

namespace FileTagger.Services
{
    public class SerializationService : ISerializationService
    {
        public T DeserializeObject<T>(string serializedObject)
        {
            if (string.IsNullOrWhiteSpace(serializedObject))
            {
                throw new ArgumentNullException(nameof(serializedObject));
            }

            return JsonConvert.DeserializeObject<T>(serializedObject);
        }

        public string SerializeObject<T>(T objectToSerialize)
        {
            if (objectToSerialize == null)
            {
                throw new ArgumentNullException(nameof(objectToSerialize));
            }

            return JsonConvert.SerializeObject(objectToSerialize);
        }
    }
}
