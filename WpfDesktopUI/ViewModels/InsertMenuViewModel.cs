using Caliburn.Micro;
using System;
using System.Windows;
using WpfDesktopUI.Library;

namespace WpfDesktopUI.ViewModels
{
    public class InsertMenuViewModel : Window
    {

        public BindableCollection<string> Sets { get; }

        string _selectedSet;
        public string SelectedSet 
        { 
            get => _selectedSet; 
            set 
            { 
                _selectedSet = value;
                Console.WriteLine(SelectedSet);
            }

        }

        public InsertMenuViewModel()
        {
            Sets = new BindableCollection<string>(Mapper.GetSetNames());
        }

    }
}