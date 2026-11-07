using System;
using System.Collections.Generic;
using System.Text;

namespace AbpApiBook.MinIO
{
    namespace AbpApiBook.MinIO
    {
        public class MinioOptions
        {
            public const string SectionName = "MinIO";

            public string Endpoint { get; set; } = string.Empty;

            public string AccessKey { get; set; } = string.Empty;

            public string SecretKey { get; set; } = string.Empty;

            public string BucketName { get; set; } = string.Empty;

            public bool UseSSL { get; set; }
        }
    }
}
