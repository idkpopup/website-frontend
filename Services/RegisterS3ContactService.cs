﻿using Site.ServiceModels;
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
        
        private const string bucketName = "idkpopup-website";
        private const string keyName = "contacts/web";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest1; 
        private static IAmazonS3 client;

        /// <summary>
        /// Saves a contact to S3.
        /// </summary>
        /// <param name="contact">A contact taken from the registration form</param>
        /// <returns></returns>
        async Task IRegisterContactService.Register(Contact contact)
        {
            contact.Id = Guid.NewGuid().ToString("N");
            logger.Info("Prepairing contact {0} for S3", contact.Id);

            Amazon.AWSConfigs.RegionEndpoint = RegionEndpoint.USWest1;
            client = new AmazonS3Client(bucketRegion);

            var request = new PutObjectRequest();

            request.BucketName = bucketName;
            request.Key = keyName + "/" + contact.Id;
            request.ContentType = "application/json";
            request.ContentBody = JsonConvert.SerializeObject(contact);
            
            try {
                logger.Info("Pushing contact {0} to S3", contact.Id);
                var response = await client.PutObjectAsync(request);
                logger.Info(string.Format("Saved contact {0} to S3", contact.Id));                
            } catch (Exception ex) {
                logger.Error(ex, string.Format("Exception occured saving contact {0} {1}", contact.Id, ex.Message));
            }
        }
    }
}

    
