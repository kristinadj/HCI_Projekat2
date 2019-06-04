using EndangeredSpeciesMap.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public static Dictionary<StatusOfEndangerment, List<String>> Tips;

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

            // Initialize 'Tips' dictionary
            Tips = initTipsDictionary();

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
         * On Closing - saving all species in Species collestion
         */
        void Window_Closing(object sender, CancelEventArgs e)
        {
            updateMainList(SpeciesOnMap1);
            updateMainList(SpeciesOnMap2);
            updateMainList(SpeciesOnMap3);
            updateMainList(SpeciesOnMap4);
            saveSpecies();
        }

        private void updateMainList(ObservableCollection<Specie> collection)
        {
            foreach (Specie sp in collection)
            {
                if (!inSpecies(sp.ID))
                    Species.Add(sp);
            }
        }

        private bool inSpecies(String id)
        {
            foreach (Specie specie in Species)
            {
                if (specie.ID.Equals(id))
                    return true;
            }
            return false;
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
            if (Label_Tag.Text == "Label" || Description_Tag.Text == "Description")
            {
                MessageBoxResult result = MessageBox.Show("Some fileds are empty!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (Tag type in Tags)
            {
                if (type.Label == Label_Tag.Text)
                {
                    MessageBoxResult result = MessageBox.Show("Tag with that label already exists!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            Tag newTag = new Tag
            {
                Label = Label_Tag.Text,
                Description = Description_Tag.Text,
                Colour = cp.SelectedColor.Value.ToString()
            };
            // Save new 'Type of specie'
            Tags.Add(newTag);
            saveTags();
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

            if (Icon_TypeOfSpecie.Source.ToString().Equals("pack://application:,,,/EndangeredSpeciesMap;component/Images/picture.png"))
            {
                MessageBoxResult result = MessageBox.Show("You have to upload an icon!", "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SpecieType newType = new SpecieType
            {
                Label = Label_TypeOfSpecie.Text,
                Name = Name_TypeOfSpecie.Text,
                Description = Description_TypeOfSpecie.Text,
                Icon = Icon_TypeOfSpecie.Source.ToString()
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

                if (TipsItem.IsChecked)
                {
                    Random rnd = new Random();
                    int tipNum = rnd.Next(0, Tips[specie.Endangerment].Count);
                    MessageBoxResult tip = MessageBox.Show(Tips[specie.Endangerment][tipNum], "Endangered Species", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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

        /*
         * Paste specie icon on map
         */
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
                    SpeciesOnMap1.Add(selectedOnMap);
                }
                else if (tab.Header.Equals("Map #2"))
                {
                    Canvas2.Children.Add(imgSelectedOnMap);
                    SpeciesOnMap2.Add(selectedOnMap);
                }
                else if (tab.Header.Equals("Map #3"))
                {
                    Canvas3.Children.Add(imgSelectedOnMap);
                    SpeciesOnMap3.Add(selectedOnMap);
                }
                else
                {
                    Canvas4.Children.Add(imgSelectedOnMap);
                    SpeciesOnMap4.Add(selectedOnMap);
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

        /*
         * Initializing dicitonary with tips for user
         */  
        private Dictionary<StatusOfEndangerment, List<String>> initTipsDictionary()
        {
            Dictionary<StatusOfEndangerment, List<String>> dict = new Dictionary<StatusOfEndangerment, List<String>>();

            dict[StatusOfEndangerment.CriticallyEndangered] = new List<String>();
            dict[StatusOfEndangerment.CriticallyEndangered].Add("Critically endanegered specie - Faces an extremely high risk of extinction in the immediate future.");
            dict[StatusOfEndangerment.CriticallyEndangered].Add("Critically endanegered specie - If the reasons for population reduction no longer occur and can be reversed, the population needs to have been reduced by at least 90 % ");
            dict[StatusOfEndangerment.CriticallyEndangered].Add("Critically endanegered specie - Severe habitat fragmentation or existing at just one location");
            dict[StatusOfEndangerment.CriticallyEndangered].Add("Critically endanegered specie - Numbers less than 50");
            dict[StatusOfEndangerment.CriticallyEndangered].Add("Critically endanegered specie - At least 50% chance of going Extinct in the Wild over 10 years");

            dict[StatusOfEndangerment.Endangered] = new List<String>();
            dict[StatusOfEndangerment.Endangered].Add("Endangered specie - Very likely to become extinct in the near future.");
            dict[StatusOfEndangerment.Endangered].Add("Endangered specie - Many nations have laws that protect conservation-reliant species: for example, forbidding hunting, restricting land development or creating protected areas.");
            dict[StatusOfEndangerment.Endangered].Add("Endangered specie - Severely fragmented or known to exist at no more than five locations.");
            dict[StatusOfEndangerment.Endangered].Add("Endangered specie - Population estimated to number fewer than 2,500 mature individuals");
            dict[StatusOfEndangerment.Endangered].Add("Endangered specie - Probability of extinction in the wild is at least 20% within 20 years or five generations");


            dict[StatusOfEndangerment.Vulnerable] = new List<String>();
            dict[StatusOfEndangerment.Vulnerable].Add("Vulnerable specie- Likely to become endangered unless the circumstances that are threatening its survival and reproduction improve");
            dict[StatusOfEndangerment.Vulnerable].Add("Vulnerable specie - Vulnerability is mainly caused by habitat loss or destruction of the species home.");
            dict[StatusOfEndangerment.Vulnerable].Add("Vulnerable specie - Severely fragmented or known to exist at no more than ten locations.");
            dict[StatusOfEndangerment.Vulnerable].Add("Vulnerable specie - Population estimated to number fewer than 10,000 mature individuals");
            dict[StatusOfEndangerment.Vulnerable].Add("Vulnerable specie - Probability of extinction in the wild is at least at least 10% within 100 years.");


            dict[StatusOfEndangerment.DependentOnTheHabitat] = new List<String>();
            dict[StatusOfEndangerment.DependentOnTheHabitat].Add("Dependent on the habitat - Dependent on conservation efforts to prevent it from becoming threatened with extinction");
            dict[StatusOfEndangerment.DependentOnTheHabitat].Add("Dependent on the habitat - Such species must be the focus of a continuing species-specific and/or habitat-specific conservation programme, the cessation of which would result in the species qualifying for one of the threatened categories within a period of five years.");
            dict[StatusOfEndangerment.DependentOnTheHabitat].Add("Dependent on the habitat - Examples of conservation-dependent species include the black caiman (Melanosuchus niger), the sinarapan, the California ground cricket, and the flowering plant Garcinia hermonii.");

            dict[StatusOfEndangerment.CloseToRisk] = new List<String>();
            dict[StatusOfEndangerment.CloseToRisk].Add("Close to risk to extinct - May be considered threatened with extinction in the near future, although it does not currently qualify for the threatened status.");

            dict[StatusOfEndangerment.SmallestRisk] = new List<String>();
            dict[StatusOfEndangerment.SmallestRisk].Add("Smallest risk of endangerment - Evaluated as not being a focus of species conservation");

            return dict;
        }

        private void EditSpecie_Click(object sender, RoutedEventArgs e)
        {
            Specie selected = (Specie)SpecieList.SelectedItem;
            EditSpecieWindow editSpecieWindow = new EditSpecieWindow(selected);
            editSpecieWindow.Show();
        }

        private void DeleteSpecie_Click(object sender, RoutedEventArgs e)
        {
            Specie selected = (Specie)SpecieList.SelectedItem;
            if (Species.Contains(selected)) {
                Species.Remove(selected);
                saveSpecies();
                MessageBoxResult success = MessageBox.Show("You have successfully deleted specie!", "Endangered Species", MessageBoxButton.OK);
            }
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Specie selected = (Specie)SpecieList.SelectedItem;
            DetailsWindow detailsWindow = new DetailsWindow(selected);
            detailsWindow.Show();
        }

        private void DeleteType_Click(object sender, RoutedEventArgs e)
        {
            SpecieType selected = (SpecieType)Types.SelectedItem;
            SpecieTypes.Remove(selected);
            saveTypesOfSpecies();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SpecieList.ItemsSource = Species;
            if (OnMap.IsChecked == true) {
                if(Tabs.SelectedIndex == 0)
                    SpecieList.ItemsSource = SpeciesOnMap1;
                else if(Tabs.SelectedIndex == 1)
                    SpecieList.ItemsSource = SpeciesOnMap2;
                else if (Tabs.SelectedIndex == 2)
                    SpecieList.ItemsSource = SpeciesOnMap3;
                else if (Tabs.SelectedIndex == 3)
                    SpecieList.ItemsSource = SpeciesOnMap4;
            }

            SpecieList.Items.Filter = item =>
            {
                Specie specI = item as Specie;
                if (specI == null) return false;

                if (SearchParam.Text == "Search...")
                    return true;

                return specI.ID.Contains(SearchParam.Text) || specI.Name.Contains(SearchParam.Text);
            };
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Filter.SelectedIndex != 0)
            {
                if (Filter.SelectedIndex <= 6)
                {
                    SpecieList.Items.Filter = item =>
                    {
                        Specie specI = item as Specie;
                        if (specI == null) return false;

                        return specI.Endangerment == (StatusOfEndangerment)Filter.SelectedIndex;
                    };
                }
                else
                {
                    foreach (Specie spec in Species)
                    {
                        SpecieList.Items.Filter = item =>
                        {
                            Specie specI = item as Specie;
                            if (specI == null) return false;

                            return (specI.Endangerment != (StatusOfEndangerment)(Filter.SelectedIndex - 6));
                        };
                    }
                }

            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, this);

            }
        }

        public void doThings(string param)
        {
            btnAddSpecie.Background = new SolidColorBrush(Color.FromRgb(32, 64, 128));
            Title = param;
        }

        private void DeleteTag_Click(object sender, RoutedEventArgs e)
        {
            Tag selected = (Tag)TagList.SelectedItem;
            Tags.Remove(selected);
            saveTags();
        }
    }
}
