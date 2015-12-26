﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Globalization;
using System.Waf.Foundation;
using Waf.BookLibrary.Library.Domain;
using Waf.BookLibrary.Library.Domain.Properties;

namespace Test.BookLibrary.Library.Domain
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void GeneralPersonTest()
        {
            Person person = new Person();
            Assert.IsNotNull(person.Id);

            person.Firstname = "Harry";
            person.Lastname = "Potter";
            person.Email = "harry.potter@hogwarts.edu";

            Assert.IsTrue(person.Validate());

            Assert.AreEqual("Harry Potter", person.ToString(null, CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void PersonFirstnameValidationTest()
        {
            Person person = new Person();
            person.Validate();

            Assert.AreEqual("", person.Firstname);
            Assert.AreEqual(Resources.FirstnameMandatory, person.GetErrors("Firstname").Single().ErrorMessage);
            
            person.Firstname = new string('A', 31);
            Assert.AreEqual(string.Format(CultureInfo.CurrentCulture, Resources.FirstnameMaxLength, "Firstname", 30), 
                person.GetErrors("Firstname").Single().ErrorMessage);

            person.Firstname = new string('A', 30);
            Assert.IsFalse(person.GetErrors("Firstname").Any());
        }

        [TestMethod]
        public void PersonLastnameValidationTest()
        {
            Person person = new Person();
            person.Validate();

            Assert.AreEqual("", person.Lastname);
            Assert.AreEqual(Resources.LastnameMandatory, person.GetErrors("Lastname").Single().ErrorMessage);

            person.Lastname = new string('A', 31);
            Assert.AreEqual(string.Format(CultureInfo.CurrentCulture, Resources.LastnameMaxLength, "Lastname",30),
                person.GetErrors("Lastname").Single().ErrorMessage);

            person.Lastname = new string('A', 30);
            Assert.IsFalse(person.GetErrors("Lastname").Any());
        }

        [TestMethod]
        public void PersonEmailValidationTest()
        {
            Person person = new Person();
            person.Validate();

            Assert.IsNull(person.Email);
            Assert.IsFalse(person.GetErrors("Email").Any());

            person.Email = "";
            Assert.IsNull(person.Email);
            Assert.IsFalse(person.GetErrors("Email").Any());

            person.Email = new string('A', 92) + "@mail.com";
            Assert.AreEqual(string.Format(CultureInfo.CurrentCulture, Resources.EmailMaxLength, "Email", 100),
                person.GetErrors("Email").Single().ErrorMessage);

            person.Email = "my." + new string('A', 88) + "@mail.com";
            Assert.IsFalse(person.GetErrors("Email").Any());

            person.Email = "harry.potter";
            Assert.AreEqual(Resources.EmailInvalid, person.GetErrors("Email").Single().ErrorMessage);
            
            person.Email = "harry.potter@hogwarts";
            Assert.AreEqual(Resources.EmailInvalid, person.GetErrors("Email").Single().ErrorMessage);
            
            person.Email = "harry@hogwarts.edu";
            Assert.IsFalse(person.GetErrors("Email").Any());
        }
    }
}
