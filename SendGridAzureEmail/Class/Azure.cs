using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SendGridAzureEmail.Class
{
    public class AzureClass
    {
        SecurityClass Security = new SecurityClass();
        static BlobContainerClient container;
        static BlobClient BlobStrg;
        static string Str_Connect = "";
        WebClient clientWeb;
        Stream streamAzure;
        BlobServiceClient AzureClient;

        public AzureClass()
		{
            string KVName = ConfigurationManager.AppSettings["KVName"];
            string KVUri = "https://" + KVName + ".vault.azure.net/";
            SecretClientOptions Secretoptions = new SecretClientOptions()
            {
                Retry =
            {
                Delay= TimeSpan.FromSeconds(2),
                MaxDelay = TimeSpan.FromSeconds(16),
                MaxRetries = 5,
                Mode = RetryMode.Exponential
            }
            };
            SecretClient client = new SecretClient(new Uri(KVUri), new DefaultAzureCredential(), Secretoptions);
            string ZConnString = ConfigurationManager.AppSettings["ZConnString"];
            KeyVaultSecret Endpoint = client.GetSecret(ZConnString);

            Str_Connect = Endpoint.Value.ToString();
            AzureClient = new BlobServiceClient(Str_Connect);
        }   
        public List<String> ListBlobFile(string PathBlob, string ContainerBlobName)
        {
            List<String> listName = new List<String>();
            BlobContainerClient containerClient = new BlobContainerClient(Str_Connect, ContainerBlobName);//Recibe cadena de conexion y nombre de contenedor
            var ListblobFiles = containerClient.GetBlobs();
            foreach (BlobItem blobItem in ListblobFiles)
            {
                listName.Add(blobItem.Name);
            }
            //container.UploadBlob();
            return listName;
        }
        public string GetUrl(string ContainerName)//Obtiene la URL del contenedor
        {
            BlobStrg = new BlobClient(Str_Connect, ContainerName, "");
            string url = BlobStrg.Uri.ToString() + "/";
            return url;
        }
        public Stream StreamGetStream(string ContainerName, string FileName)
        {
            try
            {
                Str_Connect = Security.DesEncriptar(Str_Connect);
                BlobStrg = new BlobClient(Str_Connect, ContainerName, FileName);
                clientWeb = new WebClient();
                string urlfile = BlobStrg.Uri.ToString();
                FileName = BlobStrg.Name;
                streamAzure = clientWeb.OpenRead(urlfile);//Transforma los datos del archivo de origen
            }
            catch (Exception ex)
            {

            }
            return streamAzure;
        }
    }
}