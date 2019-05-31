using EndangeredSpeciesMap.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EndangeredSpeciesMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Point startPoint = new System.Windows.Point();

        public Dictionary<string, Specie> species = new Dictionary<string, Specie>();
        public ObservableCollection<SpecieType> SpecieTypes {get; set;}
        public Dictionary<string, Tag> tags = new Dictionary<string, Tag>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Init();
        }

        public void Init()
        {
            // Reading 'Types of species' from json file
            if (File.Exists(@"..\..\Data\specieTypes.json"))
            {
                String json = File.ReadAllText(@"..\..\Data\specieTypes.json");
                SpecieTypes = JsonConvert.DeserializeObject<ObservableCollection<SpecieType>>(json);
            } else
            {
                SpecieTypes = new ObservableCollection<SpecieType>();
            }

            // Reading 'Tags' from json file
            if (File.Exists(@"..\..\Data\tags.json"))
            {
                String json = System.IO.File.ReadAllText(@"..\..\Data\tags.json");
                tags = JsonConvert.DeserializeObject<Dictionary<string, Tag>>(json);
            }

            // Reading 'Species' from json file
            if (File.Exists(@"..\..\Data\species.json"))
            {
                String json = System.IO.File.ReadAllText(@"..\..\Data\species.json");
                species = JsonConvert.DeserializeObject<Dictionary<string, Specie>>(json);
            }

            // Initialize placeholders
            SearchParam.Text = "Search...";
            SearchParam.GotFocus += RemoveText_SearchBox;
            SearchParam.LostFocus += AddText_SearchBox;

            Label_TypeOfSpecie.Text = "Label";
            Label_TypeOfSpecie.GotFocus += RemoveText_LabelType;
            Label_TypeOfSpecie.LostFocus += AddText_LabelType;

            Name_TypeOfSpecie.Text = "Name";
            Name_TypeOfSpecie.GotFocus += RemoveText_NameType;
            Name_TypeOfSpecie.LostFocus += AddText_NameType;

            Description_TypeOfSpecie.Text = "Description";
            Description_TypeOfSpecie.GotFocus += RemoveText_DescriptionType;
            Description_TypeOfSpecie.LostFocus += AddText_DescriptionType;

            Label_Tag.Text = "Label";
            Label_Tag.GotFocus += RemoveText_LabelTag;
            Label_Tag.LostFocus += AddText_LabelTag;

            Description_Tag.Text = "Description";
            Description_Tag.GotFocus += RemoveText_DescriptionTag;
            Description_Tag.LostFocus += AddText_DescriptionTag;
        }

        /* Adding and removing placeholders */
        public void RemoveText_SearchBox(object sender, EventArgs e)
        {
            SearchParam.Text = "";
        }

        public void AddText_SearchBox(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchParam.Text))
                SearchParam.Text = "Search...";
        }

        public void RemoveText_LabelType(object sender, EventArgs e)
        {
            Label_TypeOfSpecie.Text = "";
        }

        public void AddText_LabelType(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Label_TypeOfSpecie.Text))
                Label_TypeOfSpecie.Text = "Label";
        }

        public void RemoveText_NameType(object sender, EventArgs e)
        {
            Name_TypeOfSpecie.Text = "";
        }

        public void AddText_NameType(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_TypeOfSpecie.Text))
                Name_TypeOfSpecie.Text = "Name";
        }

        public void RemoveText_DescriptionType(object sender, EventArgs e)
        {
            Description_TypeOfSpecie.Text = "";
        }

        public void AddText_DescriptionType(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Description_TypeOfSpecie.Text))
                Description_TypeOfSpecie.Text = "Description";
        }

        public void RemoveText_LabelTag(object sender, EventArgs e)
        {
            Label_Tag.Text = "";
        }

        public void AddText_LabelTag(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Label_Tag.Text))
                Label_Tag.Text = "Label";
        }

        public void RemoveText_DescriptionTag(object sender, EventArgs e)
        {
            Description_Tag.Text = "";
        }

        public void AddText_DescriptionTag(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Description_Tag.Text))
                Description_Tag.Text = "Description";
        }

        /*
         * Saving 'Types of species' in json file
         */
        public void saveTypesOfSpecies()
        {
            string json = JsonConvert.SerializeObject(SpecieTypes);
            using (StreamWriter file = new StreamWriter(@"..\..\Data\specieTypes.json", false))
            {
                file.Write(json);
            }
        }

        /*
         * Cick on 'Add new tag' Button
         */
        private void BtnAddTag_Click(object sender, RoutedEventArgs e)
        {

        }

        /*
         * Cick on 'Add new type of species' Button
         */
        private void BtnAddType_Click(object sender, RoutedEventArgs e)
        {
            if (Label_TypeOfSpecie.Text == "Label" || Name_TypeOfSpecie.Text == "Name" || Description_TypeOfSpecie.Text == "Description")
            {
                MessageBoxResult result = MessageBox.Show("Some fileds are empty!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (SpecieType type in SpecieTypes)
            {
                if (type.Label == Label_TypeOfSpecie.Text)
                {
                    MessageBoxResult result = MessageBox.Show("Type with that label already exists!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            

            SpecieType newType = new SpecieType
            {
                Label = Label_TypeOfSpecie.Text,
                Name = Name_TypeOfSpecie.Text,
                Description = Description_TypeOfSpecie.Text
            };
            // Save new 'Type of specie'
            SpecieTypes.Add(newType);
            saveTypesOfSpecies();
        }

        /*
         * Cick on 'Add Specie' Button
         */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddSpecieWindow addSpecieWindow = new AddSpecieWindow();
            addSpecieWindow.Show();
        }

        /*
         * Cick on 'Loca Icon' Button for adding new type of specie
         */
        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (Label_TypeOfSpecie.Text == "Label" || Name_TypeOfSpecie.Text == "Name" || Description_TypeOfSpecie.Text == "Description")
            {
                MessageBoxResult result = MessageBox.Show("Please fill out information first!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool saved = false;
            foreach (SpecieType type in SpecieTypes)
            {
                if (type.Label == Label_TypeOfSpecie.Text)
                {
                    saved = true;
                    break;
                }
            }

            if (saved == false)
            {
                MessageBoxResult result = MessageBox.Show("Please save information first!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // open file dialog   
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // image filters  
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                // setting image file path 
                foreach (SpecieType type in SpecieTypes)
                {
                    if (type.Label == Label_TypeOfSpecie.Text)
                    {
                        type.Icon = openFileDialog.FileName;
                        break;
                    }
                }
                saveTypesOfSpecies();

                // display image in picture box  
                Icon_TypeOfSpecie.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
            }
        }
    }
}
