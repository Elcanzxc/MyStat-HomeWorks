namespace FTP.Models
{
    public class MenuItem
    {
        public string Text { get; }
        public Func<Task<string>> Func { get; }

        public MenuItem(string text, Func<Task<string>> func)
        {
            this.Text = text;
            this.Func = func;
        }
    }
}