﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentInformationEntry
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new HomePage());
        }

        private void NavigateToStudentInformation_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new StudentInformation());
        }

        private void NavigateToHomePage_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new HomePage());
        }

        private void NavigateToAboutDevelopers_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new AboutDevelopers());
        }
    }
}