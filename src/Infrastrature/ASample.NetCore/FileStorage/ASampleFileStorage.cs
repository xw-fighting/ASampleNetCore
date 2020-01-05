using ASample.NetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASample.NetCore.FileStorage
{
    public class ASampleFileStorage
    {
        private readonly IASampleHttpClient _aSampleHttpClient;
        public readonly IConfiguration _configuration;
        public ASampleFileStorage(IASampleHttpClient aSampleHttpClient, IConfiguration configuration)
        {
            _aSampleHttpClient = aSampleHttpClient;
            _configuration = configuration;
        }
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            //Log.Information($"FileSaverService -> SaveFile. webRootPath: {webRootPath}, file: {JsonConvert.SerializeObject(file)}");

            try
            {
                var fileName = $"{Path.GetExtension(file.FileName)}";
                var url = _configuration.GetSection("fileStorage:imageUrl").Value;
                var savePath = await _aSampleHttpClient.PostAsync(url, file);
                return savePath;
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Saving failed: FileSaverService -> SaveFile");
                throw ex;
            }
        }

        public async Task<ApiRequestResult> SaveMultiFileAsync(string webRootPath, List<IFormFile> files)
        {
            //Log.Information($"FileSaverService -> SaveFile. webRootPath: {webRootPath}, file: {JsonConvert.SerializeObject(file)}");

            //try
            //{
            //    var url = _configuration.GetSection("fileStorage:imageMultiUrl").Value;
            //    var savePath = await _aSampleHttpClient.PostAsync(url, files);
            //}
            //catch (Exception ex)
            //{
            //    //Log.Error(ex, "Saving failed: FileSaverService -> SaveFile");
            //    return ApiRequestResult.Error(ex.Message);
            //}
            return null;
        }

        public async Task<ApiRequestResult> DeleteFile(string filePath)
        {
            //Log.Information($"FileSaverService -> SaveFile. webRootPath: {webRootPath}, fileName: {link}");

            try
            {
                var url = _configuration.GetSection("fileStorage:imageDelUrl").Value;
                var result = await _aSampleHttpClient.DeleteAsync(url,filePath);
                return JsonConvert.DeserializeObject<ApiRequestResult>(result);
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Saving failed: FileSaverService -> SaveFile");
                throw ex;
            }
        }
    }
}
