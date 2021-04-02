using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidijaDrzave
{
    class CountryCityExtraction
    {
        #region Polja

        private ChromeDriver driver;
        private string adresa;
        private List<CountryCityObject> podaci;
        #endregion

        #region Konstruktori

        public CountryCityExtraction()
        {
            driver = new ChromeDriver();
            adresa = "https://geodata.solutions";
            podaci = new List<CountryCityObject>();
        }

        public void otvoriSranicu()
        {
            driver.Url = adresa;
        }

        public void extract()
        {
            IWebElement drzave = driver.FindElement(By.Id("countryId"));
            SelectElement listaDrzava = new SelectElement(drzave);
            CountryCityObject jednaDrzava;
            string[] zahtevaneDrzave = new string[]
            {
                "Serbia",
                "Bulgaria"
            };
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            for (int i=1; i < listaDrzava.Options.Count; i ++)
            {
                IWebElement option = listaDrzava.Options[i];
                string imeDrzave = option.Text;
                if (Array.Exists(zahtevaneDrzave,element => element == imeDrzave))
                {
                    option.Click();

                    jednaDrzava = new CountryCityObject { id = i, naziv = imeDrzave, skr = option.GetAttribute("countryid").ToString(), gradovi = new List<string>() };
                    IWebElement states = driver.FindElement(By.Id("stateId"));
                    SelectElement listaStates = new SelectElement(states);

                    wait.Until(driver => listaStates.Options[0].Text != "Please wait..");//od ove linije koda na dalje for se izvrsava tek kada u listi za states prestane da postoji Please wait..

                    for (int j = 1; j < listaStates.Options.Count; j++)
                    {
                        IWebElement optionStates = listaStates.Options[j];

                        optionStates.Click();

                        IWebElement cities = driver.FindElement(By.Id("cityId"));
                        SelectElement listaCities = new SelectElement(cities);
                        wait.Until(driver => listaCities.Options[0].Text != "Please wait..");

                        for (int k = 1; k< listaCities.Options.Count; k++)
                        {

                            IWebElement optionCities = listaCities.Options[k];
                            string imeGrada = optionCities.Text;

                            jednaDrzava.gradovi.Add(imeGrada);
                        }

                    }

                    podaci.Add(jednaDrzava);
                    
                }
           
            }

            exportToJSON(podaci, "drzave");

        }

        public void exportToJSON(List<CountryCityObject> data, string fileName)
        {
            string json = JsonConvert.SerializeObject(data.ToArray());
            System.IO.File.WriteAllText(@"C:\" + fileName + ".txt", json);
        }
        #endregion
    }
}
