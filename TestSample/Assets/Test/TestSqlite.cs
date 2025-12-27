using SQLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using SQLite.Tests;
using System.IO;
using System.Threading.Tasks;

public class TestSqlite : MonoBehaviour
{
    string databaseName = "Customer.db";
    SQLiteAsyncConnection m_db;


    // Start is called before the first frame update
    void Start()
    {
        var databasePath = $"{Application.persistentDataPath}/{databaseName}";
        m_db = new SQLiteAsyncConnection(databasePath);
        //Main();
        CreateIntTable();
    }

    // 将整个表加载到字典中，键为Id，值为Player对象
    public async UniTask<Dictionary<int, Customer>> LoadPlayersAsDictionaryAsync()
    {
        
        var players = await m_db.Table<Customer>().ToListAsync();
        var dictionary = new Dictionary<int, Customer>();
        for (int i = 0; i < players.Count; i++)
        {
            dictionary[players[i].Id] = players[i];
        }
        return dictionary;
    }

    // 将整个表加载到字典中，键为Id，值为Player对象
    public async UniTask<Customer[]> LoadArrayAsync()
    {

        var players = await m_db.Table<Customer>().ToArrayAsync();
        
        return players;
    }


    // 将整个表加载到字典中，键为Id，值为Player对象
    public async UniTask<CustomerInt[]> LoadArrayIntAsync()
    {

        var players = await m_db.Table<CustomerInt>().ToArrayAsync();

        return players;
    }

    // 新增或者替换对象
    public async void  AddArrayIntAsync()
    {
        CustomerInt item = new CustomerInt();
        item.Id = 64;
        item.FirstName = "1234";
        await AddCustomerIntAsync(item);
    }


    public async void Test2()
    {
        var databasePath = $"{Application.persistentDataPath}/{databaseName}";
        var db = new SQLiteAsyncConnection(databasePath);
        await AddCustomerAsync(new Customer());
        var customer = await GetCustomerAsync(2);
    }
    public async void Main()
    {
       
        var databasePath = $"{Application.persistentDataPath}/{databaseName}";
        var db = new SQLiteAsyncConnection(databasePath);
        
        await db.CreateTableAsync<Customer>();
        //List<Customer> list = db.Table<Customer>().ToListAsync();
        for (int i = 0; i < 10; i++)
        {
            await AddCustomerAsync(new Customer());
        }
        
        var customer = await GetCustomerAsync(1);
    }

    public async void CreateIntTable()
    {

        var databasePath = $"{Application.persistentDataPath}/{databaseName}";
        var db = new SQLiteAsyncConnection(databasePath);

        await db.CreateTableAsync<CustomerInt>();
        //List<Customer> list = db.Table<Customer>().ToListAsync();
        //for (int i = 0; i < 10; i++)
        //{
        //    await AddCustomerIntAsync(new CustomerInt());
        //}

        //var customer = await GetCustomerAsync(1);
    }

    public async UniTask AddCustomerIntAsync(CustomerInt customer)
    {
        
        await m_db.UpsertAsync(customer);
    }

    public async UniTask AddCustomerAsync(Customer customer)
    {
        var databasePath = Application.persistentDataPath + "/" + databaseName;
        var db = new SQLiteAsyncConnection(databasePath);

        await db.InsertAsync(customer);
    }

    public async UniTask<Customer> GetCustomerAsync(int id)
    {
        var databasePath = Path.Combine(Application.persistentDataPath, databaseName);
        var db = new SQLiteAsyncConnection(databasePath);

        Customer customer = await db.GetAsync<Customer>(id);
        return customer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Main();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Test2();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadPlayersAsDictionaryAsyncTest();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadArrayAsyncTest();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddArrayIntAsync();
        }
    }

    public async void LoadPlayersAsDictionaryAsyncTest()
    {
        Dictionary<int, Customer> dic = await LoadPlayersAsDictionaryAsync();
        int i = 0;
        Debug.Log($"{dic.Count}");
    }

    public async void LoadArrayAsyncTest()
    {
        CustomerInt[] dic = await LoadArrayIntAsync();
        int i = 0;
        //Debug.Log($"{dic.Length}");
    }

    
}
