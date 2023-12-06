using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCAN
{

    class FireHandle
    {
        public ObservableCollection<Can> Cans { get; set; } = new ObservableCollection<Can>();
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public FirebaseClient FirebaseClient { get; set; }
        public int NombreUser;
        public int NombrePoubelle;
        public User Admin { get; set; }
        public FireHandle() { 
            FirebaseClient = new FirebaseClient("https://projet-iot-2ing2-default-rtdb.europe-west1.firebasedatabase.app/");
            
        }

        public async void AdminDownload()
        {
            Admin = await FirebaseClient.Child("Users/admin").OnceSingleAsync<User>();
        }

        public async void DownloadAllClients()
        {
            Users.Clear();
            string path = "Users/Normal/";
            int i = 1;
            NombreUser = await FirebaseClient.Child("Users/Nombre_Users").OnceSingleAsync<int>();
            var normalUser = await FirebaseClient.Child(path + i.ToString()).OnceSingleAsync<User>();
            while (i<=NombreUser)
            {
                if(normalUser != null)
                {
                    normalUser.Id = i;
                    normalUser.NotApproved = !normalUser.Approved;
                    Users.Add(normalUser);
                }
                i++;
                normalUser = await FirebaseClient.Child(path + i.ToString()).OnceSingleAsync<User>();
            } 
        }
        public async void DownloadAllCans()
        {
            Cans.Clear();
            string path = "carte/Poubelle";

            int Nombre_Poubelle = await FirebaseClient.Child("carte/Nombre_Poubelle").OnceSingleAsync<int>();

            for (int i = 1; i <= Nombre_Poubelle; i++)
            {

                var Poubelle = await FirebaseClient.Child(path + i.ToString()).OnceSingleAsync<Can>();
                if (Poubelle != null)
                {
                    Poubelle.Id = i;
                    Poubelle.Progress = Double.Parse(Poubelle.Ultrason) / 3000;
                    Cans.Add(Poubelle);
                }
            }

        }
        public async Task<bool> DeleteUserFirebase(int id)
        {
            string path = "Users/Normal/";
            try
            {
                await FirebaseClient.Child(path + id.ToString()).DeleteAsync();
                return (true);
            }
            catch (Firebase.Database.FirebaseException ex)
            {
                return false;
            }
        }
        public bool DeleteUser(int id)
        {
            bool Ok = (bool)DeleteUserFirebase(id).Result;
            if(Ok)
            {
                var U = Users.FirstOrDefault(u => u.Id == id);
                Users.Remove(U);
                return (Ok);
            }
            return false;
        }

        public async void addClient(int id,User u)
        {
            if (Users.IndexOf(u) > 0)
            {
                DeleteUser(id);
            }
            Users.Add(u);
            await FirebaseClient.Child($"Users/Normal/{id}").PutAsync(u);
        } 

    }
}
