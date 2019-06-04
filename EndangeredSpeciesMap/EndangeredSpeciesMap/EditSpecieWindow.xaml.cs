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
    public partial class EditSpecieWindow : Window
    {
        private Specie specie;
        public ObservableCollection<SpecieType> SpecieTypes { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }
        public EditSpecieWindow(Specie specie)
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
            
            StatusOfEndangerment.SelectedIndex = (int)specie.Endangerment;
            TouristStatus.SelectedIndex = (int)specie.TouristStatus;

            DangerousForPeople.IsChecked = specie.DangerousForPeople;
            OnIUCNList.IsChecked = specie.OnIUCNList;
            InRegionWithPeople.IsChecked = specie.InRegionWithPeople;

            // Initialize placeholders
            ID.Text = specie.ID;
            ID.GotFocus += RemoveText_ID;
            ID.LostFocus += AddText_ID;
            ID.IsEnabled = false;

            Name.Text = specie.Name;
            Name.GotFocus += RemoveText_Name;
            Name.LostFocus += AddText_Name;

            Description.Text = specie.Description;
            Description.GotFocus += RemoveText_Description;
            Description.LostFocus += AddText_Description;

            TouristIncome.Text = specie.TouristIncome.ToString();
            TouristIncome.GotFocus += RemoveText_TouristIncome;
            TouristIncome.LostFocus += AddText_TouristIncome;

            DiscoveryDate.SelectedDate = specie.DiscoveryDate;

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

        /*
         * Cick on 'Add new specie' Button
         */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (SpecieType.SelectedIndex == 0 || TouristStatus.SelectedIndex == 0 || StatusOfEndangerment.SelectedIndex == 0|| ID.Text == "Label" || Name.Text == "Name" || Description.Text == "Description" || TouristIncome.Text == "Income from Tourists")
            {
                MessageBoxResult result = MessageBox.Show("Some fileds are empty!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                float.Parse(TouristIncome.Text);
            }
            catch (FormatException ex)
            {
                MessageBoxResult result = MessageBox.Show("Income from tourists is not valid number!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DiscoveryDate.DisplayDate.Date >= DateTime.Now.Date)
            {
                MessageBoxResult result = MessageBox.Show("Date of discovery is not valid!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool found = false;
            foreach (Specie specie in MainWindow.Species)
            {
                if (specie.ID == ID.Text)
                {
                    found = true;
                    break;
                }
            }

            if (!found) {
                MessageBoxResult result = MessageBox.Show("Specie with that ID doesn't exists!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // setting icon
            String iconSource = "";
            SpecieType selectedType = ((SpecieType)SpecieType.SelectedItem);

            if (Icon.Source.ToString().Equals("pack://application:,,,/EndangeredSpeciesMap;component/Images/picture.png"))
            {
                foreach (SpecieType type in MainWindow.SpecieTypes)
                {
                    if (type.Label.Equals(selectedType.Label))
                    {
                        iconSource = type.Icon;
                    }
                }
            } else
            {
                iconSource = Icon.Source.ToString();
            }

            Specie newSpecie = new Specie
            {
                ID = ID.Text,
                Name = Name.Text,
                Description = Description.Text,
                Icon = iconSource,
                DangerousForPeople = DangerousForPeople.IsChecked == true,
                OnIUCNList = OnIUCNList.IsChecked == true,
                InRegionWithPeople = InRegionWithPeople.IsChecked == true,
                TouristStatus = (TouristStatus)TouristStatus.SelectedIndex,
                TouristIncome = float.Parse(TouristIncome.Text),
                Endangerment = (StatusOfEndangerment)StatusOfEndangerment.SelectedIndex,
                SpecieType = selectedType.Label,
                DiscoveryDate = DiscoveryDate.DisplayDate.Date
            };
            // Save new 'Type of specie'
            MainWindow.Species.Remove(specie);
            MainWindow.Species.Add(newSpecie);
            MainWindow.saveSpecies();

            MessageBoxResult success = MessageBox.Show("You have successfully changed specie!", "Endangered Species", MessageBoxButton.OK);
        }

        /*
         * Cick on 'Load Icon' Button
         */
        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            // open file dialog   
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // image filters  
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                // display image in picture box  
                Icon.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EditTags editTags = new EditTags(specie);
            editTags.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ViewTagsWindow editTags = new ViewTagsWindow(specie);
            editTags.Show();
        }
    }
}
