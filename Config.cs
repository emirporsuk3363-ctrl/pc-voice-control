namespace VoiceControlMVP
{
    public static class Config
    {
        // Azure Speech - doldur
        public static string AzureKey = "<AZURE_SPEECH_KEY>";
        public static string AzureRegion = "<AZURE_REGION>";

        // YouTube Data API v3 key - doldur
        public static string YouTubeApiKey = "<YOUTUBE_API_KEY>";

        // SMTP (MailKit) - doldur (örnek Gmail: smtp.gmail.com, port 587, SSL true/false)
        public static string SmtpServer = "smtp.gmail.com";
        public static int SmtpPort = 587;
        public static bool SmtpUseSsl = true;
        public static string SmtpUsername = "<youremail@gmail.com>";
        public static string SmtpPassword = "<app_password_or_smtp_password>";
        public static string SmtpRecipient = "<recipient@example.com>";

        // Gönderilecek dosyanın yolu (demo için kendi yolunu ayarla)
        public static string FileToSendPath = @"C:\Path\to\original_valorant_file.exe";
    }
}
