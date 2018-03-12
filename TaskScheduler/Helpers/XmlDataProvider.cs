using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using TaskScheduler.Model;

namespace TaskScheduler
{
    static class XmlDataProvider
    {
        private const string DATA_FILE_PATH = @"Data.xml";
        private const string ROOT_ELEMENT = "Tasks";
        private const string XML_ELEMENT = "task";
        private const string XML_ATTRIBUTE_ID = "id";
        private const string XML_ELEMENT_TASK_NAME = "taskName";
        private const string XML_ELEMENT_STATUS = "status";
        private const string XML_ELEMENT_TRIGGER = "trigger";
        private const string XML_ELEMENT_EXECUTABLE_PATH = "executablePath";
        private const string XML_ELEMENT_DESCRIPTION = "description";

        private static XDocument xmlDocument;

        static XmlDataProvider()
        {
            if(!File.Exists(DATA_FILE_PATH)) { xmlDocument = new XDocument(new XElement(ROOT_ELEMENT)); }
            else { xmlDocument = XDocument.Load(DATA_FILE_PATH); }
        }

        public static List<TaskItem> getDataFromFile()
        {
            List<TaskItem> tasks = new List<TaskItem>();
            TaskItem buff;
            foreach (XElement task in xmlDocument.Root.Elements())
            {
                buff = new TaskItem();
                buff.ID = Convert.ToInt32(task.Attribute(XML_ATTRIBUTE_ID).Value);

                foreach(XElement el in task.Elements())
                {
                    switch(el.Name.ToString())
                    {
                        case XML_ELEMENT_TASK_NAME:
                            buff.TaskName = el.Value;
                            break;
                        case XML_ELEMENT_STATUS:
                            buff.Status = (status)Enum.Parse(typeof(status), el.Value);
                            break;
                        case XML_ELEMENT_TRIGGER:
                            buff.Trigger = Convert.ToDateTime(el.Value);
                            break;
                        case XML_ELEMENT_EXECUTABLE_PATH:
                            buff.ExecutablePath = el.Value;
                            break;
                        case XML_ELEMENT_DESCRIPTION:
                            buff.Description = el.Value;
                            break;
                    }
                }

                tasks.Add(buff);
            }
            return tasks;
        }

        public static void addTaskToData(TaskItem task) =>
            xmlDocument.Root.Add(new XElement(XML_ELEMENT, new XAttribute(XML_ATTRIBUTE_ID, task.ID),
                new XElement(XML_ELEMENT_TASK_NAME, task.TaskName),
                new XElement(XML_ELEMENT_STATUS, task.Status),
                new XElement(XML_ELEMENT_TRIGGER, task.Trigger),
                new XElement(XML_ELEMENT_EXECUTABLE_PATH, task.ExecutablePath),
                new XElement(XML_ELEMENT_DESCRIPTION, task.Description)));

        public static void redactTaskInData(TaskItem task)
        {
            //redact item in xml
        }

        public static void deleteTaskInData(int id)
        {
            //delete element in xml
        }
    }
}