using Plugin.Camera.Models;
using Microsoft.JSInterop;

namespace Plugin.Camera.Serices;

public interface ICameraService
{
    Task<Picture> TakePhoto();
    Task<Picture> PickPhoto();
}

public class CameraService : ICameraService
{
    [JSInvokable("takePhoto")]
    public async Task<Picture> TakePhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
            return new(photo.FileName, photo.FullPath,  await ToDataUrl(photo));
        }

        return new(null!, null!, null!);
    }


    [JSInvokable("pickPhoto")]
    public async Task<Picture> PickPhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();
            return new(photo.FileName, photo.FullPath,  await ToDataUrl(photo));
        }

        return new(null!, null!, null!);
    }

    private static async Task<string> ToDataUrl(FileResult photo)
    {
        if (photo != null)
        {
            using Stream sourceStream = await photo.OpenReadAsync();
            byte[] buffer = new byte[sourceStream.Length];
            sourceStream.Position = 0;
            sourceStream.Read(buffer, 0, buffer.Length);

            var result = $"data:image/{photo.ContentType};base64,{Convert.ToBase64String(buffer)}";
            return result;
        }

         return null!;

    }
}