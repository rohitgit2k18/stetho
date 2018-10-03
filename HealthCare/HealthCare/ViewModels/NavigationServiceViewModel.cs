using HealthCare.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModels
{
   public class NavigationServiceViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<NavigationViewModel> _navigationMenuList;
        public ObservableCollection<NavigationViewModel> NavigationMenuList
        {
            get
            { return _navigationMenuList; }
            set
            {
                _navigationMenuList = value;
                RaisePropertyChanged();
            }
        }
        public NavigationServiceViewModel()
        {
            LoadDate();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void LoadDate()
        {
            NavigationMenuList = new ObservableCollection<NavigationViewModel>();
            NavigationMenuList.Add(new NavigationViewModel { Id = 1, Icon = ("menu_icon3.png"), Name = "Home" });
            NavigationMenuList.Add(new NavigationViewModel { Id = 2, Icon = ("menu_icon1.png"), Name = "Prescriptions" });
            NavigationMenuList.Add(new NavigationViewModel { Id = 3, Icon = ("menu_icon2.png"), Name = "Appointments" });
            NavigationMenuList.Add(new NavigationViewModel { Id = 4, Icon = ("logout.png"), Name = "Sign Out" });
          
        }
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler == null) return;
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
