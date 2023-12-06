using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCAN
{
    class Can
    {
        public int Id { get; set; }
        public string Altitude { get; set; }
        public string Langitude { get; set; }
        public string Latitude { get; set; }

        private string _ultrason;
        public string Ultrason
        {
            get { return _ultrason; }
            set
            {
                if (_ultrason != value)
                {
                    _ultrason = value;
                    OnPropertyChanged(nameof(_ultrason));
                }
            }
        }
        private double _progress;
        public double Progress
        {
            get { return _progress; }
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged(nameof(_progress));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
