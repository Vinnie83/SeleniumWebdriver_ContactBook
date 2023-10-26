

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class SeleniumTests
    {
        private const string baseUrl = "https://contactbook.velio4ka.repl.co/";
        private WebDriver driver;

        [SetUp] 
        public void OpenWebApp() 
        { 
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(baseUrl);
        }

        [TearDown] 
        
        public void CloseWebApp() 
        
        {
        
            driver.Quit();
        }

        [Test]

        public void Find_FirstContact()
        {

            var contactUrl = baseUrl + "/contacts";

            driver.Navigate().GoToUrl(contactUrl);

            var inputFirstName = driver.FindElement(By.CssSelector("#contact1 > tbody > tr.fname > td"));

            var inputLastName = driver.FindElement(By.CssSelector("#contact1 > tbody > tr.lname > td"));

            Assert.That(inputFirstName.Text, Is.EqualTo("Steve"));   
            Assert.That(inputLastName.Text, Is.EqualTo("Jobs"));   

        }

        [Test]

        public void Find_ContactByKeyword()
        {

            var searchUrl = baseUrl + "/contacts/search";

            driver.Navigate().GoToUrl(searchUrl);

            var inputKeyWord = driver.FindElement(By.Name("keyword"));
            inputKeyWord.SendKeys("albert");

            var buttonSearch = driver.FindElement(By.Id("search"));
            buttonSearch.Click();

            var inputFieldFirstName = driver.FindElement(By.CssSelector("#contact3 > tbody > tr.fname > td"));
            var inputFieldLastName = driver.FindElement(By.CssSelector("#contact3 > tbody > tr.lname > td"));

            Assert.That(inputFieldFirstName.Text, Is.EqualTo("Albert"));
            Assert.That(inputFieldLastName.Text, Is.EqualTo("Einstein"));


        }

        [Test]

        public void Find_ContactByKeyword_NoContact()
        {
            var keyword = "missing" + DateTime.Now.Ticks;
            var searchUrl = baseUrl + "/contacts/search";

            driver.Navigate().GoToUrl(searchUrl);

            var inputKeyWord = driver.FindElement(By.Name("keyword"));
            inputKeyWord.SendKeys(keyword);

            var buttonSearch = driver.FindElement(By.Id("search"));
            buttonSearch.Click();

            var searchResult = driver.FindElement(By.Id("searchResult"));
            Assert.That(searchResult.Text, Is.EqualTo("No contacts found."));


        }

        [Test]

        public void Test_AddContact_InvalidData()
        {
            
            var searchUrl = baseUrl + "/contacts/create";

            driver.Navigate().GoToUrl(searchUrl);

            var inputEmail = driver.FindElement(By.Id("email"));
            inputEmail.SendKeys("ivan@abv.bg");

            var addContact = driver.FindElement(By.Id("create"));
            addContact.Click();

            var errorMessage = driver.FindElement(By.CssSelector("body > main > div")); 
            Assert.That(errorMessage.Text, Is.EqualTo("Error: First name cannot be empty!"));

        }

        [Test]

        public void Test_AddContact_ValidData()
        {

            var searchUrl = baseUrl + "/contacts/create";

            driver.Navigate().GoToUrl(searchUrl);

            var inputFirstName = driver.FindElement(By.Id("firstName"));
            inputFirstName.SendKeys("Velin");

            var inputLastName = driver.FindElement(By.Id("lastName"));
            inputLastName.SendKeys("Atanasov");

            var inputEmail = driver.FindElement(By.Id("email"));
            inputEmail.SendKeys("vatanasov@abv.bg");

            var inputPhone = driver.FindElement(By.Id("phone"));
            inputPhone.SendKeys("834 567 907");

            var addContact = driver.FindElement(By.Id("create"));
            addContact.Click();

            var contactTables = driver.FindElements(By.CssSelector("table.contact-entry"));
            var lastTable = contactTables[contactTables.Count - 1];
            var textFieldFirstName = lastTable.FindElement(By.CssSelector("tr.fname td"));
            Assert.That(textFieldFirstName.Text, Is.EqualTo("Velin"));

            var textFieldLastName = lastTable.FindElement(By.CssSelector("tr.lname td"));
            Assert.That(textFieldLastName.Text, Is.EqualTo("Atanasov"));

            var textFieldEmail = lastTable.FindElement(By.CssSelector("tr.email td"));
            Assert.That(textFieldEmail.Text, Is.EqualTo("vatanasov@abv.bg"));

            var textFieldPhone = lastTable.FindElement(By.CssSelector("tr.phone td"));
            Assert.That(textFieldPhone.Text, Is.EqualTo("834 567 907"));


        }


    }
}