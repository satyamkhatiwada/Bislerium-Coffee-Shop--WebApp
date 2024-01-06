using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bislerium.Data
{
    public class AddInsService
    {
        public static void SaveAllCoffee(List<AddIns> addIns)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appAddInsFilePath = Utils.GetAppAddInsFilePath();
            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(addIns);
            File.WriteAllText(appAddInsFilePath, json);
        }

        public static List<AddIns> AddAddIn(string AddInName, int AddInPrice)
        {
            List<AddIns> addIns = GetAllAddIns();
            addIns.Add(
                new AddIns
                {
                    ID = addIns.Count() + 1,
                    AddInName = AddInName,
                    AddInPrice = AddInPrice,
                });
            SaveAllCoffee(addIns);
            return addIns;

        }

        //get all coffee if the file exist else return empty coffee
        public static List<AddIns> GetAllAddIns()
        {
            string addInsFilePath = Utils.GetAppAddInsFilePath();
            if (!File.Exists(addInsFilePath))
            {
                return new List<AddIns>();
            }

            var json = File.ReadAllText(addInsFilePath);

            return JsonSerializer.Deserialize<List<AddIns>>(json);

        }

        public static List<AddIns> UpdateAddIns(int ID, string newAddInName, int newAddInPrice)
        {
            List<AddIns> addIns = GetAllAddIns();

            AddIns addInToUpdate = addIns.Find(x => x.ID == ID);
            addIns.Remove(addInToUpdate);
            addIns.Add(
                new AddIns
                {
                    ID = ID,
                    AddInName = newAddInName,
                    AddInPrice = newAddInPrice,
                });
            SaveAllCoffee(addIns);
            return addIns;

        }
        public static List<AddIns> DeleteAddIn(int ID)
        {
            List<AddIns> addIns = GetAllAddIns();
            AddIns addInToDelete = addIns.Find(x => x.ID == ID);

            if (addInToDelete == null)
                throw new Exception("Addin is not found");
            addIns.Remove(addInToDelete);
            SaveAllCoffee(addIns);
            return addIns;
        }
    }
}
