using EndangeredSpeciesMap.Model;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EndangeredSpeciesMap
{
    /// <summary>
    /// Interaction logic for EditSpecieWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        private Specie specie;
        public ObservableCollection<SpecieType> SpecieTypes { get; set; }
        public DetailsWindow(Specie specie)
        {
            this.specie = specie;
            InitializeComponent();
            DataContext = this;

            Init();
        }

        public void Init()
        {
            // Initalizing Combobox for selecting 'Type of specie'
            SpecieTypes = MainWindow.SpecieTypes;

            SpecieTypes.Insert(0, new SpecieType
            {
                Label = "Select type of specie"
            });
            
            foreach (SpecieType type in SpecieTypes) {
                if (type.Label == specie.SpecieType) {
                    SpecieType.SelectedItem = type;
                    break;
                }
            }

            SpecieType.IsEnabled = false;

            StatusOfEndangerment.SelectedIndex = (int)specie.Endangerment;
            StatusOfEndangerment.IsEnabled = false;

            TouristStatus.SelectedIndex = (int)specie.TouristStatus;
            TouristStatus.IsEnabled = false;

            DangerousForPeople.IsChecked = specie.DangerousForPeople;
            DangerousForPeople.IsEnabled = false;

            OnIUCNList.IsChecked = specie.OnIUCNList;
            OnIUCNList.IsEnabled = false;

            InRegionWithPeople.IsChecked = specie.InRegionWithPeople;
            InRegionWithPeople.IsEnabled = false;

            // Initialize placeholders
            ID.Text = specie.ID;
            ID.GotFocus += RemoveText_ID;
            ID.LostFocus += AddText_ID;
            ID.IsEnabled = false;

            Name.Text = specie.Name;
            Name.GotFocus += RemoveText_Name;
            Name.LostFocus += AddText_Name;
            Name.IsEnabled = false;

            Description.Text = specie.Description;
            Description.GotFocus += RemoveText_Description;
            Description.LostFocus += AddText_Description;
            Description.IsEnabled = false;

            TouristIncome.Text = specie.TouristIncome.ToString();
            TouristIncome.GotFocus += RemoveText_TouristIncome;
            TouristIncome.LostFocus += AddText_TouristIncome;
            TouristIncome.IsEnabled = false;

            DiscoveryDate.SelectedDate = specie.DiscoveryDate;

            DiscoveryDate.IsEnabled = false;

            Icon.Source = new BitmapImage(new Uri(specie.Icon));
        }

        /* Adding and removing placeholders */
        private void AddText_TouristIncome(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TouristIncome.Text))
                TouristIncome.Text = "Income from Tourists";
        }

        private void RemoveText_TouristIncome(object sender, RoutedEventArgs e)
        {
            TouristIncome.Text = "";
        }

        private void AddText_Description(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Description.Text))
                Description.Text = specie.Description;
        }

        private void RemoveText_Description(object sender, RoutedEventArgs e)
        {
            Description.Text = "";
        }

        private void AddText_Name(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text))
                Name.Text = "Name";
        }

        private void RemoveText_Name(object sender, RoutedEventArgs e)
        {
            Name.Text = "";
        }

        private void AddText_ID(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ID.Text))
                ID.Text = "ID";
        }

        private void RemoveText_ID(object sender, RoutedEventArgs e)
        {
            ID.Text = "";
        }

        /*
         * Cick on 'Go back' Button
         */
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*
         * On Closing
         */
        void Window_Closing(object sender, CancelEventArgs e)
        {
            SpecieTypes.RemoveAt(0); // removing label 'Select type of specie' from collection
        }

    }
}
