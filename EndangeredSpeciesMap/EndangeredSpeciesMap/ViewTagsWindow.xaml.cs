using EndangeredSpeciesMap.Model;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace EndangeredSpeciesMap
{

    public partial class ViewTagsWindow : Window
    {
        public ObservableCollection<Tag> Tags { get; set; }
        private Specie Specie;
        public ViewTagsWindow(Specie specie)
        {
            this.Specie = specie;
            InitializeComponent();
            DataContext = this;

            Init();
        }

        public void Init()
        {
            // Initalizing Combobox for selecting 'Type of specie'

            ObservableCollection<Tag> newT = new ObservableCollection<Tag>();
            foreach (Tag t in Specie.Tags) {
                newT.Add(t);
               }

       
            Tags = newT;
        }

        public bool isChecked(String label) {
            return true;
        }
    }
}
