using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using System.Xml;
using System.Runtime.CompilerServices;
using ReactiveUI;
using System.Reflection;

namespace Avalonia.Ribbon.Samples.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public void OnClickCommand(object parameter)
        {
            string paramString = "[NO CONTENT]";
            
            if (parameter != null)
            {
                if (parameter is string str)
                    paramString = str;
                else
                    paramString = parameter.ToString();
            }

            Console.WriteLine("OnClickCommand invoked: " + paramString);
            LastActionText = paramString;

            LetsTryXML();
        }

        public void LetsTryReflection()
        {

        }

        /// <summary>
        /// refer:: https://stackoverflow.com/questions/15234236/reflection-on-list-and-printing-values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Object"></param>

        void printReturnedProperties<T>(T Object)
        {
            PropertyInfo[] propertyInfos = null;
            propertyInfos = Object.GetType().GetProperties();

            foreach (var item in propertyInfos)
                Console.WriteLine(item.Name + ": " + item.GetValue(Object).ToString());
        }

        /// <summary>
        /// refer:- https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-xml-data-from-url#references
        /// </summary>
        public void LetsTryXML()
        {
            String URLString = "books.xml";
            XmlTextReader reader = new XmlTextReader(URLString);

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        Console.Write("<" + reader.Name);

                        while (reader.MoveToNextAttribute()) // Read the attributes.
                            Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                        Console.Write(">");
                        Console.WriteLine(">");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        Console.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        break;
                }
            }
        }

        public void OnClickCommand2(object parameter)
        {
            string paramString = "[NO CONTENT]";

            if (parameter != null)
            {
                if (parameter is string str)
                    paramString = str;
                else
                    paramString = parameter.ToString();
            }

            Console.WriteLine("OnClickCommand invoked: " + paramString);
            LastActionText = paramString;
        }

        string _lastActionText = "none";

        public event PropertyChangedEventHandler PropertyChanged;

        public string LastActionText
        {
            get => _lastActionText;
            set
            {
                _lastActionText = value;
                NotifyPropertyChanged();
            }
        }

        public void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string _help = "Help requested!";
        public void HelpCommand(object parameter)
        {
            Console.WriteLine("Glenn " + _help);
            LastActionText = _help;
        }
    }
}
