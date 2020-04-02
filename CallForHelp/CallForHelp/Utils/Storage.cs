using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace CallForHelp.Utils
{
    public class Storage
    {
        private const string PERSON_INFO_KEY = "personInfo";

        public static async Task<bool> NameAlreadyExists()
        {
            var jsonObject = await SecureStorage.GetAsync(PERSON_INFO_KEY);

            return !string.IsNullOrEmpty(jsonObject);
        }

        public static async Task PersistPerson(string name, string email)
        {
            var jsonObject = JsonConvert.SerializeObject(new Person
            {
                Name = name,
                Email = email
            });

            await SecureStorage.SetAsync(PERSON_INFO_KEY, jsonObject);
        }

        public static async Task<Person> GetPersistedPerson()
        {
            var jsonString = await SecureStorage.GetAsync(PERSON_INFO_KEY);

            return JsonConvert.DeserializeObject<Person>(jsonString);
        }

        public static void ClearStorage()
        {
            SecureStorage.RemoveAll();
        }
    }
}