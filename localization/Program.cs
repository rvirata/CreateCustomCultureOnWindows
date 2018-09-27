using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace localization
{
    class Program
    {
        static void Main(string[] args)
        {
            List<CustomCulture> cultures = new List<CustomCulture>();
            cultures.Add(new CustomCulture("en-US", "US", "en-DE", "en-DE", "English (German)", "English (German)", "German", "German"));
            cultures.Add(new CustomCulture("en-US", "US", "en-AT", "en-AT", "English (Austria)", "English (Austria)", "Austria", "Austria"));
            cultures.Add(new CustomCulture("en-US", "US", "en-DK", "en-DK", "English (Denmark)", "English (Denmark)", "Denmark", "Denmark"));
            cultures.Add(new CustomCulture("en-US", "US", "en-SE", "en-SE", "English (Sweden)", "English (Sweden)", "Sweden", "Sweden"));
            cultures.Add(new CustomCulture("en-US", "US", "en-FI", "en-FI", "English (Finland)", "English (Finland)", "Finland", "Finland"));
            cultures.Add(new CustomCulture("en-US", "US", "en-NL", "en-NL", "English (Netherlands)", "English (Netherlands)", "Netherlands", "Netherlands"));
            cultures.Add(new CustomCulture("en-US", "US", "en-BE", "en-BE", "English (Belgium)", "English (Belgium)", "Belgium", "Belgium"));
            cultures.Add(new CustomCulture("en-US", "US", "en-CH", "en-CH", "English (Switzerland)", "English (Switzerland)", "Switzerland", "Switzerland"));
            foreach (var culture in cultures)
            {
                try
                {
                    addCustomCulture(culture);
                    //removeCustomCulture(culture);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.ReadLine();
                }
            }
            Console.WriteLine("press enter key to continue");
            Console.ReadLine();
        }
        private static void GetCultures()
        {

            var sb = new StringBuilder();
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                //sb.Append(ci.Name + "\n");
                //sb.Append(ci.TwoLetterISOLanguageName + "\n");
                //sb.Append(ci.ThreeLetterISOLanguageName + "\n");
                //sb.Append(ci.ThreeLetterWindowsLanguageName + "\n");
                //sb.Append(ci.DisplayName + "\n");
                //sb.Append(ci.NativeName + "\n");
                //sb.Append(ci.EnglishName + "\n");
                if (ci.TwoLetterISOLanguageName.CompareTo("en") == 0)
                {
                    sb.Append(ci.Name + "\n");
                }
            }
            var console = sb.ToString();
            Console.Write(console);
            Console.ReadLine();
        }

        private static void removeCustomCulture(CustomCulture culture)
        {
            CultureAndRegionInfoBuilder.Unregister(culture.CultureName);
        }
        private static void addCustomCulture(CustomCulture culture)
        {
            CultureAndRegionInfoBuilder cib = null;
            try
            {
                var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
                var addCulture = true;
                foreach (CultureInfo info in cultures)
                {
                    if (string.Compare(info.Name, culture.CultureName, true) == 0)
                    {
                        addCulture = false;
                        break;
                    }
                }
                if (addCulture)
                {
                    Console.WriteLine("adding culture " + culture.CultureName);
                    cib = new CultureAndRegionInfoBuilder(culture.CultureName, CultureAndRegionModifiers.None);
                    cib.LoadDataFromCultureInfo(new CultureInfo(culture.BaseFrom));
                    cib.LoadDataFromRegionInfo(new RegionInfo(culture.BaseFromReg));
                    cib.CultureEnglishName = culture.EnglishName;
                    cib.CultureNativeName = culture.NativeName;
                    cib.IetfLanguageTag = culture.CultureLangTag;
                    cib.RegionEnglishName = culture.RegEnglishName;
                    cib.RegionNativeName = culture.RegNativeName;
                    cib.Register();
                    System.Console.WriteLine(cib.CultureName + " => created");
                }
                else
                {
                    Console.WriteLine("already exists " + culture.CultureName);
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
    class CustomCulture
    {
        public string BaseFrom { get; set; }
        public string BaseFromReg { get; set; }
        public string CultureName { get; set; }
        public string CultureLangTag { get; set; }
        public string EnglishName { get; set; }
        public string NativeName { get; set; }
        public string RegEnglishName { get; set; }
        public string RegNativeName { get; set; }

        public CustomCulture(string baseFrom, string baseFromReg, string cultureName, string cultureLangTag, string englishName, string nativeName, string regEnglishName, string regNativeName)
        {
            this.BaseFrom = baseFrom;
            this.BaseFromReg = baseFromReg;
            this.CultureName = cultureName;
            this.CultureLangTag = cultureLangTag;
            this.EnglishName = englishName;
            this.NativeName = nativeName;
            this.RegEnglishName = regEnglishName;
            this.RegNativeName = regNativeName;
        }

    }
}

