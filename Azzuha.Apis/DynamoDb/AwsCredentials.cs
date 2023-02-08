using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;

namespace Azzuha.Apis.DynamoDb
{
    public class AwsCredentials : AWSCredentials
    {
        private readonly AppConfig _appConfig;

        public AwsCredentials(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public override ImmutableCredentials GetCredentials()
        {
            return new ImmutableCredentials(_appConfig.AwsAccessKey,
                _appConfig.AwsSecretKey, null);
        }
    }

    public class AppConfig
    {
        public string AwsRegion { get; set; } = "us-east-1";
        public string AwsAccessKey { get; set; } = "AKIA5VWJF27IUFOGLVVX";
        public string AwsSecretKey { get; set; } = "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";
    }
}
