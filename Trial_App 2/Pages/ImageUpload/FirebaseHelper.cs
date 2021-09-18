using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Trial_App_2.Pages.ImageUpload
{
    public class FirebaseStorageHelper
    {
        FirebaseStorage firebaseStorage = new FirebaseStorage("fir-demo-71fa5.appspot.com");

        public async Task<string> UploadFile(Stream fileStream, string folderName, string fileName)
        {
            var imageUrl = await firebaseStorage
                .Child(folderName)
                .Child(fileName)
                .PutAsync(fileStream);
            return imageUrl;
        }

        public async Task<string> GetFile(string folderName, string fileName)
        {
            return await firebaseStorage
                .Child(folderName)
                .Child(fileName)
                .GetDownloadUrlAsync();
        }
        public async Task DeleteFile(string folderName, string fileName)
        {
            await firebaseStorage
                 .Child(folderName)
                 .Child(fileName)
                 .DeleteAsync();

        }
    }
}
