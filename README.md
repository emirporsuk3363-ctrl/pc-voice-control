# VoiceControl MVP - Quick Start

Gereksinimler
- Windows 10/11
- Visual Studio 2022 (veya .NET 7 SDK)
- Azure Speech SDK anahtarı (ve bölge)
- YouTube Data API Key (isteğe bağlı, en yeni video özelliği için)
- Gmail SMTP için App Password veya başka SMTP bilgisi (dosya gönderme için)

Kurulum
1. Projeyi Visual Studio'da aç (veya yeni bir WPF .NET 7 projesi oluşturup yukarıdaki dosyaları ekle).
2. NuGet paketlerini yükle: 
   - Microsoft.CognitiveServices.Speech
   - MailKit
   - Newtonsoft.Json
3. Config.cs içindeki placeholder değerleri doldur:
   - AzureKey ve AzureRegion
   - YouTubeApiKey (YouTube en yeni video özelliği için)
   - SMTP ayarları ve FileToSendPath (dosya gönderme testi için kendi dosya yolunu kullan)

Çalıştırma
- Uygulamayı başlat, "Dinle" butonuna bas, konuş.
- Örnek komutlar:
  - "Chrome'u aç"
  - "Notepad aç"
  - "Vural Üzül en yeni videosunu aç"
  - "CMD aç"
  - "Dosya gönder"

Güvenlik notları
- Dosya gönderme sadece kendi dosyalarınız ve izin verdiğiniz içerik için kullanılmalı.
- Uzaktan erişim veya otomatik güncelleme ekleyeceksen güvenli kimlik doğrulama ve HTTPS zorunludur.
- Admin yetkileri gerektiren işlemler için uygulamayı UAC ile yükselt; sürekli SYSTEM olarak çalıştırma.

İleri adımlar (isteğe bağlı)
- Global hotkey ekleme (Ctrl+Alt+Space) ve uygulama tepsi simgesi.
- Komut doğrulama ve PIN/biometric onayı ekleyeyim mi? (kritik komutlar için).
- Daha gelişmiş NLU (Rasa, LUIS) ekleyerek esnek komut anlayışı.
- Local STT (whisper.cpp) ile bulutta veri gönderimini engelleme.
