using System;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace VoiceControlMVP
{
    public partial class MainWindow : Window
    {
        private readonly SpeechService _speech;
        private readonly CommandHandler _commands;

        public MainWindow()
        {
            InitializeComponent();
            _speech = new SpeechService();
            _commands = new CommandHandler(Log);
        }

        private void Log(string s)
        {
            Dispatcher.Invoke(() =>
            {
                TxtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {s}\n");
                TxtLog.ScrollToEnd();
            });
        }

        private async void BtnListen_Click(object sender, RoutedEventArgs e)
        {
            BtnListen.IsEnabled = false;
            Log("Mikrofon dinleniyor (tek seferlik)...");

            try
            {
                var text = await _speech.RecognizeOnceAsync();
                TxtTranscript.Text = text ?? "";
                Log($"Transkript: {text}");
                if (!string.IsNullOrEmpty(text))
                {
                    await _commands.HandleCommandAsync(text);
                }
            }
            catch (Exception ex)
            {
                Log("Hata: " + ex.Message);
            }
            finally
            {
                BtnListen.IsEnabled = true;
            }
        }
    }
}
