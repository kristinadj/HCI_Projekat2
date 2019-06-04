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
    /// <summary>
    /// Interaction logic for AddSpecieWindow.xaml
    /// </summary>
    public partial class AddSpecieWindow : Window
    {
        public ObservableCollection<SpecieType> SpecieTypes { get; set; }
        public AddSpecieWindow()
        {
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
            SpecieType.SelectedIndex = 0;

            // Initialize placeholders
            ID.Text = "ID";
            ID.GotFocus += RemoveText_ID;
            ID.LostFocus += AddText_ID;

            Name.Text = "Name";
            Name.GotFocus += RemoveText_Name;
            Name.LostFocus += AddText_Name;

            Description.Text = "Description";
            Description.GotFocus += RemoveText_Description;
            Description.LostFocus += AddText_Description;

            TouristIncome.Text = "Income from Tourists";
            TouristIncome.GotFocus += RemoveText_TouristIncome;
            TouristIncome.LostFocus += AddText_TouristIncome;
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
                Description.Text = "Description";
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

            foreach (Specie specie in MainWindow.Species)
            {
                if (specie.ID == ID.Text)
                {
                    MessageBoxResult result = MessageBox.Show("Specie with that ID already exists!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
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
            MainWindow.Species.Add(newSpecie);
            MainWindow.saveSpecies();

            MessageBoxResult success = MessageBox.Show("You have successfully added new specie!", "Endangered Species", MessageBoxButton.OK);
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
    }
}
