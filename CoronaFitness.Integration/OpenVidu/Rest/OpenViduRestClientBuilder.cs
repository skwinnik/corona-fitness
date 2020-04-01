using CoronaFitness.Integration.OpenVidu.Settings;
using RestSharp;
using RestSharp.Authenticators;

namespace CoronaFitness.Integration.OpenVidu.Rest
{
    public class OpenViduRestClientBuilder
    {
        private readonly IxOpenViduSettings settings;

        public OpenViduRestClientBuilder(IxOpenViduSettings setting)
        {
            this.settings = setting;
        }

        public RestClient Build()
        {
            return new RestClient(this.settings.Url)
            {
                Authenticator = new HttpBasicAuthenticator(this.settings.Login, this.settings.Password)
            };
        }
    }
}