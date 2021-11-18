using System;
using System.Configuration; 
using System.Threading.Tasks; 
using Azure.Identity;
using Azure.Storage.Queues; 
using Azure.Storage.Queues.Models;

public class PublishMessagesAsyncSample
{
    
    public void CreateQueueClient(string queueName)
    {
        string connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

        QueueClient queueClient = new QueueClient(connectionString, queueName);
    }
    public static bool CreateQueue(string queueName)
    {
        try
        {
            string connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

            QueueClient queueClient = new QueueClient(connectionString, queueName);

            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                Console.WriteLine($"Queue created: '{queueClient.Name}'");
                return true;
            }
            else
            {
                Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}\n\n");
            Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
            return false;
        }
    }
    public void InsertMessage(string queueName, string message)
    {
        string connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

        QueueClient queueClient = new QueueClient(connectionString, queueName);

        queueClient.CreateIfNotExists();

        if (queueClient.Exists())
        {
            queueClient.SendMessage(message);
        }

        Console.WriteLine($"Inserted: {message}");
    }
    static void Main(string[] args)
    {
        try
        {
            bool createQueueResult = CreateQueue("incmoing-message");


            if (createQueueResult == true)
            {
                Console.WriteLine("Something went wrong");
            }
            else
            {
                Console.WriteLine("All good!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}