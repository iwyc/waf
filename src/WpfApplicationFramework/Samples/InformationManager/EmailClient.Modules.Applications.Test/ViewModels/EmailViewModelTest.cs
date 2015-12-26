﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waf.InformationManager.EmailClient.Modules.Applications.ViewModels;
using Waf.InformationManager.EmailClient.Modules.Domain.Emails;
using System.Waf.UnitTesting;

namespace Test.InformationManager.EmailClient.Modules.Applications.ViewModels
{
    [TestClass]
    public class EmailViewModelTest : EmailClientTest
    {
        [TestMethod]
        public void PropertiesTest()
        {
            var viewModel = Container.GetExportedValue<EmailViewModel>();

            var email = new Email();
            AssertHelper.PropertyChangedEvent(viewModel, x => x.Email, () => viewModel.Email = email);
            Assert.AreEqual(email, viewModel.Email);
        }
    }
}
