using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XF.Ctrls
{
    public partial class MainPage : ContentPage
    {
        List<string> Items1 = new List<string>();
        List<string> Items2 = new List<string>();
        bool IsItem1 = true;

        public MainPage()
        {
            InitializeComponent();

            for (int i = 0; i < 4; i++)
            {
                Items1.Add(i.ToString());
            }

            for (int i = 0; i < 10; i++)
            {
                Items2.Add(i.ToString());
            }

            dropdown.ItemsSource = Items1;
            dropdown.SelectedIndex = 1;
            dropdown.ItemSelected += OnDropdownSelected;
        }

        private void OnDropdownSelected(object sender, ItemSelectedEventArgs e)
        {
            label.Text = IsItem1 ? Items1[e.SelectedIndex] : Items2[e.SelectedIndex];
        }

        private void btn_Clicked(object sender, EventArgs e)
        {
            dropdown.ItemsSource = IsItem1 ? Items2 : Items1;
            dropdown.SelectedIndex = IsItem1 ? 5 : 1;
            IsItem1 = !IsItem1;
        }
    }
}
