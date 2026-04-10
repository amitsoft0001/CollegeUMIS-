using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon;
using Amazon.S3.Model;

/// <summary>
/// Summary description for AmazonUploader
/// </summary>
public class AmazonUploader
{
    public AmazonUploader()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void UploadFile(System.IO.Stream localFilePath, string _bucketName, string subDirectoryInBucket, string fileNameInS3)
    {
        string _awsAccessKey = "AKIAILDMZDXGLLYHEYTA";
        string _awsSecretKey = "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";
        string BucketName = string.Empty;
        if (localFilePath != null)
        {
            AmazonS3Config S3Config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.USEast1, //its default region set by amazon
            };

            AmazonS3Client client;
            //-------------

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                BucketName = _bucketName; //no subdirectory just bucket name  
            }
            else
            {   // subdirectory and bucket name  
                BucketName = _bucketName + @"/" + subDirectoryInBucket;
            }
            //--
            using (client = new Amazon.S3.AmazonS3Client(_awsAccessKey, _awsSecretKey, S3Config))
            {

                var request = new PutObjectRequest()
                {
                    BucketName = _bucketName,
                    CannedACL = S3CannedACL.PublicRead,//PERMISSION TO FILE PUBLIC ACCESIBLE
                    Key = string.Format("",fileNameInS3),
                    InputStream = localFilePath//SEND THE FILE STREAM
                };

                var response = client.PutObject(request);
                if (Convert.ToString(response.HttpStatusCode) == "OK")
                {
                    //do what you want..
                }
            }
        }
    }
    public bool sendMyFileToS3(System.IO.Stream localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
    {
        IAmazonS3 client = new AmazonS3Client(RegionEndpoint.APSouth1);
        TransferUtility utility = new TransferUtility(client);
        TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

        if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
        {
            request.BucketName = bucketName; //no subdirectory just bucket name  
        }
        else
        {   // subdirectory and bucket name  
            request.BucketName = bucketName + @"/" + subDirectoryInBucket;
        }
        request.Key = fileNameInS3; //file name up in S3  
        request.InputStream = localFilePath;
        utility.Upload(request); //commensing the transfer 
         

        return true; //indicate that the file was sent  
    }
}