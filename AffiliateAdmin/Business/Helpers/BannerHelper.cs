using System.Drawing;
using System.IO;
using System.Web;

namespace AffiliateAdmin.Business.Helpers
{
    public class BannerHelper
    {
        public static bool IsValidExtension(string fileName, ref string ext)
        {
            string[] arr = fileName.Split('\\');
            string[] arr1 = arr[arr.Length - 1].Split('.');
            ext = arr1[arr1.Length - 1].ToLower();
            return ext.Equals("gif") == true || ext.Equals("jpg") == true || ext.Equals("png") == true || ext.Equals("jpeg") == true || ext.Equals("swf") == true;
        }

        public static void UploadFileWithAutoDetectSize(HttpContext context, string newFolder, string path, string fileName, ref int errcode, out string size, ref string strDst)
        {
            size = string.Empty;
            errcode = 0;
            string file = "";
            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                {
                    file = context.Request.Files[i].FileName;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(file))
            {
                if (Directory.Exists(path + newFolder) == false)
                {
                    Directory.CreateDirectory(path + newFolder);
                }
                //string strDst;
                //strSrc = context.Request.Files[0].FileName;
                string Extension = Path.GetExtension(file);
                if (Extension.ToLower().Equals(".gif") == true || Extension.ToLower().Equals(".jpg") == true || Extension.ToLower().Equals(".png") == true || Extension.ToLower().Equals(".jpeg") == true || Extension.ToLower().Equals(".swf") == true)
                {
                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                        {
                            if (context.Request.Files[i].ContentLength <= AppSettings.ImageSize)
                            {
                                strDst = path + newFolder + "\\" + fileName;
                                context.Request.Files[i].SaveAs(strDst);
                                if (!Extension.ToLower().Equals(".swf"))
                                {
                                    var bitmap = new Bitmap(context.Request.Files[0].InputStream);
                                    size = string.Format("{0}x{1}", bitmap.Width, bitmap.Height);
                                }
                                else
                                {
                                    SwfInfo swfInfo = new SwfInfo(strDst);
                                    size = string.Format("{0}x{1}", swfInfo.Width, swfInfo.Height);
                                }
                                break;
                            }
                            else
                            {
                                errcode = 3;
                            }
                        }
                    }
                }
                else
                {
                    // Extension fail
                    errcode = 2;
                }
            }
            else
            {
                // file is null
                errcode = 1;
            }
        }

        public static void DeleteFile(string path)
        {
            try
            {
                var file = new FileInfo(path);
                if (file.Exists)
                    file.Delete();
            }
            catch
            {
            }
        }
    }
}