using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Windows;
using Newtonsoft.Json;
using Month06YueKaoMVC.Models;

namespace Month06YueKaoMVC.Controllers
{
    public class DefaultController : Controller
    {
        //定义cache储存信息
        public static Cache cache1 = new Cache();
        // GET: Default
        public ActionResult Index()
        {
            Cache cache = new Cache();
            var date = DateTime.MaxValue;
            TimeSpan timeSpan = new TimeSpan(1, 1, 1);
            cache.Add("userInfo", "10.1.172.13", null, date, timeSpan, CacheItemPriority.AboveNormal, null);
            var list = cache.Get("userInfo");
            ViewBag.sl = list;
            //获取对应的catch缓存
            var list1 = cache1.Get("userInfo1");
            ViewBag.dz = list1;
            return View();
        }
        //第一级菜单
        public string GetS()
        {            
            var list = Hctp.GetApi("get", "getList");
            var list1 = JsonConvert.DeserializeObject<List<AddressInfo>>(list);
            var list2 = list1.Where(m => m.Pid == 0).ToList();
            return JsonConvert.SerializeObject(list2);
        }
        //第二三级菜单
        public string GetErSanJI(int id)
        {
            var list = Hctp.GetApi("get", "getList");
            var list1 = JsonConvert.DeserializeObject<List<AddressInfo>>(list);
            var list2 = list1.Where(m => m.Pid == id).ToList();
            return JsonConvert.SerializeObject(list2);
        }
        //把信息添加进缓存里
        public int AddAdre(string one,string two,string three)
        {
            var city = Hctp.GetApi("get", "getList");
            //根据传过来的Id查询对应的城市
            var city1 = JsonConvert.DeserializeObject<List<AddressInfo>>(city);
            one = city1.Where(m => m.Id == int.Parse(one)).FirstOrDefault().Name;
            two = city1.Where(m => m.Id == int.Parse(two)).FirstOrDefault().Name;
            three = city1.Where(m => m.Id == int.Parse(three)).FirstOrDefault().Name;

            var list = one + two + three; 
            var date = DateTime.MaxValue;
            TimeSpan timeSpan = new TimeSpan(1,1,1);
            //添加进去
            cache1.Add("userInfo1", list, null, date, timeSpan, CacheItemPriority.AboveNormal, null);
            var i = cache1.Count;
            //判断是否成功
            return i;
            
        }
    }
}