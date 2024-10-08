namespace App.Ui.Services.Interface
{
    public interface IImageProcessor<T> where T : class
    {
        Task ProcessImageAsync(IFormFile file, T model);

    }
}
