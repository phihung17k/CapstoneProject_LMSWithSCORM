using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace LMS.Infrastructure.Utils
{
    public class ConfigurationUtils
    {
        private static IConfiguration _configuration;
        private static IHttpContextAccessor _accessor;

        public static void Init(IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _configuration = configuration;
            _accessor = accessor;
        }

        #region folder name
        public static string FilesFolder => _configuration["AppSettings:FilesFolder"];
        public static string ImagesFolder => _configuration["AppSettings:ImagesFolder"];
        public static string PDFsFolder => _configuration["AppSettings:PDFsFolder"];
        public static string VideosFolder => _configuration["AppSettings:VideosFolder"];
        public static string SCORMsFolder => _configuration["AppSettings:SCORMsFolder"];
        public static string UploadSCORMFolder => _configuration["AppSettings:UploadSCORMFolder"];
        public static string CourseSCORMFolder => _configuration["AppSettings:CourseSCORMFolder"];
        #endregion

        #region folder path
        public static string LMSPath => Environment.CurrentDirectory.Replace("API", "Core");
        public static string FilesFolderPath => Path.Combine(LMSPath, FilesFolder);
        public static string ImagesFolderPath => Path.Combine(path1: FilesFolderPath, ImagesFolder);
        public static string PDFsFolderPath => Path.Combine(path1: FilesFolderPath, PDFsFolder);
        public static string VideosFolderPath => Path.Combine(path1: FilesFolderPath, VideosFolder);
        public static string SCORMsFolderPath => Path.Combine(path1: FilesFolderPath, SCORMsFolder);
        public static string UploadSCORMsFolderPath => Path.Combine(SCORMsFolderPath, UploadSCORMFolder);
        public static string CourseSCORMsFolderPath => Path.Combine(SCORMsFolderPath, CourseSCORMFolder);
        #endregion

        public static string HttpScheme => _accessor.HttpContext.Request.Scheme; // http or https
        public static string HostValue => _accessor.HttpContext.Request.Host.Value; // localhost:port
        public static string SiteURL => $"{HttpScheme}://{HostValue}";

        public static string FTPHost => _configuration["FTPServer:Host"];
        public static string FTPPort => _configuration["FTPServer:Port"];
        public static string FTPUser => _configuration["FTPServer:FEAccount:User"];
        public static string FTPPassword => _configuration["FTPServer:FEAccount:Password"];
        public static string FTPUri => $"ftp://{FTPHost}:{FTPPort}";
        public static string FTPFEDomain => _configuration["FTPServer:FEAccount:Domain"];
    }
}
