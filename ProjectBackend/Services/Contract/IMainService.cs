using ProjectBackend.Models;

namespace ProjectBackend.Services.Contract
{
    public interface IMainService
    {
        List<MainModel> GetData();
        MainModel GetDataBySerialNumber(int serialNumber);
        void SaveData(MainModel model);
        void UpdateData(MainModel model);
        void DeleteData(int serialNumber);

        // Zip file upload Code Interface
        Task<bool> UploadFilesAsync(IFormFileCollection files, string uploadLink);
        MainModel Login(string emailId, string password);
    }
}
