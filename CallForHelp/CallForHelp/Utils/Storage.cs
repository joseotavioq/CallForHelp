using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CallForHelp.Utils
{
    public class Storage
    {
        private const string NAME_ATTRIBUTE = "Name";

        public static async Task<bool> IsNameExists()
        {
            var name = await SecureStorage.GetAsync(NAME_ATTRIBUTE);

            return !string.IsNullOrEmpty(name);
        }

        public static async Task PersistPerson(string name, string email)
        {
            var jsonObject = JsonConvert.SerializeObject(new Person
            {
                Name = name,
                Email = email
            });

            await SecureStorage.SetAsync(NAME_ATTRIBUTE, jsonObject);
        }

        public static async Task<Person> GetPersistedPerson()
        {
            var jsonString = await SecureStorage.GetAsync(NAME_ATTRIBUTE);

            return JsonConvert.DeserializeObject<Person>(jsonString);
        }

        public static void ClearStorage()
        {
            SecureStorage.RemoveAll();
        }
    }
}
