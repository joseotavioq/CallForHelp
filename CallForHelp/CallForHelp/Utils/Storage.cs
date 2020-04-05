using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System;

namespace CallForHelp.Utils
{
    public class Storage
    {
        private const string PERSON_INFO_KEY = "personInfo";
        private const string REGISTRATION_ID_KEY = "regIdInfo";

        public static async Task<bool> NameAlreadyExists()
        {
            var jsonObject = await SecureStorage.GetAsync(PERSON_INFO_KEY);

            return !string.IsNullOrEmpty(jsonObject);
        }

        public static async Task PersistPerson(string name, string email)
        {
            var regId = await SecureStorage.GetAsync(REGISTRATION_ID_KEY);

            var jsonObject = JsonConvert.SerializeObject(new Person
            {
                Name = name,
                Email = email,
                RegId = regId
            });

            await SecureStorage.SetAsync(PERSON_INFO_KEY, jsonObject);
        }

        public static async Task<Person> GetPersistedPerson()
        {
            var jsonString = await SecureStorage.GetAsync(PERSON_INFO_KEY);

            return JsonConvert.DeserializeObject<Person>(jsonString);
        }

        public static async Task PersistRegistrationId(string regID)
        {
            await SecureStorage.SetAsync(REGISTRATION_ID_KEY, regID);
        }
    }
}