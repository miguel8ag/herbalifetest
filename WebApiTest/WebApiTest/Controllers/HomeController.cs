using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace WebApiTest.Controllers
{
    public class HomeController : Controller
    {

        private Uri baseUrl = new Uri("http://localhost:62449/api/students/");

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Students()
        {
            List<Student> students = new List<Student>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseUrl;
                var req = client.GetAsync("get_all");
                req.Wait();
                var result = req.Result;
                if (result.IsSuccessStatusCode)
                {
                    students = result.Content.ReadAsAsync<List<Student>>().Result;
                }
            }
            return View(students);
        }

        public ActionResult Details(int id)
        {
            Student student = new Student();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseUrl;
                var req = client.GetAsync(string.Format("get_by_ncontrol/{0}", id));
                req.Wait();
                var result = req.Result;
                if (result.IsSuccessStatusCode)
                {
                    student = result.Content.ReadAsAsync<Student>().Result;
                }
                else
                {
                    ViewBag.EditMsg = string.Format("Student with Control Number {0} does not exist.", id);
                    return View(student);
                }
            }
            return View(student);
        }

        public ActionResult Create()
        {
            return View(new Student());
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = baseUrl;
                var response = client.PostAsJsonAsync<Student>("insert", student);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultPost = result.Content.ReadAsAsync<bool>().Result;
                    if (resultPost)
                    {
                        return RedirectToAction("Students");
                    }
                    else
                    {
                        ViewBag.InsertMsg = "An error ocurred on insert student.";
                        return View(student);
                    }
                }
                else
                {
                    ViewBag.InsertMsg = "An error occurred...";
                    return View(student);
                }
            }
        }

        public ActionResult Edit(int id)
        {
            Student student = new Student();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseUrl;
                var req = client.GetAsync(string.Format("get_by_ncontrol/{0}", id));
                req.Wait();
                var result = req.Result;
                if (result.IsSuccessStatusCode)
                {
                    student = result.Content.ReadAsAsync<Student>().Result;
                }
                else
                {
                    ViewBag.EditMsg = string.Format("Student with Control Number {0} does not exist.", id);
                    return View(student);
                }
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = baseUrl;
                var response = client.PutAsJsonAsync<Student>("edit", student);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultPost = result.Content.ReadAsAsync<bool>().Result;
                    if (resultPost)
                    {
                        return RedirectToAction("Students");
                    }
                    else
                    {
                        ViewBag.InsertMsg = "An error ocurred on edit student.";
                        return View(student);
                    }
                }
                else
                {
                    ViewBag.InsertMsg = "An error occurred...";
                    return View(student);
                }
            }
        }

        public ActionResult Delete(int id)
        {
            Student student = new Student();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseUrl;
                var req = client.GetAsync(string.Format("get_by_ncontrol/{0}", id));
                req.Wait();
                var result = req.Result;
                if (result.IsSuccessStatusCode)
                {
                    student = result.Content.ReadAsAsync<Student>().Result;
                }
                else
                {
                    ViewBag.EditMsg = string.Format("Student with Control Number {0} does not exist.", id);
                    return View(student);
                }
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Delete(Student student)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = baseUrl;
                var response = client.DeleteAsync(string.Format("delete/{0}", student.NControl));
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultPost = result.Content.ReadAsAsync<bool>().Result;
                    if (resultPost)
                    {
                        return RedirectToAction("Students");
                    }
                    else
                    {
                        ViewBag.DeleteMsg = "An error ocurred on delete student.";
                        return View(student);
                    }
                }
                else
                {
                    ViewBag.DeleteMsg = "An error occurred...";
                    return View(student);
                }
            }
        }
    }
}
