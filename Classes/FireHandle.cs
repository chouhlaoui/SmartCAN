using Firebase.Database;
using Firebase.Database.Query;
using Plugin.LocalNotification;
using SmartCAN.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public ObservableCollection<Notification> notificationCollection = new ObservableCollection<Notification>();

        public FireHandle() { 
            FirebaseClient = new FirebaseClient("https://projet-iot-2ing2-default-rtdb.europe-west1.firebasedatabase.app/");
        }
        public async Task Subscribe()
        {
            notificationCollection.Clear();
            int i = 1;
            NombrePoubelle = await FirebaseClient.Child("carte/Nombre_Poubelle").OnceSingleAsync<int>();

            while (i <= NombrePoubelle)
            {
                try
                {
                    SubscribeToPoubelle("Poubelle" + i.ToString());
                    i++;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"An error occurred during SubscribeToPoubelle: {ex.Message}");
                }
            }
        }

        private void SubscribeToPoubelle(string poubelleName)
        {
            var observable = FirebaseClient
                .Child($"carte/{poubelleName}")
                .AsObservable<string>()
                .Subscribe(d =>
                {
                    if (d != null)
                    {
                        

                        if (d.Key == "Ultrason")
                        {
                            if (double.Parse(d.Object) > 3000)
                            {
                                notificationCollection.Add(new Notification
                                {
                                    title = $"Changement d'état détecté pour {poubelleName}",
                                    message = $"{d.Key} a detecté une saturation pour cette poubelle.\nVeuillez depecher !",
                                    Created = DateTime.Now.ToString("yyyy-MM-dd \n HH:mm:ss"),
                                });
                                SendNotification($"Changement d'état détecté pour {poubelleName}", $"{d.Key} a detecté une saturation pour cette poubelle.\nVeuillez depecher !");
                            }
                        }
                        else
                        {
                            notificationCollection.Add(new Notification
                            {
                                title = $"Changement d'état détecté pour {poubelleName}",
                                message = $"{d.Key} a detecté un changement d'mplacement pour cette poubelle.\nVeuillez verifier !",
                                Created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            });
                            SendNotification($"Changement d'état détecté pour {poubelleName}", $"{d.Key} a detecté un changement d'mplacement pour cette poubelle.\nVeuillez verifier !");
                        }

                    }
                });
        }


        public void SendNotification(string title, string message)
        {
            var notification = new NotificationRequest
            {
                Title = title,
                Description = message,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now,
                }
            };

            LocalNotificationCenter.Current.Show(notification);
        }

        public async void AdminDownload()
        {
            Admin = await FirebaseClient.Child("Users/admin").OnceSingleAsync<User>();
        }

        public async void DownloadAllClients()
        {
            Users.Clear();
            string path = "Users/Normal/";
            NombreUser = await FirebaseClient.Child("Users/Nombre_Users").OnceSingleAsync<int>();
            int i = 1;
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

            NombrePoubelle = await FirebaseClient.Child("carte/Nombre_Poubelle").OnceSingleAsync<int>();

            for (int i = 1; i <= NombrePoubelle; i++)
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
            catch (Firebase.Database.FirebaseException)
            {
                return false;
            }
        }
        public async Task<bool> DeleteUser(User user)
        {
            bool Ok = await DeleteUserFirebase(user.Id);
            if(Ok)
            {
                Users.Remove(user);
                return true;
            }
            return false;
        }
        public async Task<bool> AddNewClient(User u)
        {
            bool result = await Task.Run(() =>
            {
                FirebaseClient.Child($"Users/Normal/{u.Id}").PutAsync(u);
                return true;
            });

            return result;
        }
        public async Task<bool> ModifyClient(int id, User u)
        {
            bool result = await Task.Run(() =>
            {
                DeleteUserFirebase(id);
                Users.Remove(u);
                Users.Add(u);
                FirebaseClient.Child($"Users/Normal/{id}").PutAsync(u);
                return true;
            });

            return result;
        }
        public async Task<bool> DeleteCanFirebase(int id)
        {
            try
            {
                await FirebaseClient
                    .Child("carte")  
                    .Child("Poubelle" + id.ToString())
                    .DeleteAsync();
                return (true);
            }
            catch (Firebase.Database.FirebaseException)
            {
                return false;
            }
        }
        public async Task<bool> DeleteCan(Can poubelle)
        {
            bool Ok = await DeleteCanFirebase(poubelle.Id);
            if (Ok)
            {
                Cans.Remove(poubelle);
                return true;
            }
            return false;
        }
        
    }
}
