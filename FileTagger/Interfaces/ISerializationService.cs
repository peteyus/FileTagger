namespace FileTagger.Interfaces
{
    public interface ISerializationService
    {
        string SerializeObject<T>(T objectToSerialize);

        T DeserializeObject<T>(string serializedObject);
    }
}
