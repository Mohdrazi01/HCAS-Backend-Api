using APSystem.Models.Auth;

namespace APSystem.Services.MetaData
{
    public class MetaDataService : IMetaDataService
    {
        private RequestMetaData _requestMetaData;
        public MetaDataService()
        {
            _requestMetaData = new RequestMetaData();
        }



        RequestMetaData IMetaDataService.GetMetaData()
        {
            return _requestMetaData;
        }

        bool IMetaDataService.SetAttribute(string key, string val)
        {
            if (_requestMetaData.Attributes.ContainsKey(key) || string.IsNullOrWhiteSpace(val))
                return false;

            _requestMetaData.Attributes.Add(key, val);
            return true;
        }

        bool IMetaDataService.SetAuth(AuthResponse authResponse)
        {
            if (authResponse == null)
                return false;
            _requestMetaData.AuthData = authResponse;
            return true;
        }

    }
}