namespace ReutersClient
{
    public class ReutersClientFactory
    {

        public static ReutersClient Create(Action<ReutersConnectionSettings> settings)
        {
            ReutersConnectionSettings reutersConnectionSettings = new ReutersConnectionSettings();
            settings(reutersConnectionSettings);
            return new ReutersClient(reutersConnectionSettings);

        }
    }
}
