using System;
using System.Speech.Synthesis;   // ← This is the only new using
using System.Threading;

namespace CybersecurityAwarenessBot_Part1
{
    internal class Program
    {
        private static string userName = string.Empty; 

        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot - Part 1";

            // 1. Voice Greeting using DEFAULT SYSTEM VOICE
            PlayVoiceGreeting();

            // 2. Display ASCII Art Logo
            DisplayAsciiArt();

            // 3. Text-Based Greeting & Get Name
            userName = GetUserName();

            PrintColoredHeader($"Hello, {userName}! Welcome to the Cybersecurity Awareness Bot!");

            Console.WriteLine("I’m here to help you stay safe online.\n");
            ShowHelp();

            // 4. Main Chat Loop
            ChatbotLoop();
        }

        private static void PlayVoiceGreeting()
        {
            try 
            {
                using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
                {
                    synthesizer.SetOutputToDefaultAudioDevice();
                    synthesizer.Speak("Hello! Welcome to the Cybersecurity Awareness Bot. I’m here to help you stay safe online.");
                }
            }
            catch
            {
                Console.WriteLine("[INFO] Voice greeting could not play. Continuing without audio...\n");
            }
        }

        private static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
   ________      ___    ___ ________  _______   ________          ________  ________  _________   
|\   ____\    |\  \  /  /|\   __  \|\  ___ \ |\   __  \        |\   __  \|\   __  \|\___   ___\ 
\ \  \___|    \ \  \/  / | \  \|\ /\ \   __/|\ \  \|\  \       \ \  \|\ /\ \  \|\  \|___ \  \_| 
 \ \  \        \ \    / / \ \   __  \ \  \_|/_\ \   _  _\       \ \   __  \ \  \\\  \   \ \  \  
  \ \  \____    \/  /  /   \ \  \|\  \ \  \_|\ \ \  \\  \|       \ \  \|\  \ \  \\\  \   \ \  \ 
   \ \_______\__/  / /      \ \_______\ \_______\ \__\\ _\        \ \_______\ \_______\   \ \__\
    \|_______|\___/ /        \|_______|\|_______|\|__|\|__|        \|_______|\|_______|    \|__|
             \|___|/                                                                            
                                                 
            Cybersecurity Awareness Assistant
");
            Console.ResetColor();
            Console.WriteLine("".PadRight(70, '=') + "\n");
        }

        private static string GetUserName()
        {
            string name;
            do
            {
                PrintColored("What is your name? ", ConsoleColor.Yellow);
                name = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(name))
                    PrintColored("Please enter a valid name.\n", ConsoleColor.Red);
            } while (string.IsNullOrEmpty(name));

            return name;
        }

        private static void ChatbotLoop()
        {
            while (true)
            {
                PrintColored("\nYou: ", ConsoleColor.Green);
                string input = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (string.IsNullOrEmpty(input))
                {
                    PrintColored("I didn’t quite understand that. Could you rephrase?\n", ConsoleColor.Red);
                    continue;
                }

                if (input == "exit" || input == "bye" || input == "quit")
                {
                    PrintColored($"Goodbye, {userName}! Stay safe online! 👋\n", ConsoleColor.Magenta);
                    break;
                }

                string response = GetResponse(input);
                TypeWriterEffect(response, 20);
            }
        }

        private static string GetResponse(string input)
        {
            if (input.Contains("how are you") || input.Contains("how r u"))
                return $"I'm doing great, {userName}! Ready to help you stay cyber-safe today.";

            if (input.Contains("purpose") || input.Contains("who are you"))
                return "My purpose is to educate you about cybersecurity – phishing, passwords, safe browsing, and more!";

            if (input.Contains("password"))
                return "Use strong, unique passwords (at least 12 characters with numbers, symbols, and uppercase letters). Never reuse passwords!";

            if (input.Contains("phishing"))
                return "Phishing emails often look real. Never click suspicious links or give personal info. Check the sender carefully.";

            if (input.Contains("safe browsing") || input.Contains("browse"))
                return "Always look for the padlock 🔒 in the address bar. Avoid public Wi-Fi for banking.";

            if (input.Contains("what can i ask"))
                return "You can ask me about: passwords, phishing, safe browsing, how I am, my purpose, or say 'exit' to quit.";

            return "I didn’t quite understand that. Could you rephrase? Try asking about passwords, phishing, or safe browsing.";
        }

        private static void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("You can ask me things like:");
            Console.WriteLine("• How are you?");
            Console.WriteLine("• What’s your purpose?");
            Console.WriteLine("• Tell me about passwords / phishing / safe browsing");
            Console.WriteLine("• What can I ask you about?");
            Console.WriteLine("Type 'exit' or 'bye' to quit.");
            Console.ResetColor();
            Console.WriteLine("".PadRight(70, '-') + "\n");
        }

        private static void TypeWriterEffect(string text, int delayMs = 20)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        private static void PrintColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        private static void PrintColoredHeader(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("".PadRight(70, '='));
            Console.WriteLine($"  {text}");
            Console.WriteLine("".PadRight(70, '='));
            Console.ResetColor();
        }
    }
}