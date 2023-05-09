using System.Security.Cryptography;
using System.Text;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace DomShtor.Service;

public class WebFile
{

    public string GetFileName(string fileName)
    {
        var dir = GetWebFileFolder(fileName);
        CreateFolder(dir);
        return dir + "/" + Path.GetFileNameWithoutExtension(fileName) + ".jpg";
    }

    private string GetWebFileFolder(string fileName)
    {
        var md5Hash = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(fileName);
        var hashBytes = md5Hash.ComputeHash(inputBytes);

        string hash = Convert.ToHexString(hashBytes);
        
        return "./wwwroot/images/" + hash.Substring(0, 2) + "/" +
                  hash.Substring(0, 4);
    }

    private void CreateFolder(string dir)
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    public async Task UploadAndResizeImage(Stream fileStream, string fileName, int newWidth, int newHeight)
    {
        using (Image image = await Image.LoadAsync(fileStream))
        {
            int aspectWidth = newWidth;
            int aspectHeight = newHeight;

            if (image.Width / (image.Height / newHeight) > newWidth)
                aspectHeight = (int)(image.Height / (image.Width / (float)newHeight));
            else
                aspectWidth = image.Width / (image.Height / newHeight);

            image.Mutate(x=>x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));
            
            await image.SaveAsync(fileName, new JpegEncoder() {Quality = 75});
        }
    }
}