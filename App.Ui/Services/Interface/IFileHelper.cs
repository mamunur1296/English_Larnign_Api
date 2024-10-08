namespace App.Ui.Services.Interface
{
    public interface IFileHelper
    {
        Task<bool> FileExists(string path);

    }
}
