using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WeilerGewinnRechner.enums;
using WeilerGewinnRechner.model;

namespace WeilerGewinnRechner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Resources.Add("showAnteileBox", Visibility.Hidden);

            InitializeComponent();

            this.gesellschafterListBox.Items.Add(new Gesellschafter(0));

            foreach (GesellschaftsForm form in (GesellschaftsForm[])Enum.GetValues(typeof(GesellschaftsForm)))
            {
                this.gesellschaftsFormenComboBox.Items.Add(form);
            }

            this.gesellschaftsFormenComboBox.SelectedIndex = 0;
        }

        private void calcBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Gesellschaft gesellschaft = new Gesellschaft((GesellschaftsForm)this.gesellschaftsFormenComboBox.SelectedItem, 
                    this.gesellschafterListBox.Items.OfType<Gesellschafter>().ToList(), 
                    Double.Parse(this.gewinnTBox.Text), 
                    Double.Parse(this.zinsenTBox.Text), 
                    this.verlustCheckbox.IsChecked == true);

                List<CalcResult> calcResults = new GesellschaftsRechner(gesellschaft).Calc();

                this.resultListBox.ItemsSource = calcResults;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception thrown");
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            this.gesellschafterListBox.Items.Add(new Gesellschafter(0));
        }

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.gesellschafterListBox.SelectedIndex >= 0)
                this.gesellschafterListBox.Items.Remove(this.gesellschafterListBox.SelectedItem);
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            this.gesellschafterListBox.SelectedIndex = -1;
            this.gesellschafterListBox.Items.Clear();
        }

        private Regex regex = new Regex("^([0-9]*)(\\,{0,1})([0-9]*)$");

        private void zinsenTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            zinsenTBox.Text = regex.IsMatch(zinsenTBox.Text) ? zinsenTBox.Text : "0";
        }

        private void gewinnTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            gewinnTBox.Text = regex.IsMatch(gewinnTBox.Text) ? gewinnTBox.Text : "0";
        }

        private void gesellschaftsFormenComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*switch (this.gesellschaftsFormenComboBox.SelectedItem)
            {
                case GesellschaftsForm.KG:
                    Resources["showAnteileBox"] = Visibility.Visible;
                    break;
                default:
                    Resources["showAnteileBox"] = Visibility.Hidden;
                    break;
            }*/
        }
    }
}
