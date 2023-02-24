using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendGridAzureEmail.Class
{
    public class Azure
    {
        SecurityClass Security = new SecurityClass();
        static string Str_Connect = "RABlAGYAYQB1AGwAdABFAG4AZABwAG8AaQBuAHQAcwBQAHIAbwB0AG8AYwBvAGwAPQBoAHQAdABwAHMAOwBBAGMAYwBvAHUAbgB0AE4AYQBtAGUAPQBzAHQAbwByAGEAZwBlAGEAYwBjAG8AdQBuAHQAZQB0AGwAOQA4ADsAQQBjAGMAbwB1AG4AdABLAGUAeQA9ADAATwBkACsAbQBhAGsAZwBoAG0AbwBZAEsATgBIAEMAQgBnAHEAVQBRAHQAbABtADkAdAA3AC8AMAB3AEoAUQBsAFcAWgBiAGoAawBUAHoAOABxAEMASgBVAC8AUQBTAEYASQBUAG4ALwBUAHEAVwBUAFEAYQAvAHoARQBrAFIAQwAzADMAYwB1ADAAcQBTAFcAbgBuAHYAKwBBAFMAdABiAEEANABtACsAUQA9AD0AOwBFAG4AZABwAG8AaQBuAHQAUwB1AGYAZgBpAHgAPQBjAG8AcgBlAC4AdwBpAG4AZABvAHcAcwAuAG4AZQB0AA==";
        public List<String> ListBlobFile(string PathBlob, string ContainerBlobName)
        {
            List<String> listName = new List<String>();
            string Str_Connect2 = Security.DesEncriptar(Str_Connect);
            BlobContainerClient containerClient = new BlobContainerClient(Str_Connect2, ContainerBlobName);//Recibe cadena de conexion y nombre de contenedor
            var ListblobFiles = containerClient.GetBlobs();
            foreach (BlobItem blobItem in ListblobFiles)
            {
                listName.Add(blobItem.Name);
            }
            //container.UploadBlob();
            return listName;
        }
    }
}