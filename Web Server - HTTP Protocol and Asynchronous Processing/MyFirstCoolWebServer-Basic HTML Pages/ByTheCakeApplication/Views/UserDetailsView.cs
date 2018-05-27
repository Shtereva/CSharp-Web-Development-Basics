using System;
using MyFirstCoolWebServer.Server.Contracts;
using MyFirstCoolWebServerBasicHTMLPages.ByTheCakeApplication;

namespace MyFirstCoolWebServer.Application.Views
{
    public class UserDetailsView : IView
    {
        private readonly Model model;

        public UserDetailsView(Model model)
        {
            this.model = model;
        }

        public string View()
        {
            return $"<body>Hello, {this.model["name"]}!</body>" + Environment.NewLine;
        }
    }
}