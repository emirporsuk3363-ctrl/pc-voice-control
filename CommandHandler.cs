using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VoiceControlMVP
{
    public class CommandHandler
    {
        private readonly Action<string> _log;
        private readonly YouTubeService _yt;
        private readonly MailService _mail;

        public CommandHandler(Action<string> log)
        {
            _log = log;
            _yt = new YouTubeService(_log);
            _mail = new MailService(_log);
        }

        public async Task HandleCommandAsync(string text)
        {
            text = text.ToLowerInvariant();

            // Uygulama açma örnekleri
            if (Regex.IsMatch(text, @"\b(chrome|google chrome|chrome'u)\b") && text.Contains("aç"))
            {
                _log("Chrome açılıyor...");
                Process.Start(new ProcessStartInfo("chrome") { UseShellExecute = true });
                return;
            }
            if (text.Contains("notepad") || text.Contains("not defteri") || text.Contains("notepad") && text.Contains("aç"))
            {
                _log("Notepad açılıyor...");
                Process.Start(new ProcessStartInfo("notepad") { UseShellExecute = true });
                return;
            }

            // YouTube en yeni video açma: "vural üzül en yeni videosunu aç" veya "<kanal> en yeni videosunu aç"
            var m = Regex.Match(text, @"(.+?) (en yeni|son) (video|videosunu) aç");
            if (m.Success)
            {
                var kanal = m.Groups[1].Value.Trim();
                _log($"{kanal} kanalının en yeni videosu aranıyor...");
                var url = await _yt.GetLatestVideoUrlAsync(kanal);
                if (url != null)
                {
                    _log("Video açılıyor: " + url);
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else
                {
                    _log("Video bulunamadı.");
                }
                return;
            }

            // CMD açıp "hacker gibi" yazı simülasyonu
            if (text.Contains("cmd") && text.Contains("aç"))
            {
                _log("CMD açılıyor ve yeşil metin yazılıyor...");
                OpenCmdWithText();
                return;
            }

            // Dosya gönderme örneği: "dosyayı gönder" (configte belirtilen dosyayı gönderir)
            if (text.Contains("dosya gönder") || (text.Contains("gönder") && text.Contains("dosya")))
            {
                _log("Dosya gönderme tetiklendi (config içindeki path kullanılacak)...");
                if (System.IO.File.Exists(Config.FileToSendPath))
                {
                    await _mail.SendEmailWithAttachmentAsync(Config.SmtpRecipient, "Dosya gönderimi", "iyi oyunlar kanki", Config.FileToSendPath);
                }
                else
                {
                    _log($"Dosya bulunamadı: {Config.FileToSendPath} — README'deki Config kısmını kontrol et.");
                }
                return;
            }

            _log("Komut tanınmadı ya da şu an desteklenmiyor.");
        }

        private void OpenCmdWithText()
        {
            var psi = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };
            var p = Process.Start(psi);
            if (p != null)
            {
                // yeşil arka plan/siyah değilse boş ekran için color 0A yeşil yazı
                p.StandardInput.WriteLine("color 0A");
                p.StandardInput.WriteLine("echo Merhaba kanki, hacker modu acildi.");
                p.StandardInput.WriteLine("echo ---------------");
                // Bazı rastgele satırlar
                p.StandardInput.WriteLine("echo " + DateTime.Now);
            }
        }
    }
}
