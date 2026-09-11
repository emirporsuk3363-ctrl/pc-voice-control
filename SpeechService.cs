using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace VoiceControlMVP
{
    // Basit: RecognizeOnceAsync kullanır. Azure anahtarı/region Config.cs içinde.
    public class SpeechService
    {
        public async Task<string?> RecognizeOnceAsync()
        {
            var config = SpeechConfig.FromSubscription(Config.AzureKey, Config.AzureRegion);
            config.SpeechRecognitionLanguage = "tr-TR"; // Türkçe
            using var recognizer = new SpeechRecognizer(config);
            var result = await recognizer.RecognizeOnceAsync();
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                return result.Text;
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                return null;
            }
            else
            {
                throw new Exception($"Speech error: {result.Reason}");
            }
        }
    }
}
