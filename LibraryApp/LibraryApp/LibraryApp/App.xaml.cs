﻿using LibraryApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Books());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
