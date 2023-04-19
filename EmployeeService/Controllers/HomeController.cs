using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeService.Controllers
{
    public class HomeController : Controller
    {
        
        public async Task<ActionResult> Index(Employee user)
        {
            ViewBag.Title = "Home Page";
            var list = await GetAllUser();
            if (list != null)       // Nếu list user khác null thì trả về View có chứa list 
                return View(list);
            return View();
        }
        private async Task<List<Employee>> GetAllUser() // Hàm Gọi API trả về list user 
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/"; // Lấy base uri của website 
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage res = await httpClient.GetAsync(baseUrl + "api/Employees");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Employee> list = new List<Employee>();
                    list = res.Content.ReadAsAsync<List<Employee>>().Result; return list;
                }
                return null;
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Employee e)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/"; // Lấy base uri của website 
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsJsonAsync(baseUrl + "api/Employees", e).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(e);
        }

        public ActionResult Edit(int id)
        {
            EmployeeDBContext db = new EmployeeDBContext();
            var emp = db.Employees.Find(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Employee e)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/"; // Lấy base uri của website 
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PutAsJsonAsync(baseUrl + "api/Employees", e).Result;
                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(e);
        }

     
        public async Task<ActionResult> Delete(int id)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/"; // Lấy base uri của website 
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.DeleteAsync(baseUrl + "api/Employees/"+id).Result;
                //response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}
