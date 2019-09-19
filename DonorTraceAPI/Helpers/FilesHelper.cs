using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DonorTraceAPI.Helpers
{
    public class FilesHelper
    {
        public static bool UploadPhoto(MemoryStream memoryStream, string fileName)

        {

            try

            {

                memoryStream.Position = 0;
                var folderName = Path.Combine("Contents", "Donors");

                var path = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileName);

                File.WriteAllBytes(path, memoryStream.ToArray());

            }

            catch

            {

                return false;

            }



            return true;

        }
    }
}
