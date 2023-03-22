// using Amazon.S3;
// using Amazon.S3.Model;
using System.Text.Json;

public class AWSS3Wrapper

{
    private Amazon.RegionEndpoint endpoint;
    
    private AWSS3Wrapper(Amazon.RegionEndpoint endpoint){
        this.endpoint = endpoint;
    }

    public static AWSS3Wrapper NewS3Connection(Amazon.RegionEndpoint endpoint){
        return AWSS3Wrapper(endpoint);
    }
    public async Task<List<string>> ListS3BucketContents(string bucketName)
    {
        List<string> data = new();
        using (var client = new AmazonS3Client(endpoint))
        {
            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = bucketName,
                };
                ListObjectsV2Response response;

                do
                {
                    response = await client.ListObjectsV2Async(request);
                    response.S3Objects
                        .ForEach((obj) =>
                        {
                            data.Add(obj.Key);
                        });
                    request.ContinuationToken = response.NextContinuationToken;
                }
                while (response.IsTruncated);
            }
            catch
            {
                data.Add("Issues retreiving content.");
            }
        }
        return data;
    }

    public async Task<T> GetS3Item<T>(string bucketName, string key)
    {
        string contents = "";
        using (var client = new AmazonS3Client(endpoint))
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                };
                // Issue request and remember to dispose of the response
                using GetObjectResponse response = await client.GetObjectAsync(request);
                using (StreamReader reader = new StreamReader(response.ResponseStream))
                {
                    contents = reader.ReadToEnd();
                }
                return JsonSerializer.Deserialize<T>(contents)!;
            }
            catch
            {
                return JsonSerializer.Deserialize<T>("")!;
            }
        }
    }

    public async Task<bool> PutS3Item(string bucketName, string key, object contents)
    {
        using (var client = new AmazonS3Client(endpoint))
        {
            try
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                    ContentBody = JsonSerializer.Serialize(contents)
                };
                await client.PutObjectAsync(request);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public async Task<bool> DeleteS3Item(string bucketName, string key)
    {
        using (var client = new AmazonS3Client(endpoint))
        {
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = key,
                };
                await client.DeleteObjectAsync(request);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}