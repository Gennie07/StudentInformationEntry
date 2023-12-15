using System.Windows;
using System.Windows.Controls;

namespace StudentInformationEntry
{
    public partial class UpdateStudentWindow : Window
    {
        public UpdateStudentWindow()
        {
            InitializeComponent();
        }

        public string FirstName => txtFirstName.Text;
        public string LastName => txtLastName.Text;

        public int Age => int.Parse(intAge.Text);

        public string Gender
        {
            get => (cmbGender.SelectedItem as ComboBoxItem)?.Content as string;
            set => cmbGender.SelectedItem = cmbGender.Items.OfType<ComboBoxItem>().FirstOrDefault(item => (string)item.Content == value);
        }


        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
