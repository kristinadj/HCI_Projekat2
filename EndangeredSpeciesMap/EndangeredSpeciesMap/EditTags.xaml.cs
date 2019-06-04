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

    public partial class EditTags : Window
    {
        public ObservableCollection<Tag> Tags { get; set; }
        private Specie Specie;
        public EditTags(Specie specie)
        {
            this.Specie = specie;
            InitializeComponent();
            DataContext = this;

            Init();
        }

        public void Init()
        {
            // Initalizing Combobox for selecting 'Type of specie'
            Tags = MainWindow.Tags;

            
        }

        public bool isChecked(String label) {
            return true;
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Species.Remove(Specie);
            Specie newSpecie = Specie;
            newSpecie.Tags.Clear();

            foreach (Tag tag in TagList.SelectedItems)
            {
                newSpecie.Tags.Add(tag);
            }
            
            MainWindow.Species.Add(newSpecie);
            MainWindow.saveSpecies();

            MessageBoxResult success = MessageBox.Show("You have successfully added tags!", "Endangered Species", MessageBoxButton.OK);

        }
    }
}
