using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringTemplates
{
    [TestClass]
    public class UnitTest1
    {
        private const string Template = "Her name is {0} and her birthday is on {1:MM/dd/yyyy}, which is in {1:MMMM}.";

        [TestMethod]
        public void Template1()
        {
            var s = string.Format(Template, "April", new DateTime(2000, 4, 10));
            Console.WriteLine(s);
        }

        [TestMethod]
        public void Template2()
        {
            var s = new MyString("April", new DateTime(2000, 4, 10));
            Console.WriteLine(s);
        }
    }

    public class MyString
    {
        private const string Template = @"Her name is {name} and her birthday is on {dob}, which is in {month}.";
        private readonly Dictionary<string, string> _parameters = new();

        public MyString(string name, DateTime dob)
        {
            // validate parameters before assignments

            _parameters.Add(@"{name}", name);
            _parameters.Add(@"{dob}", $"{dob:MM/dd/yyyy}");
            _parameters.Add(@"{month}", $"{dob:MMMM}");
        }

        public override string ToString()
        {
            return _parameters.Aggregate(Template, (s, kv) => s.Replace(kv.Key, kv.Value));
        }
    }
}
