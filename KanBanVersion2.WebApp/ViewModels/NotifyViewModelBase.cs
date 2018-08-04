using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KanBanVersion2.WebApp.ViewModels
{
    public class NotifyViewModelBase<T>
    {
        public List<T> Items { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public int RedirectingTimeOut { get; set; }
        public bool IsRedirect { get; set; }
        public string RedirectingUrl { get; set; }


        public NotifyViewModelBase()
        {
            Items = new List<T>();
            Header = "Yönlediriliyorsunuz..";
            Title = "Geçersiz işlem";
            IsRedirect = true;
            RedirectingUrl = "/Home/Todo";
            RedirectingTimeOut = 10000;
        }
    }
}