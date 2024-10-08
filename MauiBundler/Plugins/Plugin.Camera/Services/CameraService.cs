using Microsoft.JSInterop;
using Plugin.Camera.Models;

namespace Plugin.Camera.Services;

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
            FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo != null)
                return new(photo.FileName, photo.FullPath,  await ToDataUrl(photo));
        }

        return new(null!, null!, null!);
    }


    [JSInvokable("pickPhoto")]
    public async Task<Picture> PickPhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult? photo = await MediaPicker.Default.PickPhotoAsync();
            if (photo != null)
                return new(photo.FileName, photo.FullPath,  await ToDataUrl(photo));
        }

        return new(null!, null!, null!);
    }

    private static async Task<string> ToDataUrl(FileResult? photo)
    {
        if (photo != null)
        {
            await using Stream sourceStream = await photo.OpenReadAsync();
            byte[] buffer = new byte[sourceStream.Length];
            sourceStream.Position = 0;
            var _ = await sourceStream.ReadAsync(buffer, 0, buffer.Length);

            var result = $"data:image/{photo.ContentType};base64,{Convert.ToBase64String(buffer)}";
            return result;
        }

        return null!;
    }
}