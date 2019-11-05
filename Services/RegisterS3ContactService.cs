using Microsoft.AspNetCore.Mvc;
using Site.ServiceModels;
using System;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;

namespace Site.Services
{
    
    public class RegisterS3ContactService : IRegisterContactService
    {
        Logger logger = LogManager.GetLogger("aws");
        
        private const string bucketName = "polarcloud";
        private const string keyName = "contacts/web";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest1; 
        private static IAmazonS3 client;

        async Task IRegisterContactService.Register(Contact contact)
        {
            contact.Id = Guid.NewGuid().ToString("N");
            logger.Info("Saving contact {0} to S3", contact.Id);

            Amazon.AWSConfigs.RegionEndpoint = RegionEndpoint.USWest1;
            client = new AmazonS3Client(bucketRegion);

            var request = new PutObjectRequest();

            request.BucketName = bucketName;
            request.Key = keyName + "/" + contact.Id;
            request.ContentType = "application/json";
            request.ContentBody = JsonConvert.SerializeObject(contact);
            
            try {
                var response = await client.PutObjectAsync(request);
                Console.WriteLine(response.HttpStatusCode);
                logger.Info(string.Format("Saved contact {0} to S3", contact.Id));
            } catch (Exception ex) {
                logger.Error(ex, string.Format("Exception occured saving contact {0}", contact.Id));
                Console.WriteLine(ex);
            }
        }
    }
}

    
