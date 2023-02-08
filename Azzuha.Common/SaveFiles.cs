using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Azzuha.DataViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Common
{
    public class SaveFiles
    {
        public FileUrlResponce SaveImage(string rootPath, IFormFile file, string savefolder, string savefolderThumbnail)
        {
            try
            {
                FileUrlResponce image = new FileUrlResponce();

                if (file.Length > 0)
                {
                    var folderPath = "/uploads/" + savefolder;
                    var folderPathThumnail = "\\uploads\\" + savefolderThumbnail + "\\Thumbnail";

                    var filePath = rootPath + folderPath;
                    Directory.CreateDirectory(filePath);
                    Directory.CreateDirectory(rootPath + folderPathThumnail);
                    var FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                    string thisThumbnailFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N") + "_Thumbnail" + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(filePath, FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    image.URL = Path.Combine(folderPath, FileName).Replace(@"\", "/");
                    string thumbnailUrl = GenerateThumbImage(Path.Combine(filePath, FileName), Path.Combine(rootPath + folderPathThumnail, thisThumbnailFileName), 160, 250);
                    image.ThumbUrl = Path.Combine(folderPathThumnail, thisThumbnailFileName).Replace("\\", "/");
                }

                return image;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FileUrlResponce> SaveImages(string rootPath, IEnumerable<IFormFile> files)
        {
            try
           {
                List<FileUrlResponce> images = new List<FileUrlResponce>();
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var folderPath = "uploads/" + DateTime.UtcNow.ToString("yyyy") + "/" + DateTime.UtcNow.ToString("MMMM") + "/" + DateTime.UtcNow.ToString("dd");
                        var folderPathThumnail = "\\uploads\\" + DateTime.UtcNow.ToString("yyyy") + "\\" + DateTime.UtcNow.ToString("MMMM") + "\\" + DateTime.UtcNow.ToString("dd");
                        string path3 = Path.Combine(rootPath, "Uploads");
                        FileUrlResponce url = new FileUrlResponce();
                        var filePath = Path.Combine(rootPath, folderPath);
                        var path = Directory.CreateDirectory(filePath);
                        var FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                        string thisThumbnailFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N") + "_Thumbnail" + Path.GetExtension(file.FileName);
                        using (var fileStream = new FileStream(Path.Combine(filePath, FileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        url.URL = Path.Combine(folderPath, FileName).Replace(@"\", "/");
                        string thumbnailUrl = GenerateThumbImage(Path.Combine(filePath, FileName), Path.Combine(rootPath + folderPathThumnail, thisThumbnailFileName), 160, 250);
                        url.ThumbUrl = Path.Combine(folderPathThumnail, thisThumbnailFileName).Replace("\\", "/");

                        images.Add(url);
                    }
                }

                return images;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FileUrlResponce SaveFile(string rootPath, IFormFile file, string folder)
        {
            try
            {
                FileUrlResponce image = new FileUrlResponce();

                if (file.Length > 0)
                {
                    var folderPath = "/uploads/" + folder + "/" + DateTime.UtcNow.ToString("yyyy") + "/" + DateTime.UtcNow.ToString("MMMM") + "/" + DateTime.UtcNow.ToString("dd");
                    var filePath = rootPath + folderPath;
                    var path = Directory.CreateDirectory(filePath);
                    var FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(filePath, FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    image.URL = Path.Combine(folderPath, FileName).Replace(@"\", "/");
                    image.ThumbUrl = "";
                }
                return image;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GenerateThumbImage(string originalImagePath, string thumbImagePath, int newWidth, int newHeight)
        {
            try
            {

                using (Bitmap srcBmp = new Bitmap(originalImagePath))
                {
                    float ratio = 1;
                    float minSize = Math.Min(newWidth, newHeight);

                    if (srcBmp.PropertyIdList.Contains(0x112))
                    {
                        var prop = srcBmp.GetPropertyItem(0x112);
                        if (prop.Type == 3 && prop.Len == 2)
                        {
                            UInt16 orientationExif = BitConverter.ToUInt16(srcBmp.GetPropertyItem(0x112).Value, 0);
                            if (orientationExif == 8)
                            {
                                srcBmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            }
                            else if (orientationExif == 3)
                            {
                                srcBmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            }
                            else if (orientationExif == 6)
                            {
                                srcBmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            }
                        }
                    }

                    if (srcBmp.Width > srcBmp.Height)
                    {
                        ratio = minSize / (float)srcBmp.Width;
                    }
                    else
                    {
                        ratio = minSize / (float)srcBmp.Height;
                    }

                    SizeF newSize = new SizeF(srcBmp.Width * ratio, srcBmp.Height * ratio);
                    Bitmap target = new Bitmap((int)newSize.Width, (int)newSize.Height);

                    using (Graphics graphics = Graphics.FromImage(target))
                    {
                        graphics.CompositingQuality = CompositingQuality.HighSpeed;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.CompositingMode = CompositingMode.SourceCopy;
                        graphics.DrawImage(srcBmp, 0, 0, newSize.Width, newSize.Height);
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            target.Save(thumbImagePath);
                        }
                    }
                }
                return thumbImagePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<FileUrlResponce> SendMyFileToS3(IFormFile file, string bucketName, string subDirectoryInBucket, bool isVedio, string accessKey, string accessSecret, string baseUrl)
        {
            try
            {
                FileUrlResponce reponse = new FileUrlResponce();
                var FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                using (var source = file.OpenReadStream())
                {
                    source.Position = 0; Stream thumbStream = new MemoryStream(); source.CopyTo(thumbStream); var client = new AmazonS3Client(accessKey, accessSecret, RegionEndpoint.USEast1); PutObjectRequest putRequest = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = subDirectoryInBucket + "/" + FileName,
                        InputStream = source,
                        ContentType = file.ContentType,
                        CannedACL = S3CannedACL.PublicRead
                    }; PutObjectResponse response = await client.PutObjectAsync(putRequest);

                    reponse.URL = baseUrl + putRequest.Key;

                    if (!isVedio)
                    {
                        thumbStream.Seek(0, SeekOrigin.Begin);
                        thumbStream.Position = 0;

                        Bitmap srcBmp = new Bitmap(thumbStream);
                        float ratio = 1;
                        float minSize = Math.Min(256, 256);

                        if (srcBmp.PropertyIdList.Contains(0x112)) //0x112 = Orientation
                        {
                            var prop = srcBmp.GetPropertyItem(0x112);
                            if (prop.Type == 3 && prop.Len == 2)
                            {
                                UInt16 orientationExif = BitConverter.ToUInt16(srcBmp.GetPropertyItem(0x112).Value, 0);
                                if (orientationExif == 8)
                                {
                                    srcBmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                }
                                else if (orientationExif == 3)
                                {
                                    srcBmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                }
                                else if (orientationExif == 6)
                                {
                                    srcBmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                }
                            }
                        }

                        if (srcBmp.Width > srcBmp.Height)
                        {
                            ratio = minSize / (float)srcBmp.Width;
                        }
                        else
                        {
                            ratio = minSize / (float)srcBmp.Height;
                        }

                        SizeF newSize = new SizeF(srcBmp.Width * ratio, srcBmp.Height * ratio);
                        Bitmap target = new Bitmap((int)newSize.Width, (int)newSize.Height);


                        Stream saveableThumbStream = new MemoryStream();

                        using (Graphics graphics = Graphics.FromImage(target))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.DrawImage(srcBmp, 0, 0, newSize.Width, newSize.Height);
                            graphics.Dispose();

                            target.Save(saveableThumbStream, ImageFormat.Jpeg);


                        }

                        PutObjectRequest putRequestThumb = new PutObjectRequest
                        {
                            BucketName = bucketName,
                            Key = subDirectoryInBucket + "/thumb_" + FileName,
                            InputStream = saveableThumbStream,
                            ContentType = file.ContentType,
                            CannedACL = S3CannedACL.PublicRead
                        };
                        PutObjectResponse responseThumb = await client.PutObjectAsync(putRequestThumb);

                        reponse.ThumbUrl = baseUrl + putRequestThumb.Key;
                    }
                    else
                    {
                        reponse.ThumbUrl = baseUrl + putRequest.Key;
                    }

                }
                return reponse;
            }
            catch (Exception ex) { throw ex; }
        }

    }
}