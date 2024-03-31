using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AYellowpaper.SerializedCollections;
using Share;
using UnityEngine;
using Writer.Ex;

namespace Writer.CommandConsole
{
    public class CommandLineExecutor
    {
        private const string commandSplitter = ".";
        private static List<CommandTargetAttribute> commandTargets 
            = new List<CommandTargetAttribute>();
        
        private static SerializedDictionary<string, SerializedDictionary<string, CommandMethodInfo>> commandMethods 
            = new SerializedDictionary<string, SerializedDictionary<string, CommandMethodInfo>>();

        public CommandLineExecutor()
        {
            IEnumerable<CommandTargetAttribute> targets = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Select(x => CustomAttributeExtensions.GetCustomAttribute<CommandTargetAttribute>((MemberInfo)x))
                .Where(x => x != null);
        
            foreach (CommandTargetAttribute target in targets)
            {
                AddTarget(target);
            }
            SetUp();
        }

        private static void AddTarget(CommandTargetAttribute targetAttribute)
        {
            if (commandTargets.Contains(targetAttribute)) return;
            commandTargets.Add(targetAttribute);
        }

        public void Clear()
        {
            commandTargets.Clear();
            commandMethods.Clear();
            CommandLineAutoCompletion.Clear();
        }

        private static void SetUp()
        {
            if(commandTargets.Count == 0) return;
            
            foreach (CommandTargetAttribute target in commandTargets)
            {
                MethodInfo[] methods = target.type.GetMethods();
                if(methods.Length == 0) return;
                
                if (target.containsAll)
                {
                    SetUpAllMethods(target.tag, methods);
                    continue;
                }
                SetUpMethods(methods);
            }
        }

        private static void SetUpAllMethods(string tag, MethodInfo[] methods)
        {
            foreach (MethodInfo methodInfo in methods)
            {
                if (!IsValidMethod(methodInfo.Name)) continue;
                
                (string methodName, string commandFormat, string[] parameterTypeNames) 
                    = AnalyzeCommandFormat(tag, methodInfo);
                
                if(methodName.IsNullOrEmpty()) continue;
                
                if (!commandMethods.ContainsKey(tag))
                {
                    commandMethods.Add(tag, new SerializedDictionary<string, CommandMethodInfo>());
                }
                
                if (commandMethods[tag].ContainsKey(methodName)) continue;
                
                CommandLineAutoCompletion.AddCommand(tag, new CommandFormatInfo()
                {
                    commandFormat = commandFormat
                });

                CommandLineDescriptionAttribute additionalInfo
                    = methodInfo.GetCustomAttribute<CommandLineDescriptionAttribute>();
                    
                string description = $"{additionalInfo?.description}".IsNullOrEmptyThen("No description");
             
                commandMethods[tag].Add(
                    methodName,
                    new CommandMethodInfo
                    {
                        tag = tag,
                        command = commandFormat,
                        parameterTypeNames = parameterTypeNames,
                        methodInfo = methodInfo,
                        description = description
                    });
            }
        }

        private static bool IsValidMethod(string methodName)
        {
            return methodName != "GetHashCode" &&
                   methodName != "Equals" &&
                   methodName != "ToString" &&
                   methodName != "GetType";
        } 
        private static (string methodName, string commandFormat, string[] parameterTypeNames) AnalyzeCommandFormat(string tag, MethodInfo methodInfo)
        {
            string methodName = methodInfo.Name;

            string[] parameterTypeNames = methodInfo.GetParameters().Select(x => x.ParameterType.Name).ToArray();
            string parameterFormat = AnalyzeCommandParametersFormat(parameterTypeNames);
            return (methodName, $"{tag}.{methodName}{parameterFormat}", parameterTypeNames);
        }
        private static void SetUpMethods(MethodInfo[] methods)
        {
            foreach (MethodInfo method in methods)
            {
                if (!IsValidMethod(method.Name)) continue;
                
                CommandLineAttribute commandInfo = method.GetCustomAttribute<CommandLineAttribute>();
                if (commandInfo == null) continue;
                
                (string tag, string methodName, string commandFormat, string[] parameterTypeNames) 
                    = AnalyzeCommandFormat(commandInfo);
                
                if (!commandMethods.ContainsKey(tag))
                {
                    commandMethods.Add(tag, new SerializedDictionary<string, CommandMethodInfo>());
                }
                
                if (commandMethods[tag].ContainsKey(methodName)) continue;
                
                CommandLineAutoCompletion.AddCommand(tag, new CommandFormatInfo()
                {
                    commandFormat = commandFormat
                });
                
                CommandLineDescriptionAttribute additionalInfo 
                    = method.GetCustomAttribute<CommandLineDescriptionAttribute>();
                
                string description = $"{additionalInfo?.description}".IsNullOrEmptyThen("No description");

                commandMethods[tag].Add(
                    methodName,
                    new CommandMethodInfo
                    {
                        tag = commandInfo.tag,
                        command = commandInfo.commandFormat,
                        parameterTypeNames = parameterTypeNames,
                        methodInfo = method,
                        description = description
                    });
            }
        }

        private static string AnalyzeCommandParametersFormat(string[] parameterTypeNames)
        {
            int parameterLength = parameterTypeNames.Length;
            string parameterFormat = parameterLength switch
            {
                0 => "",
                1 => $".({parameterTypeNames[0]})",
                _ => $".({(", ").Join(parameterTypeNames)})"
            }; 
            return parameterFormat;
        }
        private static (string tag, string methodName, string commmandFormat, string[] parameterTypeNames) AnalyzeCommandFormat(
            CommandLineAttribute commandInfo)
        {
            string tag = commandInfo.tag;
            int methodIndex = 1;
            string methodName = commandInfo.commandFormat.Split('.')[methodIndex];
            int parameterLength = commandInfo.parameterTypeNames.Length;
            string parameterFormat = AnalyzeCommandParametersFormat(commandInfo.parameterTypeNames);
                
            string commandFormat = $"{commandInfo.commandFormat}{parameterFormat}";
            if (parameterLength < 1) return (tag, methodName, commandFormat, null);
            
            string[] parameterTypeNames = commandInfo.parameterTypeNames;
            return (tag, methodName, commandFormat, parameterTypeNames);
        }

        private static (string tag, string commnad, string[] parameterValues) DeconstructInputCommand(string commandInput)
        {
            string[] command = commandInput.Split(commandSplitter);
            string tag = command[0];
            string methodName = command[1];
            if(command.Length == 2) return (tag, methodName, null);
            
            string[] parameterValues = command.Skip(2).ToArray();
            
            return (tag, methodName, parameterValues);
        }
        // input format help.test.(1,2,3,4,5,6)
        private static (string tag, string commnad, object[] parameters) AnalyzeInputCommand(string commandInput)
        {
            (string tag, string methodName, string[] parameterValues) = DeconstructInputCommand(commandInput);

            bool hasTagName = commandMethods.ContainsKey(tag);
            bool hasMethodName = commandMethods[tag].ContainsKey(methodName);
            if(!hasTagName || !hasMethodName)
            {
                Debug.LogError($"{(!hasTagName ? $"Tag : {tag} is not found." : $"Command : {methodName} is not found.")}");
                return (null, null, null);
            }
            
            string[] parameterTypeNames = commandMethods[tag][methodName].parameterTypeNames;
            object[] results = new object[parameterTypeNames.Length];
            for (var index = 0; index < parameterTypeNames.Length; index++)
            {
                string parameterTypeName = parameterTypeNames[index];
                Type type = parameterTypeName.IsBasicType() 
                    ? parameterTypeName.GetBasicType() 
                    : Type.GetType(parameterTypeName, false, true);
                
                if (type == null)
                {
                    Debug.LogError($"Type {parameterTypeName} not found");
                    return (null, null, null);
                }

                results[index] = type.Parse(parameterValues[index]);
            }

            return (tag, methodName, results);
        }
        
        public static bool TryExecuteCommand<T>(T instance, string command, out object result)
        {
            result = null;
            
            (string tag, string methodName, object[] parameters) = AnalyzeInputCommand(command);
            
            CommandMethodInfo methodInfo = commandMethods[tag][methodName];
            if (methodInfo.parameterTypeNames.Length != parameters.Length)
            {
                Debug.LogError($"Parameter count is not matched. {methodInfo.parameterTypeNames.Length} != {parameters.Length}");
                return false;
            }
            result = methodInfo.methodInfo.Invoke(instance, parameters);
            return true;
        }
    }
}