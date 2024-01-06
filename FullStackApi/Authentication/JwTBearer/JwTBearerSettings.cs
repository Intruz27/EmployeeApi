namespace FullStackApi.Authentication.JwTBearer
{
    public class JwTBearerSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SignInKey { get; set; }
    }
}
