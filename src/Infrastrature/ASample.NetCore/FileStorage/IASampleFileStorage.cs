using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASample.NetCore.FileStorage
{
    public interface  IASampleFileStorage
    {
        ApiRequestResult DeleteFile(string webRootPath, string link);

        Task<ApiRequestResult> SaveFileAsync(string webRootPath, IFormFile file);
        Task<ApiRequestResult> SaveMultiFileAsync(string webRootPath, List<IFormFile> files);
    }
}
