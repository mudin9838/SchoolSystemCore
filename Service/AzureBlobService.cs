using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using SchoolSystemCore.Data.Constants;

namespace SchoolSystemCore.Service;

public class AzureBlobService
{
    private readonly BlobServiceClient _blobClient;
    BlobContainerClient _containerClient;
    public AzureBlobService(BlobServiceClient blobClient)
    {
        _blobClient = blobClient;
        _containerClient = _blobClient.GetBlobContainerClient(BlobClientContainer.StudentImagesContainer);
    }

    public async Task<List<Azure.Response<BlobContentInfo>>> UploadFiles(IFormFile file)
    {
        var azureResponse = new List<Azure.Response<BlobContentInfo>>();
        if (file != null)
        {
            string fileName = file.FileName;
            var blobClient = _containerClient.GetBlobClient(fileName);
            var client = await blobClient.UploadAsync(file.OpenReadStream(), true);
            azureResponse.Add(client);
        }
        return azureResponse;
    }

    public async Task<List<BlobItem>> GetUploadedBlobs()
    {
        var items = new List<BlobItem>();
        var uploadedFiles = _containerClient.GetBlobsAsync();
        await foreach (BlobItem file in uploadedFiles)
        {
            items.Add(file);
        }

        return items;
    }
}
