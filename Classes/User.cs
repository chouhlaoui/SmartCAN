using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCAN
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nom { get; set; }
        public int Tel { get; set; }
        public string mdp { get; set; }
        private bool _approved;
        public bool Approved
        {
            get { return _approved; }
            set
            {
                if (_approved != value)
                {
                    _approved = value;
                    OnPropertyChanged(nameof(Approved));
                }
            }
        }
        private bool _notapproved;
        public bool NotApproved
        {
            get { return _notapproved; }
            set
            {
                if (_notapproved != value)
                {
                    _notapproved = value;
                    OnPropertyChanged(nameof(Approved));
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"User ID: {Id}, Email: {Email}, Name: {Nom}, Telephone: {Tel}, Approved: {Approved}, Not Approved: {NotApproved}";
        }
    }

}
