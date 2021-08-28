namespace LocalstackBridge.Abstractions
{
    public class LocalCredentials
    {
        public LocalCredentials()
        {
            AwsAccessKeyId = "111111";
            AwsSecretAccessKey = "111111";
            ServiceURL = "http://localhost:4566";
        }

        public LocalCredentials(string awsAccessKeyId, string awsSecretAccessKey, string serviceURL)
        {
            AwsAccessKeyId = awsAccessKeyId;
            AwsSecretAccessKey = awsSecretAccessKey;
            ServiceURL = serviceURL;
        }

        public string AwsAccessKeyId { get; }
        public string AwsSecretAccessKey { get; }
        public string ServiceURL { get; }
    }
}
