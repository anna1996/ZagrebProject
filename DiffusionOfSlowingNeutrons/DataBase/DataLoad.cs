using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class DataLoad
    {

        public static List<RootObject> data = null;

        public static void LoadDataFromJson()
        {
            using (StreamReader r = new StreamReader("database.json"))
            {
                string json = r.ReadToEnd();
     
                 data = JsonConvert.DeserializeObject<List<RootObject>>(json);

                /*
                foreach (var item in items)
                {
                    Console.Write(item.Answers[0].Text);
                }

                Console.Read();
                */
            }
        }

        public class Answer
        {
            public string Text { get; set; }
            public int isRight { get; set; }
        }

        public class RootObject
        {
            public string TestType { get; set; }
            public string Theme { get; set; }
            public string Question { get; set; }
            public string Complexity { get; set; }
            public List<Answer> Answers { get; set; }
        }


    }
    
