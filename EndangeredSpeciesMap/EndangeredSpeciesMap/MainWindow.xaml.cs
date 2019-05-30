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
            init();
        }

        public void init()
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
        }

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
    }
}
