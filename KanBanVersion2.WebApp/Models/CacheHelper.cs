using KanBanVersion2.BusinessLayer;
using KanBanVersion2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace KanBanVersion2.WebApp.Models
{
    public class CacheHelper
    {
        
        public static List<Proje> GetProjectFromCache()
        {
            var result = WebCache.Get("Proje-cache");
            if(result ==null)
            {
                ProjectManager projectManager = new ProjectManager();
                result = projectManager.List();
                WebCache.Set("proje-cache" ,result,20,true);
            }
            return result;
            
        }
        public static void RemoveProjectFromCache()
        {
            WebCache.Remove("proje-cache");
        }

        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }
    }
}