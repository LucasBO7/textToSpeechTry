using Microsoft.CognitiveServices.Speech;

namespace TextoPraFalaTeste.Repositories
{
    public class Talk
    {
        // https://learn.microsoft.com/en-us/azure/ai-services/speech-service/get-started-text-to-speech?tabs=windows%2Cterminal&pivots=programming-language-csharp

        static string speechKey = "d894f46895f749d5b79c9b185b586098";
        static string speechRegion = "eastus";

        public static string OutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
        {
            switch (speechSynthesisResult.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    // Console.WriteLine($"Speech synthesized for text: [{text}]");
                    return "Texto reconhecido com sucesso!";
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        return "Falha no reconhecimento da voz: " + cancellation.Reason;
                    }
                    break;
                default:
                    break;
            }

            return "Áudio não reconhecido.";
        }


        public async Task<string> SpeechText(string textToSpeech, string voiceAgentName)
        {
            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            string audioResult;

            // The neural multilingual voice can speak different languages based on the input text.
            // speechConfig.SpeechSynthesisVoiceName = "en-US-AvaMultilingualNeural";
            //ThalitaNeural
            //AntonioNeural
            speechConfig.SpeechSynthesisVoiceName = $"pt-BR-{voiceAgentName}";

            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                // Get text from the console and synthesize to the default speaker.
                // Console.WriteLine("Enter some text that you want to speak >");
                string text = textToSpeech;

                var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
                audioResult = OutputSpeechSynthesisResult(speechSynthesisResult, text);
                return audioResult;
            }
        }

    }
}
