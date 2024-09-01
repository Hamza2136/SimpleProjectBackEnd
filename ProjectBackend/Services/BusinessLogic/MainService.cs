using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectBackend.Models;
using ProjectBackend.Services.Contract;

namespace ProjectBackend.Services.BusinessLogic
{
    public class MainService : IMainService
    {
        private readonly string filePath = @"D:/Coding Practical/Angular/ProjectBackend/ProjectBackend/Data1.txt";
        private readonly string baseFilePath = @"D:/Coding Practical/Angular/ProjectBackend/ProjectBackend/Uploads";

        public List<MainModel> GetData()
        {
            if (!File.Exists(filePath))
            {
                return new List<MainModel>();
            }
            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<MainModel>>(jsonData) ?? new List<MainModel>();
        }

        public MainModel GetDataBySerialNumber(int serialNumber)
        {
            return GetData().FirstOrDefault(d => d.SerialNumber == serialNumber);
        }

        public void SaveData(MainModel model)
        {
            var data = GetData();
            data.Add(model);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(data));
        }

        public void DeleteData(int serialNumber)
        {
            var data = GetData();
            var dataToDelete = data.FirstOrDefault(d => d.SerialNumber == serialNumber);
            if (dataToDelete != null)
            {
                data.Remove(dataToDelete);
                File.WriteAllText(filePath, JsonConvert.SerializeObject(data));
            }
        }

        // Extra function Use if needed!
        public void UpdateData(MainModel model)
        {
            var data = GetData();
            var existingData = data.FirstOrDefault(d => d.SerialNumber == model.SerialNumber);
            if (existingData != null)
            {
                existingData.Name = model.Name;
                existingData.Email = model.Email;
                existingData.Password = model.Password;
                File.WriteAllText(filePath, JsonConvert.SerializeObject(data));
            }
        }

        // Zip file upload code

        public async Task<bool> UploadFilesAsync(IFormFileCollection files, string uploadLink)
        {
            if (files == null || files.Count == 0)
            {
                return false;
            }

            if (string.IsNullOrEmpty(uploadLink))
            {
                return false;
            }
            string uploadPath = Path.Combine(baseFilePath, uploadLink);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(uploadPath, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            return true;
        }

        public MainModel Login(string emailId, string password)
        {
            var data = GetData();
            return data.FirstOrDefault(x => x.Email == emailId && x.Password == password);
        }

    }

}
