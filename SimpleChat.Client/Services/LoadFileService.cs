using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SimpleChat.Client.Resources.Constants;
using SimpleChat.Client.Resources.ResourceFiles;

namespace SimpleChat.Client.Services
{
    public class LoadFileService
    {
        private const long MaxFileSize = 1048576; // 1MB

        [Inject]
        private ErrorStateService ErrorState { get; set; }
        
        public async Task<byte[]> LoadFile(InputFileChangeEventArgs e)
        {
            try
            {
                using var reader =
                    new StreamReader(e.File.OpenReadStream(MaxFileSize));
                var ms = new MemoryStream();
                await reader.BaseStream.CopyToAsync(ms);
                return ms.ToArray();
            }
            catch
            {
                ErrorState.SetError(Resource.Error, Resource.LoadFileError);
                throw;
            }
        }
    }
}