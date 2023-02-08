using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Azzuha.Apis.DynamoDb
{
    public class DynamoClient
    {
        public static AmazonDynamoDBClient CreateClient(AppConfig appConfig)
        {
            var dynamoDbConfig = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(appConfig.AwsRegion)
            };
            var awsCredentials = new AwsCredentials(appConfig);
            return new AmazonDynamoDBClient(awsCredentials, dynamoDbConfig);
        }
    }
}
