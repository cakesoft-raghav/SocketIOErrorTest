using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocketIOClientApp
{
    delegate void FileReadEventHandler(DummyData dummyData);
    class DummyTask
    {
        public event FileReadEventHandler FileReadEvent;

        public void doTask()
        {
            Console.WriteLine("Doing task");
            for (int i = 0; i < 20; i++)
            {
                DummyData data = ReadFile();
                data.id = i;
                OnFileRead(data);
            }
        }

        private DummyData ReadFile()
        {
            string fpath = "file.jpg";
            string fdata = Convert.ToBase64String(File.ReadAllBytes(fpath));
            DummyData data = new()
            {
                htmlData = fdata.Substring(0,99000),
            };
            return data;
        }

        protected virtual void OnFileRead(DummyData dummyData)
        {
            FileReadEvent?.Invoke(dummyData);
        }
    }
}
