namespace HyperlinkDemo
{
    public class HyperlinkLabel1 : Label
    {
        public static readonly BindableProperty UrlProperty = BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkLabel), null);

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public  HyperlinkLabel1()
        {
            TextDecorations = TextDecorations.Underline;
            TextColor = Colors.Blue;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => System.Diagnostics.Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", Url))

            });
        }
    }
}

