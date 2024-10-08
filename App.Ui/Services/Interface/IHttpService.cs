using App.Ui.DTOs;

namespace App.Ui.Services.Interface
{
    public interface IHttpService
    {
        Task<string> SendData(ClientRequest requestData, bool token = true);
    }
}
