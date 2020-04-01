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

        public static async Task PersistName(string name)
        {
            await SecureStorage.SetAsync(NAME_ATTRIBUTE, name);
        }
    }
}