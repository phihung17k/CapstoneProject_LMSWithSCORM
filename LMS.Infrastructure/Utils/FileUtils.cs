using FluentFTP;
using LMS.Core.Enum;
using LMS.Infrastructure.Exceptions;
using LMS.Core.Models.ViewModels;
using MediaInfo;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace LMS.Infrastructure.Utils
{
    public class FileUtils
    {
        //unit Bytes
        public const long maxFileSize = 134217728;  // = 128MB
        public const long maxImageFileSize = 5242880; // = 5MB

        private static string getUniqueFileName(string path)
        {
            if (!File.Exists(path))
            {
                return "";
            }
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
            string pathWithoutFileName = Path.GetDirectoryName(path);
            string extension = Path.GetExtension(path);
            for (int i = 1; i < 1000; i++)
            {
                string newName = $"{fileNameWithoutExtension}({i}){extension}";
                if (!File.Exists($"{pathWithoutFileName}/{newName}"))
                {
                    return newName;
                }
            }
            return "";
        }

        private static void ValidateFile(IFormFile file, bool isImageFile = false, bool isZipFile = false)
        {
            if (file == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.InvalidFile, ErrorMessages.FileIsInvalid);
            }
            if (isImageFile)
            {
                if (!file.ContentType.Contains(FileType.image.ToString()))
                {
                    throw new RequestException(HttpStatusCode.UnsupportedMediaType, ErrorCodes.UnsupportedFile,
                        ErrorMessages.UnsupportedFile);
                }
            }
            else if (isZipFile)
            {
                string zip = FileType.zip.ToString();
                if (!file.ContentType.Contains(zip))
                {
                    throw new RequestException(HttpStatusCode.UnsupportedMediaType, ErrorCodes.UnsupportedFile,
                        ErrorMessages.UnsupportedFile);
                }
            }
            else
            {
                string video = FileType.video.ToString();
                string pdf = FileType.pdf.ToString();
                if (!file.ContentType.Contains(video) && !file.ContentType.Contains(pdf))
                {
                    throw new RequestException(HttpStatusCode.UnsupportedMediaType, ErrorCodes.UnsupportedFile,
                        ErrorMessages.UnsupportedFile);
                }
            }
            if (file.Length == 0)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.InvalidFile, ErrorMessages.FileIsInvalid);
            }
            if (file.Length >= maxFileSize || (isImageFile && file.Length >= maxImageFileSize))
            {
                throw new RequestException(HttpStatusCode.RequestEntityTooLarge, ErrorCodes.FileTooLarge,
                    ErrorMessages.FileTooLarge);
            }
            string fileName = file.FileName;
            int dotIndex = fileName.LastIndexOf(".");
            string fileNameWithoutExtension = fileName.Substring(0, dotIndex);
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                if (fileNameWithoutExtension.Contains(c))
                {
                    throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.UnsupportedFile,
                        ErrorMessages.UnsupportedFile);
                }
            }
            string extension = fileName.Substring(dotIndex);
            if (extension.Contains("avi"))
            {
                throw new RequestException(HttpStatusCode.UnsupportedMediaType, ErrorCodes.UnsupportedFile,
                        ErrorMessages.UnsupportedFile);
            }
        }

        private static FileType GetFileType(string contentType)
        {
            foreach (FileType type in (FileType[])Enum.GetValues(typeof(FileType)))
            {
                if (contentType.Contains(type.ToString()))
                {
                    return type;
                }
            }
            return 0;
        }

        private static string CreateFolderIfNotExist(FileType fileType)
        {
            string folderPath = ConfigurationUtils.SCORMsFolderPath;
            if (fileType == FileType.image)
            {
                folderPath = ConfigurationUtils.ImagesFolderPath;
            }
            if (fileType == FileType.pdf)
            {
                folderPath = ConfigurationUtils.PDFsFolderPath;
            }
            else if (fileType == FileType.video)
            {
                folderPath = ConfigurationUtils.VideosFolderPath;
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }

        public static string GetUrlForSCORM(string pathToFolder, string fileName)
        {
            //string siteUrl = ConfigurationUtils.SiteURL;
            string resourcesFolder = ConfigurationUtils.CourseSCORMFolder.ToLower();
            pathToFolder = pathToFolder.ToLower().Replace(@"\", @"/");
            int i = pathToFolder.IndexOf(resourcesFolder);
            if (i >= 0)
            {
                pathToFolder = pathToFolder.Substring(i, pathToFolder.Length - i);
            }
            string url = $"{pathToFolder}{fileName}";
            return url;
        }

        public static string GetUrl(string pathToFolder, string fileName)
        {
            string siteUrl = ConfigurationUtils.SiteURL;
            string resourcesFolder = ConfigurationUtils.FilesFolder.ToLower();
            pathToFolder = pathToFolder.ToLower().Replace(@"\", @"/");
            int i = pathToFolder.IndexOf(resourcesFolder);
            if (i >= 0)
            {
                pathToFolder = pathToFolder.Substring(i, pathToFolder.Length - i);
            }
            string url = $"{siteUrl}/{pathToFolder}{fileName}";
            return url;
        }

        public static string GetFolderUrl(string pathToFolder)
        {
            string domain = ConfigurationUtils.FTPFEDomain;
            string resourcesFolder = ConfigurationUtils.CourseSCORMFolder.ToLower();
            pathToFolder = pathToFolder.ToLower().Replace(@"\", @"/");
            int i = pathToFolder.IndexOf(resourcesFolder);
            if (i >= 0)
            {
                pathToFolder = pathToFolder.Substring(i, pathToFolder.Length - i);
            }
            string url = $"{domain}/{pathToFolder}";
            return url;
        }

        public static async Task<ResourceInfoModel> SaveFileToFolder(IFormFile resource, bool isImageFile = false,
            bool isZipFile = false)
        {
            ValidateFile(resource, isImageFile: isImageFile, isZipFile: isZipFile);
            FileType fileType = GetFileType(resource.ContentType);
            string folderPath = CreateFolderIfNotExist(fileType);

            string fileName = resource.FileName;
            //trim fileName
            int dotIndex = fileName.LastIndexOf(".");
            string fileNameWithoutExtension = fileName.Substring(0, dotIndex);
            fileNameWithoutExtension = fileNameWithoutExtension.Trim();
            fileName = $"{fileNameWithoutExtension}{fileName.Substring(dotIndex)}";

            string pathToFile = Path.Combine(folderPath, fileName);

            string newFilename = getUniqueFileName(pathToFile);
            if (!string.IsNullOrWhiteSpace(newFilename))
            {
                fileName = newFilename;
                pathToFile = Path.Combine(folderPath, newFilename);
            }

            using (FileStream fileStream = new FileStream(pathToFile, FileMode.Create))
            {
                await resource.CopyToAsync(fileStream);
                string url = GetUrl(folderPath, fileName);
                TimeSpan? duration = null;
                if (fileType == FileType.video)
                {
                    var media = new MediaInfoWrapper(pathToFile);
                    if (media.MediaInfoNotloaded)
                    {
                        throw new RequestException(HttpStatusCode.UnsupportedMediaType, ErrorCodes.UnsupportedFile,
                            ErrorMessages.UnsupportedFile);
                    }
                    int durationMilisecond = media.Duration;
                    duration = TimeSpan.FromMilliseconds(durationMilisecond);
                }

                return new ResourceInfoModel
                {
                    FileName = fileName,
                    FileType = fileType,
                    Url = url,
                    Duration = duration,
                    pathToFile = pathToFile
                };
            }
        }

        public static string ExtractZipFile(string pathToFile)
        {
            string courseSCORMsFolderPath = ConfigurationUtils.CourseSCORMsFolderPath;
            if (!Directory.Exists(courseSCORMsFolderPath))
            {
                Directory.CreateDirectory(courseSCORMsFolderPath);
            }

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathToFile);
            string pathToCourseSCORMFolder = Path.Combine(courseSCORMsFolderPath, fileNameWithoutExtension);
            if (!Directory.Exists(pathToCourseSCORMFolder))
            {
                Directory.CreateDirectory(pathToCourseSCORMFolder);
            }
            ZipFile.ExtractToDirectory(pathToFile, pathToCourseSCORMFolder, true);
            return pathToCourseSCORMFolder;
        }

        public static string FindManifestFile(string pathToCourseSCORMFolder)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(pathToCourseSCORMFolder);
            string fileName = "imsmanifest.xml";
            FileInfo[] files = directoryInfo.GetFiles("*.xml");
            string fullPath = "";
            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    if (file.Name.Equals(fileName))
                    {
                        fullPath = Path.GetFullPath(file.FullName);
                    }
                }
            }
            if (!File.Exists(fullPath))
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.FileManifestNotFound,
                    ErrorMessages.FileManifestNotFound);
            }
            return fullPath;
        }

        public static void DeleteImageFile(string pathToFile)
        {
            DeleteOtherLearningResourceFile(pathToFile);
        }

        public static void DeleteOtherLearningResourceFile(string pathToFile)
        {
            string filesFolder = ConfigurationUtils.FilesFolder.ToLower();
            int filesFolderIndex = pathToFile.IndexOf(filesFolder);
            pathToFile = pathToFile.Substring(filesFolderIndex);
            string absolutePath = Path.Combine(ConfigurationUtils.LMSPath, pathToFile);
            if (File.Exists(absolutePath))
            {
                File.Delete(absolutePath);
            }
            else
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.FileNotFound,
                    ErrorMessages.FileNotFound);
            }
        }

        public static async Task DeleteSCORMFile(string pathToFolder, string titleFromUpload)
        {
            string scormCourseFolder = ConfigurationUtils.CourseSCORMFolder.ToLower();
            int scormCourseFolderIndex = pathToFolder.IndexOf(scormCourseFolder);
            pathToFolder = pathToFolder.Substring(scormCourseFolderIndex);

            //delete folder unzip in ftp fe server
            string host = ConfigurationUtils.FTPHost;
            int port = int.Parse(ConfigurationUtils.FTPPort);
            string user = ConfigurationUtils.FTPUser;
            string password = ConfigurationUtils.FTPPassword;
            FtpClient client = new FtpClient(host, port, user, password);
            client.ValidateAnyCertificate = true;
            await client.AutoConnectAsync();

            if (await client.DirectoryExistsAsync(pathToFolder))
            {
                await client.DeleteDirectoryAsync(pathToFolder);
            }

            //get path in folder on disk
            //  .../scormcourses/the importance of customers and customer service (3)
            int lastSlashIndex = pathToFolder.LastIndexOf("/");
            string folderName = pathToFolder.Substring(lastSlashIndex + 1);
            string scormCourseFolderPath = ConfigurationUtils.CourseSCORMsFolderPath.ToLower();
            string pathToFolderOnDisk = Path.Combine(scormCourseFolderPath, folderName);

            //delete folder on disk
            if (Directory.Exists(pathToFolderOnDisk))
            {
                Directory.Delete(pathToFolderOnDisk, true);
            }
            else
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.FolderNotFound,
                    ErrorMessages.FolderNotFound);
            }

            //delete file zip on disk
            string scormFolderPath = ConfigurationUtils.SCORMsFolderPath;
            string extension = "zip";
            string pathToFile = $"{scormFolderPath}{folderName}.{extension}";
            //string pathToFile = Path.Combine(ConfigurationUtils.SCORMsFolderPath, titleFromUpload);
            if (File.Exists(pathToFile))
            {
                File.Delete(pathToFile);
            }
            else
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.FileNotFound,
                    ErrorMessages.FileManifestNotFound);
            }
        }

        public static async Task UploadUnzipSCORMFolder(string pathToFolder)
        {
            string scormCourseFolder = ConfigurationUtils.CourseSCORMFolder.ToLower();
            int scormCourseFolderIndex = pathToFolder.IndexOf(scormCourseFolder);
            pathToFolder = pathToFolder.Substring(scormCourseFolderIndex);    // scormcourses/dangerous goods traini...
            string absolutePath = Path.Combine(ConfigurationUtils.SCORMsFolderPath, pathToFolder); //E:/..

            int lastSlashIndex = pathToFolder.LastIndexOf("/");
            string currentFolder = pathToFolder.Substring(lastSlashIndex + 1);

            string host = ConfigurationUtils.FTPHost;
            int port = int.Parse(ConfigurationUtils.FTPPort);
            string user = ConfigurationUtils.FTPUser;
            string password = ConfigurationUtils.FTPPassword;
            FtpClient client = new FtpClient(host, port, user, password);
            client.ValidateAnyCertificate = true;
            await client.AutoConnectAsync();

            string scormFolderPath = $"/{ConfigurationUtils.CourseSCORMFolder}";
            string currentFolderPath = $"{scormCourseFolder}/{currentFolder}";
            if ((await client.DirectoryExistsAsync(scormFolderPath)) == false)
            {
                await client.CreateDirectoryAsync(scormFolderPath);
            }
            if ((await client.DirectoryExistsAsync(currentFolderPath)) == false)
            {
                await client.CreateDirectoryAsync(currentFolderPath);
            }

            await client.UploadDirectoryAsync(absolutePath, currentFolderPath, FtpFolderSyncMode.Update);
        }

        private static string SaveImageFileByBytes(byte[] imageBytes, string fileName)
        {
            string folderPath = CreateFolderIfNotExist(FileType.image);
            //trim fileName
            int dotIndex = fileName.LastIndexOf(".");
            string fileNameWithoutExtension = fileName.Substring(0, dotIndex);
            fileNameWithoutExtension = fileNameWithoutExtension.Trim();
            fileName = $"{fileNameWithoutExtension}{fileName.Substring(dotIndex)}";
            string pathToFile = Path.Combine(folderPath, fileName);
            //get filename unique
            string newFilename = getUniqueFileName(pathToFile);
            if (!string.IsNullOrWhiteSpace(newFilename))
            {
                fileName = newFilename;
                pathToFile = Path.Combine(folderPath, newFilename);
            }
            //save file to storage
            File.WriteAllBytes(pathToFile, imageBytes);

            //get url on server: lmsapi.hisoft.vn/...
            string url = GetUrl(folderPath, fileName);
            return url;
        }

        public static string ConvertAndStoreImage(string content, string oldContent = null, bool isUpdate = false)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            var node = doc.DocumentNode;
            //remove unnecessary (XPath: /div/figure/img => /img)
            var imageNodes = node.SelectNodes("/div/figure/img");
            if (imageNodes != null)
            {
                foreach (var img in imageNodes)
                {
                    node.ReplaceChild(img, img.ParentNode.ParentNode);
                }
            }

            if (isUpdate)
            {
                //get the existed img link in db
                var tempDoc = new HtmlDocument();
                tempDoc.LoadHtml(oldContent);
                var tempNodes = tempDoc.DocumentNode.SelectNodes("/img");
                List<string> imageLinksDb = new();
                if (tempNodes != null)
                {
                    foreach (var tempNode in tempNodes)
                    {
                        //all src start with http
                        string src = tempNode.Attributes["src"].Value;
                        imageLinksDb.Add(src);
                    }
                }

                //get the existed img link in request
                var imgNodes = node.SelectNodes("/img");
                List<string> imageLinksRequest = new();
                if (imgNodes != null)
                {
                    foreach (var img in imgNodes)
                    {
                        string src = img.Attributes["src"].Value;
                        //img has link start with http meaning existed
                        if (src.StartsWith("http"))
                        {
                            imageLinksRequest.Add(src);
                        }
                    }
                }

                //remove old image: exist in db and not exist in request
                List<string> exceptImageLinks = imageLinksDb.Except(imageLinksRequest).ToList();
                if (exceptImageLinks.Any())
                {
                    foreach (string removedImage in exceptImageLinks)
                    {
                        DeleteImageFile(removedImage.ToLower());
                    }
                }
            }
            var newImageNodes = node.SelectNodes("/img");
            //List<string> imageLinksRequest = new();
            if (newImageNodes != null)
            {
                foreach (var newImgNode in newImageNodes)
                {
                    string src = newImgNode.Attributes["src"].Value;
                    //2 case src starting
                    //- base64: not existed on server => to create file and store
                    //- http: existed on server
                    if (src.StartsWith("http"))
                    {
                    }
                    else
                    {
                        //base 64 => create new file
                        //get file name: abc.png
                        string fileName = newImgNode.Attributes["data-file-name"].Value;
                        //remove the unnecessary content and get base64 string (image)
                        int base64Index = src.IndexOf("base64");
                        src = src.Substring(base64Index);
                        string base64String = src.Replace("base64,", "");
                        byte[] imageBytes = Convert.FromBase64String(base64String);

                        string url = SaveImageFileByBytes(imageBytes, fileName);

                        //create <img> tag
                        var newNode = HtmlNode.CreateNode("<img src=\"" + url + "\" width=\"300px\" height=\"200px\"/>");

                        //replace old <img> to new <img> (XPath: /img -> /img)
                        node.ReplaceChild(newNode, newImgNode);
                    }
                }
            }

            //parse to html string
            return node.WriteContentTo();
        }
    }
}
