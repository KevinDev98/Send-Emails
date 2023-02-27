using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
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
        static string Str_Connect = "RABlAGYAYQB1AGwAdABFAG4AZABwAG8AaQBuAHQAcwBQAHIAbwB0AG8AYwBvAGwAPQBoAHQAdABwAHMAOwBBAGMAYwBvAHUAbgB0AE4AYQBtAGUAPQBzAHQAbwByAGEAZwBlAGEAYwBjAG8AdQBuAHQAZQB0AGwAOQA4ADsAQQBjAGMAbwB1AG4AdABLAGUAeQA9ADAATwBkACsAbQBhAGsAZwBoAG0AbwBZAEsATgBIAEMAQgBnAHEAVQBRAHQAbABtADkAdAA3AC8AMAB3AEoAUQBsAFcAWgBiAGoAawBUAHoAOABxAEMASgBVAC8AUQBTAEYASQBUAG4ALwBUAHEAVwBUAFEAYQAvAHoARQBrAFIAQwAzADMAYwB1ADAAcQBTAFcAbgBuAHYAKwBBAFMAdABiAEEANABtACsAUQA9AD0AOwBFAG4AZABwAG8AaQBuAHQAUwB1AGYAZgBpAHgAPQBjAG8AcgBlAC4AdwBpAG4AZABvAHcAcwAuAG4AZQB0AA==";
        string Str_Connect2;
        WebClient clientWeb;
        Stream streamAzure;
        public List<String> ListBlobFile(string PathBlob, string ContainerBlobName)
        {
            List<String> listName = new List<String>();
            Str_Connect2 = Security.DesEncriptar(Str_Connect);
            BlobContainerClient containerClient = new BlobContainerClient(Str_Connect2, ContainerBlobName);//Recibe cadena de conexion y nombre de contenedor
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
            Str_Connect2 = Security.DesEncriptar(Str_Connect);
            BlobStrg = new BlobClient(Str_Connect2, ContainerName, "");
            string url = BlobStrg.Uri.ToString() + "/";
            return url;
        }
        public Stream StreamGetStream(string ContainerName, string FileName)
        {
            try
            {
                Str_Connect2 = Security.DesEncriptar(Str_Connect);
                BlobStrg = new BlobClient(Str_Connect2, ContainerName, FileName);
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