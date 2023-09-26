using MVC.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC
{
    [TestFixture]
    public class Testclass
    {
        [Test]
        public void BlogInfoIdTest()
        {
            var blog = new BlogInfo { BlogId = 1 };
            var blogIdTest = blog.BlogId;
            Assert.AreEqual(1, blogIdTest);
        }

        [Test]
        public void BlogInfoTitleTest()
        {
            var blog = new BlogInfo { Title = "Test" };
            var blogTitleTest = blog.Title;
            Assert.AreEqual("Test", blogTitleTest);
        }

        [Test]
        public void EmpInfoIdTest()
        {
            var emp = new EmpInfo { Id = 1 };
            var empIdTest = emp.Id;
            Assert.AreEqual(1, empIdTest);
        }

        [Test]
        public void EmpInfoNameTest()
        {
            var emp = new EmpInfo { Name = "Name" };
            var empNameTest = emp.Name;
            Assert.AreEqual("Name", empNameTest);
        }
    }
}