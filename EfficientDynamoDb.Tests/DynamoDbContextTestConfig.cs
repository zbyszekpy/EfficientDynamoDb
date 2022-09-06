using System;
using Amazon.Runtime;
using EfficientDynamoDb.Configs;
using RegionEndpoint = Amazon.RegionEndpoint;

namespace EfficientDynamoDb.Tests
{
    public static class DynamoDbContextTestConfig
    {
        private const string AwsDefaultRegion = "AWS_DEFAULT_REGION";
        private static readonly string AwsRegion = Environment.GetEnvironmentVariable(AwsDefaultRegion);

        private static DynamoDbContextConfig _config;
        private static IDynamoDbContext _context;

        public static DynamoDbContextConfig Config
        {
            get
            {
                if (_config != null)
                {
                    return _config;
                }

                var endpointUrl = "http://localhost:8000";

                var endpoint = CreateRegionEndpoint(endpointUrl);
                var credentialsProvider = CreateCredentialsProvider();

                _config = new DynamoDbContextConfig(endpoint, credentialsProvider);

                return _config;
            }
        }

        public static IDynamoDbContext Context
        {
            get
            {
                return _context ??= new DynamoDbContext(Config);
            }
        }

        private static AwsCredentials CreateCredentialsProvider()
        {
            var awsCredentials = FallbackCredentialsFactory.GetCredentials();
            var credentials = awsCredentials.GetCredentials();
            var credentialsProvider = new AwsCredentials(credentials.AccessKey, credentials.SecretKey);
            return credentialsProvider;
        }

        private static EfficientDynamoDb.Configs.RegionEndpoint CreateRegionEndpoint(string endpointUrl)
        {
            var region = AwsRegion != null ? RegionEndpoint.GetBySystemName(AwsRegion) : RegionEndpoint.EUWest2;
            var endpoint = string.IsNullOrWhiteSpace(endpointUrl)
                ? EfficientDynamoDb.Configs.RegionEndpoint.Create(region.SystemName)
                : EfficientDynamoDb.Configs.RegionEndpoint.Create(region.SystemName, endpointUrl);
            return endpoint;
        }
    }
}
