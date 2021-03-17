using FileTagger.Extensions;
using FileTagger.Interfaces;
using Newtonsoft.Json;
using System;

namespace FileTagger.Services
{
    public class SerializationService : ISerializationService
    {
        public T DeserializeObject<T>(string serializedObject)
        {
            serializedObject.CheckWhetherArgumentIsNull(nameof(serializedObject));

            return JsonConvert.DeserializeObject<T>(serializedObject);
        }

        public string SerializeObject<T>(T objectToSerialize)
        {
            objectToSerialize.CheckWhetherArgumentIsNull(nameof(objectToSerialize));

            return JsonConvert.SerializeObject(objectToSerialize);
        }
    }
}
