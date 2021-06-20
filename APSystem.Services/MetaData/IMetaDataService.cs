namespace APSystem.Services.MetaData
{
    public interface IMetaDataService
    {
         bool SetAttribute(string key, string val);
        bool SetAuth(Models.Auth.AuthResponse authResponse);

        Models.Auth.RequestMetaData GetMetaData();
    }
}