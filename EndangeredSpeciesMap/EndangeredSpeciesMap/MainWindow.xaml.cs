using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EndangeredSpeciesMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Init();
        }

        public void Init()
        {
            InitializeComponent();

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

        }

        /*
         * Cick on 'Add Specie' Button
         */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddSpecieWindow addSpecieWindow = new AddSpecieWindow();
            addSpecieWindow.Show();
            this.Close();
        }
    }
}
