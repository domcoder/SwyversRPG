namespace SwyversRPG.Engine
{
    public class GameFrontend
    {
        private readonly GameBackend backend;

        public GameFrontend(GameBackend backend)
        {
            this.backend = backend;
        }

        public void Run()
        {
            ShowMainMenu();
            while (true)
            {
                DisplayCurrentState();
                string input = GetUserInput();
                HandleCommand(input);
            }
        }

        private void ShowMainMenu() { /* Start/load game */ }

        private void DisplayCurrentState()
        {
            Console.WriteLine(backend.GetLocationDescription());
            Console.WriteLine("Available actions:");
            foreach (var action in backend.GetAvailableActions())
                Console.WriteLine($"- {action}");
        }

        private string GetUserInput()
        {
            Console.Write("> ");
            return Console.ReadLine()?.Trim().ToLower() ?? "";
        }

        private void HandleCommand(string command)
        {
            string result = backend.ProcessCommand(command);
            Console.WriteLine(result);
        }
    }
}