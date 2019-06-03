using EndangeredSpeciesMap.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EndangeredSpeciesMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Point startPoint = new System.Windows.Point();

        public static ObservableCollection<Specie> Species { get; set; }
        public static ObservableCollection<Specie> SpeciesOnMap1 { get; set; }
        public static ObservableCollection<Specie> SpeciesOnMap2 { get; set; }
        public static ObservableCollection<Specie> SpeciesOnMap3 { get; set; }
        public static ObservableCollection<Specie> SpeciesOnMap4 { get; set; }
        public static ObservableCollection<SpecieType> SpecieTypes {get; set;}
        public static ObservableCollection<Tag> Tags { get; set; }

        Specie selectedForDrag;
        Specie selectedOnMap = null;
        Image imgSelectedOnMap = null;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Init();
        }

        public void Init()
        {
            SpeciesOnMap1 = new ObservableCollection<Specie>();
            SpeciesOnMap2 = new ObservableCollection<Specie>();
            SpeciesOnMap3 = new ObservableCollection<Specie>();
            SpeciesOnMap4 = new ObservableCollection<Specie>();

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
                Tags = JsonConvert.DeserializeObject<ObservableCollection<Tag>>(json);
            } else
            {
                Tags = new ObservableCollection<Tag>();
            }


            // Reading 'Species' from json file
            if (File.Exists(@"..\..\Data\species.json"))
            {
                String json = System.IO.File.ReadAllText(@"..\..\Data\species.json");
                Species = JsonConvert.DeserializeObject<ObservableCollection<Specie>>(json);
            } else
            {
                Species = new ObservableCollection<Specie>();
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
         * Saving 'Species' in json file
         */
        public static void saveSpecies()
        {
            string json = JsonConvert.SerializeObject(Species);
            using (StreamWriter file = new StreamWriter(@"..\..\Data\species.json", false))
            {
                file.Write(json);
            }
        }

        /*
         * Saving 'Tags' in json file
         */
        public void saveTags()
        {
            string json = JsonConvert.SerializeObject(Tags);
            using (StreamWriter file = new StreamWriter(@"..\..\Data\tags.json", false))
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
                MessageBoxResult result = MessageBox.Show("Some fileds are empty!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (SpecieType type in SpecieTypes)
            {
                if (type.Label == Label_TypeOfSpecie.Text)
                {
                    MessageBoxResult result = MessageBox.Show("Type with that label already exists!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            SpecieType newType = new SpecieType
            {
                Label = Label_TypeOfSpecie.Text,
                Name = Name_TypeOfSpecie.Text,
                Description = Description_TypeOfSpecie.Text,
                Icon = Icon_TypeOfSpecie.Source
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
            // open file dialog   
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // image filters  
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                // display image in picture box  
                Icon_TypeOfSpecie.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
            }
        }

        /* Drag and drop */
        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);

        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                // Find the data behind the ListViewItem
                Specie specie = (Specie)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                Image image = new Image();
                image.Source = new BitmapImage(new Uri(specie.Icon.ToString(), UriKind.Absolute));

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", specie);
                DragDrop.DoDragDrop(image, dragData, DragDropEffects.Move);
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Specie specie = e.Data.GetData("myFormat") as Specie;

                // Add to list for specific map
                TabItem tab = Tabs.SelectedItem as TabItem;
                if (tab.Header.Equals("Map #1")) {
                    SpeciesOnMap1.Add(specie);
                }   
                else if (tab.Header.Equals("Map #2"))
                {
                    SpeciesOnMap2.Add(specie);
                } 
                else if (tab.Header.Equals("Map #3"))
                {
                    SpeciesOnMap3.Add(specie);
                } 
                else
                {
                    SpeciesOnMap4.Add(specie);
                }
                Species.Remove(specie);

                Image image = new Image();
                if (!specie.Icon.Equals(""))
                    image.Source = new BitmapImage(new Uri(specie.Icon.ToString(), UriKind.Absolute));

                image.Width = 50;
                image.Height = 50;
                image.Tag = specie.ID;

                image.PreviewMouseLeftButtonDown += DraggedIcon_PreviewMouseLeftButtonDown;
                image.PreviewMouseMove += DraggedIcon_MouseMove;
                image.PreviewMouseRightButtonUp += DraggedIcon_PreviewMouseRightButtonUp;


                var positionX = e.GetPosition(sender as Canvas).X;
                var positionY = e.GetPosition(sender as Canvas).Y;

                specie.X = positionX;
                specie.Y = positionY;

                (sender as Canvas).Children.Add(image);
                Canvas.SetLeft(image, positionX - image.Width / 2.0);
                Canvas.SetTop(image, positionY - image.Height / 2.0);
            }
        }

        private void DraggedIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            startPoint = e.GetPosition(null);
            Image img = sender as Image;

            TabItem tab = Tabs.SelectedItem as TabItem;
            if (tab.Header.Equals("Map #1"))
            {
                foreach (Specie specie in SpeciesOnMap1)
                {
                    if (specie.ID.Equals(img.Tag))
                    {
                        selectedForDrag = specie;
                    }
                }
            }
            else if (tab.Header.Equals("Map #2"))
            {
                foreach (Specie specie in SpeciesOnMap2)
                {
                    if (specie.ID.Equals(img.Tag))
                    {
                        selectedForDrag = specie;
                    }
                }
            }
            else if (tab.Header.Equals("Map #3"))
            {
                foreach (Specie specie in SpeciesOnMap3)
                {
                    if (specie.ID.Equals(img.Tag))
                    {
                        selectedForDrag = specie;
                    }
                }
            }
            else
            {
                foreach (Specie specie in SpeciesOnMap4)
                {
                    if (specie.ID.Equals(img.Tag))
                    {
                        selectedForDrag = specie;
                    }
                }
            }

            if (e.LeftButton == MouseButtonState.Released)
                e.Handled = true;
        }

        
        private void DraggedIcon_PreviewMouseRightButtonUp(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            Point point = e.GetPosition(sender as Canvas);
            Image img = sender as Image;

            TabItem tab = Tabs.SelectedItem as TabItem;
            if (tab.Header.Equals("Map #1"))
            {
                foreach (Specie specie in SpeciesOnMap1)
                {
                    if (specie.ID.Equals(img.Tag))
                    {
                        selectedOnMap = specie;
                    }
                }
            }
            else if (tab.Header.Equals("Map #2"))
            {
                foreach (Specie specie in SpeciesOnMap2)
                {
                    if (specie.ID.Equals(img.Tag))
                    {
                        selectedOnMap = specie;
                    }
                }
            }
            else if (tab.Header.Equals("Map #3"))
            {
                foreach (Specie specie in SpeciesOnMap3)
                {
                    if (specie.ID.Equals(img.Tag))
                    {
                        selectedOnMap = specie;
                    }
                }
            }
            else
            {
                foreach (Specie specie in SpeciesOnMap4)
                {
                    if (specie.ID.Equals(img.Tag))
                    {
                        selectedOnMap = specie;
                    }
                }
            }

            imgSelectedOnMap = sender as Image;

            // Icon context menu
            System.Windows.Forms.ContextMenuStrip iconContextMenu = new System.Windows.Forms.ContextMenuStrip();
            iconContextMenu.Items.Add("Cut", null, DraggedIcon_Cut);
            iconContextMenu.Items.Add("Remove", null, DraggedIcon_Remove);

            if (e.RightButton == MouseButtonState.Released)
            {
                iconContextMenu.Show(new System.Drawing.Point((int)point.X, (int)point.Y));  //places the menu at the pointer position
            }
        }

        /*
         * Cutting specie from map
         */
        private void DraggedIcon_Cut(object sender, EventArgs e)
        {
            TabItem tab = Tabs.SelectedItem as TabItem;

            if (tab.Header.Equals("Map #1"))
            {
                SpeciesOnMap1.Remove(selectedOnMap);
            }
            else if (tab.Header.Equals("Map #2"))
            {
                SpeciesOnMap2.Remove(selectedOnMap);
            }
            else if (tab.Header.Equals("Map #3"))
            {
                SpeciesOnMap3.Remove(selectedOnMap);
            }
            else
            {
                SpeciesOnMap4.Remove(selectedOnMap);
            }

            (imgSelectedOnMap.Parent as Canvas).Children.Remove(imgSelectedOnMap);
        }

        /*
         * Removing specie from map
         */
        private void DraggedIcon_Remove(object sender, EventArgs e)
        {
            TabItem tab = Tabs.SelectedItem as TabItem;
            if (tab.Header.Equals("Map #1"))
            {
                SpeciesOnMap1.Remove(selectedOnMap);   
            }
            else if (tab.Header.Equals("Map #2"))
            {
                SpeciesOnMap2.Remove(selectedOnMap);
            }
            else if (tab.Header.Equals("Map #3"))
            {
                SpeciesOnMap3.Remove(selectedOnMap);
            }
            else
            {
                SpeciesOnMap4.Remove(selectedOnMap);
            }

            (imgSelectedOnMap.Parent as Canvas).Children.Remove(imgSelectedOnMap);
            saveSpecies();
            selectedOnMap = null;
            imgSelectedOnMap = null;
        }

        private void Canvas_Paste(object sender, RoutedEventArgs e)
        {
            if (selectedOnMap == null)
            {
                MessageBox.Show("Nothing is copied!", "Endangered species", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else
            {
                TabItem tab = Tabs.SelectedItem as TabItem;
                if (tab.Header.Equals("Map #1"))
                {
                    Canvas1.Children.Add(imgSelectedOnMap);
                }
                else if (tab.Header.Equals("Map #2"))
                {
                    Canvas2.Children.Add(imgSelectedOnMap);
                }
                else if (tab.Header.Equals("Map #3"))
                {
                    Canvas3.Children.Add(imgSelectedOnMap);
                }
                else
                {
                    Canvas4.Children.Add(imgSelectedOnMap);
                }  
            }
            saveSpecies();
        }

        private void DraggedIcon_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
               Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                Image img = sender as Image;
                (img.Parent as Canvas).Children.Remove(img);

                Specie selected = selectedForDrag;

                var positionX = e.GetPosition(sender as Canvas).X;
                var positionY = e.GetPosition(sender as Canvas).Y;

                selected.X = positionX;
                selected.Y = positionY;

                DataObject dragData = new DataObject("myFormat", selected);
                DragDrop.DoDragDrop(img, dragData, DragDropEffects.Move);
                e.Handled = true;

            }

        }
    }
}
