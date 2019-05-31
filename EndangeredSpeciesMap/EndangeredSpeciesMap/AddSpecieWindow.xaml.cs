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
using System.Windows.Shapes;

namespace EndangeredSpeciesMap
{
    /// <summary>
    /// Interaction logic for AddSpecieWindow.xaml
    /// </summary>
    public partial class AddSpecieWindow : Window
    {
        public AddSpecieWindow()
        {
            Init();
        }

        public void Init()
        {
            InitializeComponent();

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
    }
}
