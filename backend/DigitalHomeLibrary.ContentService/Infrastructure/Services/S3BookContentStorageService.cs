using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using DigitalHomeLibrary.ContentService.Domain.Services;

namespace DigitalHomeLibrary.ContentService.Infrastructure.Services
{
    public class S3BookContentStorageService : IBookContentStorageService
    {
        readonly AmazonS3Client _s3Client;
        readonly string _bucketName = string.Empty;
        readonly int _bigFileMinLen;

        public S3BookContentStorageService(IConfiguration configuration)
        {
            var config = new AmazonS3Config
            {
                ServiceURL = configuration["S3Storage:ServiceURL"],
                ForcePathStyle = true,
            };

            _s3Client = new AmazonS3Client(
                configuration["S3Storage:AccessKey"],
                configuration["S3Storage:SecretKey"],
                config
            );

            _bucketName = configuration["S3Storage:BucketName"] ?? throw new ArgumentException("S3Storage:BucketName is empty");
            _bigFileMinLen = int.Parse(configuration["S3Storage:BigFileMinLen"] ?? throw new ArgumentException("S3Storage:BigFileMinLen is empty"));
        }

        public async Task DeleteFileAsync(string keyName)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = keyName
            };

            await _s3Client.DeleteObjectAsync(request);
        }

        public async Task<Stream> DownloadFileAsync(string keyName)
        {
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = keyName
            };

            var response = await _s3Client.GetObjectAsync(request);
            return response.ResponseStream;
        }

        public async Task<string> UploadFile(IFormFile file, string keyName, Action<int>? progressCallback = null)
        {
            using MemoryStream memoryStream = new();
            file.CopyTo(memoryStream);

            if (file.Length < _bigFileMinLen)
            {
                var request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = keyName,
                    InputStream = memoryStream,
                    UseChunkEncoding = false
                };

                await _s3Client.PutObjectAsync(request);
            }
            else
                await UploadBigFile(keyName, progressCallback, memoryStream);

            return keyName;
        }

        private async Task UploadBigFile(string keyName, Action<int>? progressCallback, MemoryStream memoryStream)
        {
            var fileTransferUtility = new TransferUtility(_s3Client);
            var fileTransferUtilityRequest = new TransferUtilityUploadRequest
            {
                BucketName = _bucketName,
                Key = keyName,
                InputStream = memoryStream,
                StorageClass = S3StorageClass.StandardInfrequentAccess,
                PartSize = 6291456, // 6 MB.
                CannedACL = S3CannedACL.PublicRead
            };

            fileTransferUtilityRequest.UploadProgressEvent += (s, e) =>
            {
                progressCallback?.Invoke(e.PercentDone);
            };

            await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
        }
    }
}
