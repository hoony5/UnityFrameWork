using System;
using Share;
using UnityEngine;
using UnityEngine.UIElements;

namespace Writer.CommandConsole
{
    public class CommandConsole : MonoBehaviour, IDisposable
    {
        public static CommandConsole Instance { get; private set; }
        
        private CommandLineExecutor commandLineExecutor;
        private ConsoleView consoleView;
        private UIDocument uiDocument;
        
        private IVisualElementScheduledItem autoCompletionTask;
        public CommandLineType ConsoleLogType { get; set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                Application.logMessageReceived += OnLogMessageReceived;
                return;
            }

            DestroyImmediate(this);
        }

        private void Start()
        {
            // setup Attributes
            commandLineExecutor = new CommandLineExecutor();
            consoleView = new ConsoleView();
            
            // setup UI
            // uiDocument = GetComponent<UIDocument>();
            // AddToDocument();
            // Bind();
        }

        // TODO :: DI Pattern
        private void AddToDocument()
        {
            if (uiDocument == null)
            {
                Debug.LogWarning("UIDocument is not found.");
                return;
            }
            uiDocument.rootVisualElement.Add(consoleView.element);
        }
        
        private CommandLineType GetCommandLineType(LogType type)
        {
            switch (type)
            {
                default:
                case LogType.Assert:
                case LogType.Log:
                    return CommandLineType.Info;
                case LogType.Error:
                case LogType.Exception:
                    return CommandLineType.Error;
                case LogType.Warning:
                    return CommandLineType.Warning;
            }
        }

        private bool IsOk(CommandLineType type)
        {
            return type == ConsoleLogType;
        }

        private void OnLogMessageReceived(string condition, string stacktrace, LogType type)
        {
            CommandLineElement commandLine = Log(condition, GetCommandLineType(type));
            
            if(commandLine == default) return;
            commandLine.element.userData = new CommandLineUserData(typeof(string), condition);
        }
        
        private CommandLineElement Log(string message, CommandLineType type = CommandLineType.User)
        {
            if(!IsOk(type)) return default;
            
            CommandLineElement line = new CommandLineElement(type).SetHeader($"> [{message}] > [{DateTime.Now}]");
            consoleView.SendToLog(line);
            return line;
        }
        public string[] ExecuteAutoCompletion(string input)
        {
            return CommandLineAutoCompletion.GetAutoCompletionOptions(input);
        }
        public CommandLineElement ExecuteCommand<T>(T instance, string input, CommandLineType type = CommandLineType.User)
        {
            // usage ( specific instance, line, return result )
            if (!CommandLineExecutor.TryExecuteCommand(instance, input, out object result))
            {
                Debug.LogError($"Command {input} is not found in {typeof(T).Name}.");
                return default;
            }

            CommandLineElement line = Log(input, type);

            if (line == default) 
                return default;
            
            if(result != null)
                line.element.userData = new CommandLineUserData(result.GetType(), result);
            
            return line;
        }

        private void SetAutoCompletions()
        {
            string input = consoleView.contentInputField.element.GetValue<TextField, string>();
            
            if (input.IsNullOrEmpty()) return;
            consoleView.SetAutoCompleteView(ExecuteAutoCompletion(input));
        }

        private void Bind()
        {
            BindAutoCompletion();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            Application.logMessageReceived -= OnLogMessageReceived;
            DisposeAutoCompletion();
            commandLineExecutor.Clear();
            consoleView.Clear();
        }
        private void BindAutoCompletion()
        {
            autoCompletionTask = consoleView.contentInputField.element
                .schedule.Execute(SetAutoCompletions).Every(500);
        }
        private void DisposeAutoCompletion()
        {
            autoCompletionTask?.Pause();
            autoCompletionTask = null;
        }
        
        /*
        public void TestAutoComplete()
        {
            string input = "help.";
            Debug.Log(ExecuteAutoCompletion(input).Join("\n\n"));
        }
        public void TestExecuteCommand()
        {
            commandLineExecutor = new CommandLineExecutor(); // init should be called before execute command
            TestInstance instance = new TestInstance();
            ExecuteCommand(instance, "help.TestMethod.(123)");
        }*/
    }
}