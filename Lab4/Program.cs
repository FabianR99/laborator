using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Threading.Tasks;

namespace L04
{
    class Program
    {
        const string StorageAccountName = "edward20";
        const string StorageAccountKey = "eAAAMZGanfivVzNc8/YrZ0uUIh3NJ1U6ixeYD6XqVZxRH3gTgaxAnDHj/qC7Hb+z+YYLsb04ZX6nm7PkC6HGhg==";
        public static async Task<string> InsertTableEntity(CloudTable p_tbl)
        {
            Console.WriteLine("Numar matricol student:");
            int codMatricol = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Nume student:");
            string nume = Console.ReadLine();
            Console.WriteLine("Prenume student:");
            string prenume = Console.ReadLine();
            Console.WriteLine("Medie student:");
            int medie = Convert.ToInt32(Console.ReadLine());
            StudentEntity entity = new StudentEntity(codMatricol, nume, prenume, medie);
            TableOperation insertOperation = TableOperation.InsertOrMerge(entity);
            TableResult result = await p_tbl.ExecuteAsync(insertOperation);
            Console.WriteLine("Student Added");
            return "Student added";
        }
        static void Main(string[] args)
        {
            try
            {
                var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), false);
                var table = storageAccount.CreateCloudTableClient().GetTableReference("temaLab4");
                var insertResult = InsertTableEntity(table);
                if (insertResult == null)
                {
                    Console.WriteLine("Something went wrong");
                }
                else
                {
                    Console.WriteLine("All good!");
                }
                var result = table.ExecuteAsync(TableOperation.Retrieve<StudentEntity>("<partition-key>", "<row-key>"));
                var entity = result.Result;
                Console.WriteLine(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
       
    }

    class StudentEntity : TableEntity
    {
        private int codMatricol;
        private string nume;
        private string prenume;
        private int medie;
        public StudentEntity(int cod,string num,string prenum,int med)
        {
            codMatricol = cod;
            nume = num;
            prenume = prenum;
            medie = med;
        }
       
    }
   
}
