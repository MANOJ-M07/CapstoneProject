﻿using MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class BlogController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5271/api");
        HttpClient client;

        public BlogController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
       /* public BlogController(HttpClient Client)
        {
            client = Client;
            client.BaseAddress = new Uri("http://localhost:5271/api");
        }*/

        public ActionResult Index()
        {
            List<BlogInfo> blogs = new List<BlogInfo>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/BlogInfoes").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                blogs = JsonConvert.DeserializeObject<List<BlogInfo>>(data);
            }
            return View(blogs);
        }
        public ActionResult GuestIndex()
        {
            List<BlogInfo> blogs = new List<BlogInfo>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/BlogInfoes").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                blogs = JsonConvert.DeserializeObject<List<BlogInfo>>(data);
            }
            return View(blogs);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BlogInfo blogs)
        {
            string data = JsonConvert.SerializeObject(blogs);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responce = client.PostAsync(client.BaseAddress + "/BlogInfoes", content).Result;
            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            BlogInfo blogs = new BlogInfo();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/BlogInfoes/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                blogs = JsonConvert.DeserializeObject<BlogInfo>(data);
            }
            return View(blogs);
        }

        [HttpPost]
        public ActionResult Edit(BlogInfo blog)
        {
            try
            {
                string data = JsonConvert.SerializeObject(blog);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/BlogInfoes/" + blog.BlogId, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating blog.");
                    return View(blog);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                return View(blog);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                BlogInfo blogs = new BlogInfo();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/BlogInfoes/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    blogs = JsonConvert.DeserializeObject<BlogInfo>(data);
                }
                return View(blogs);
            }
            catch (Exception)
            {
                return View();
            }

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/BlogInfoes/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return View();
                throw;
            }
            return View();
        }
    }
}