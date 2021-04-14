# DotNetCore+Oracle
Getting started with .Net Core and Oracle.

We will see how to mix and match .Net Core with Oracle databases and Google client side technology Angular with a hands-on approach, quick and easy to follow. How to connect, which tools to have and how to begin development.
Setup
First you need a basic understanding about many programming technologies like C#, and SQL, but also about ASP Net, and Oracle itself. Luckily, there are many good resources about those on the web, if you need an introduction.
Then you need Visual Studio 2019 (I used VS Community 2019 – 16.6.0) with both “ASP .Net and Web Development” and “.NET Core Development” workloads checked in your installation (you can control this during the install process but also after VS is installed; run the VS Installer, it’s very clear, especially if you do not find something reading the article).
We will use the Oracle HR sample database so I suppose you already have access to it. But here are all the possible cases:
1.	as I just said, if you can connect to the HR schema/database already, then jump to the [Check the Oracle connection] section of this article
2.	if you can connect to an Oracle server but not to the HR schema/database: then you either need to create or unlock that schema ([Unlock or Create the Schema] section) or run the object creation script inside your actual schema (go to the [References-Github] section, but later remember to replace your schema name anywhere I eventually used the schema name “HR”, if you have a different schema name)
3.	if you cannot connect to any Oracle server but it’s OK for you to install one, go to the [XE] section. PRO: Oracle XE is a complete and free database server solution. CON: it’s BIG and your computer may suffer. So use it if you are going to be serious about Oracle, not just for a single article
4.	if you can neither connect to Oracle nor install XE: you need a Cloud database! Here the good news is that Oracle has a beautiful free offer and the bad news is that it’s a bit more complex to connect to it, so I wrote an entire article about this. Read the [Cloud] section to know more.
XE
Oracle XE, or Oracle Express Edition, is currently an Oracle 18c server with just a few limitations, free and very good for testing, studying or for development purposes. According to an official Oracle blog XE has the limit of 2 CPUs and 2GB of RAM (respectively), and now supports up to 12GB of user data [but it] includes key database features such as; pluggable databases, in-memory column store, compression, spatial & graph support, encryption and redaction, partitioning,  analytic views, and more.
Oracle is probably the most difficult database to install and maintain, because of its countless features, so this makes XE a good choice only if you are planning to spend time on it, not just for giving it a try.
It’s free to install and use for many different situations. You find more on it in [References-XE] together with a Quick Start document you find in [References-QuickStart].
Cloud
If you don’t want to go through the process of installing and running an Oracle server, you can leave this burden to Oracle and go for a cloud service. Using an Oracle database in the cloud is a new and exciting opportunity because it’s quick, and because it won’t touch your computer, still allowing huge possibilities of study/development. But it’s slightly more difficult to connect to, however, being on the public Internet and so needing higher protection compared to a LAN hosted server. Last but not least, like XE, there’s a free plan we can use to follow this article.
I wrote an entire article to explain this. You find it in [References-Cloud]. After you read it, come back here to follow the article again.
Unlock or Create the Schema
The Oracle HR schema is installed by default in Oracle XE, but it’s locked, so you have to unlock it. To do it open a command prompt window (in Windows) or a terminal window (in Linux) and type:
sqlplus / as sysdba
After that, run the command:
alter user hr identified by hr account unlock;
If the user does not exist you can create it and use it with your database. Run the command below to do it:
create user hr identified by hr;
Now, grant permissions to hr by running the script below:
GRANT CREATE SESSION, CREATE TABLE, UNLIMITED TABLESPACE TO HR;
And now, but only if you had to create the schema, you need to run the object creation script you find later.
Check the Oracle connection
I suppose you want to connect to Oracle, now, so install the Oracle SQL Developer tool, a powerful free database development tool. It’s also super easy: you download the release with “JDK included” and you just need to unzip the file and run the sqldeveloper.exe from the folder you have just chosen [References-SQLDeveloper].
Now, in SQL Developer, locate the “Connections” window and press the green plus button to create a “New Database Connection”.

Insert “hr” in Name, Username, and Password. Then check Save Password and insert the IP of your Oracle server along with 1521 in Hostname and Port; “XEPDB1” is the Service name of a newly installed XE. Now press Test and you should be able to connect to Oracle. Save the connection and if you can connect from SQL Developer, then you can also from any other software. So be sure to have no connection errors, tough, to follow the article from this point forward. If you have, double check the data you entered in the connection window and then go to the Oracle site [References-18cHelpCenter].
Creation Script
You do not need to run the creations scripts in the [References-Github] companion project, if you already see the tables in your database, after the connection.
So run it only if you have no tables in it, use a new Sql Worksheet (AlT-F10), load it and run it (F5). After the script completes use the current worksheet to see if you can query your data. Clear it and type:
SELECT * FROM employees
Now press CTRL+RETURN: you should see the data.
Let’s start
Fire up VS 2019 and select Create New Project. Now select ASP .NET Core Web Application give it a meaningful name like OracleHR and press Create. Choose the Angular template and leave Configure for HTTPS checked. Press Create again and let VS do the work.
When VS has finished scaffolding it’s time to check if everything is in place because we need to check our project before we go on. Press CTRL+SHIFT+B and build the project. The process should take some time because it has to download a few components from the web. If you see errors (not uncommon), first try to build it again.
The VS Installer may not install Node by default; if, at some point, VS complains that Node is not installed use the last or the LTS release; it makes no difference, at time of writing. See instructions in [References-InstallNode].
And quite probably this happens to Angular too, but you install it from within the Node Package Manager (NPM) tool. To start it, press the Windows button and type “Node”, then launch the Node JS command prompt. Inside it type the following command and presse ENTER:
npm install -g @angular/cli
Follow any on-screen instructions; at the end you probably want to restart your pc even if they didn’t ask you to do so.
The server
Now your project should build and then start correctly; go back to VS, open your solution, press F5 and let’s see it in action. You should see a web page somewhat like the one in the next figure (it may look different as it changes from release to release):

Now click on the Fetch data link which brings you to a new page with a table filled with (fake) data, not really fetched somewhere, but simply hard-coded in the source code. We are going to fetch it from our Oracle database server soon!
But as it would be the case for any other database, we need software to access it. So stop VS, right-click on your solution and select Manage NU Get Packages for solution. Click on Browse and search and install latest versions of the following two components:
1.	Oracle.ManagedDataAccess.Core
2.	Dapper
Now build or run your app to check that everything is still fine and then, under your project, you should find your components enlisted under the Dependencies/Packages folders.
Let’s now create the model which shapes the data for our app. Right-click on the OracleHR project and select Add. Add a new folder called “Model” and inside it add a new class file called Employee.cs. Add to the class the following properties, which have to match closely both the name and the type of the database fields of the table EMPLOYEES we have already seen in SQL Developer:
public int employee_id { get; set; }
public string first_name { get; set; }
public string last_name { get; set; }
public string email { get; set; }
public string phone_number { get; set; }
public DateTime hire_date { get; set; }
public string job_id { get; set; }
public float? salary { get; set; }
public float? commission_pct { get; set; }
public float? manager_id { get; set; }
public float? department_id { get; set; }

Now open the Controllers folder and then the WeatherForecastController.cs file. Remove the Get method of the class and use this instead:
[HttpGet]
public IEnumerable<Employee> Get()
{
    string oradb = _config.GetValue<string>("ORACLE");
    try
    {
        using (var conn = new OracleConnection(oradb))
        {
            conn.Open();
            var recs = conn.Query<Employee>($@"SELECT * from EMPLOYEES");
            return recs;
        }
    }
    catch (Exception e)
    {
        string error = e.Message;
    }
    return null;
}

You should find errors, at this point. Let VS help you fixing the missing using statements, or just copy and paste these:
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using OracleHR.Model;

One last stop at the configuration file, before we finish the server part. Open the appsettings.json file and add the connection string of your Oracle database at the very end of the file, remembering to add a comma at the end of the line before:

"ORACLE": "DATA SOURCE=<YOUR SERVER ADDRESS>:1521/XEPDB1;PASSWORD=hr;PERSIST SECURITY INFO=True;USER ID=hr"

The server part is now ready. Build your project and it should build fine. You can already run it to see the raw data in json format. Let’s do it. Put a breakppoint at the first line of the public IEnumerable<Employee> Get() in the WeatherForecastController class. Now press F5 and wait for the browser to show up; add the controller name at the end of the current address, in the address bar, like this and press RETURN:
<actual address>/weatherforecast
The breakpoint should now hit, and you can follow the code pressing F10, until it returns the data (you may have to fix any error before going on; typically: passwords, grants and so on). Now we see the data in your browser, in RAW (json) format. Good!
Let’s format the data using a real web page.
The client
At the time of writing this article VS 2019 creates a single project for two very different things (but, as we know, it may change over time):
1.	(server) a platform independent .NET Core server app to provide data, by means of WEB API Rest services
2.	(client) a platform independent Angular application to consume that data, using an HTML5 web browser (but it could have been another kind of client, like an Android or iOS app, a computer program or any other)
The server part is that we have already seen so far, while the code of the client application is already present in the ClientApp folder. In a large application you may likely want to separate the two projects, but for the sake of simplicity we will leave it as it is (and also to honor the “full stack developer” myth, still alive and kicking, in the software industry, even if server developers and UI developers are always people with very different skillsets).
Now a couple of words about Angular2+ (the first Angular is a completely different product; and this is quite misleading). Angular uses a Component based approach which is no way similar to neither MVC nor MVVM. Here we have a model to hold the data and a class which exposes methods to work with that data. This class is injected into the HTML template because Angular supports Dependency Injection out of the box. I know, it’s a bit complicated. Moreover, explaining Angular if far from the scope of this article, but let’s briefly explain how it works.
In our template there’s a “src”, then an “app” and finally a “fetch-data” folder. Here, you find two files. Let’s begin from the fetch-data.component.ts. Again, there is a Model and it is that near the keyword “interface” at the end. Clearly we need to replace the template fields with new ones according to our HR Oracle database table “Employees”. Like this:
Employee_id: number;
First_name: string;
Last_name: string;
Email: string;
Phone_number: string;
Hire_date: Date;
Job_id: string;
Salary: number;
Commission_pct: number;
Manager_id: number;
Department_id: number;

Did I say “quick and easy” in the abstract? You may have noticed that I left the interface name to a misleading WeatherForecast, is it true? Well, we will fix names appropriately, later on.
Let’s now change the HTML template which is in charge to format our data. First we fix the table header, using appropriate column names:
<thead>
  <tr>
    <th>Employee id</th>
    <th>First name</th>
    <th>Last name</th>
    <th>Email</th>
    <th>Phone number</th>
    <th>Hire date</th>
    <th>Job id</th>
    <th>Salary</th>
    <th>Commission pct</th>
    <th>Manager id</th>
    <th>Department id</th>
  </tr>
</thead>

Then we fix the field definitions:
<tr *ngFor="let forecast of forecasts">
  <td>{{ forecast.Employee_id }}</td>
  <td>{{ forecast.First_name  }}</td>
  <td>{{ forecast.Last_name }}</td>
  <td>{{ forecast.Emailn }}</td>
  <td>{{ forecast.Phone_number }}</td>
  <td>{{ forecast.Hire_date }}</td>
  <td>{{ forecast.Job_id }}</td>
  <td>{{ forecast.Salary }}</td>
  <td>{{ forecast.Commission_pct }}</td>
  <td>{{ forecast.Manager_id }}</td>
  <td>{{ forecast.Department_id }}</td>
</tr>

Now press F5, click on the Fetch data link, wait and then look at the results:

Whatever Armstrong may have said, “that's one small step for a man but one giant leap for mankind” :).
Fix now your code and rename those “forecasts” with what you find appropriate. Notice that you can also start modifying the HTML template without having to stop VS: you save your file and see the “Live reload” feature of the Angular CLI: the browser refreshes automatically, a huge time-saver.
Conclusion
This was a very quick introduction to a less common and often mistreated development configuration: Oracle and .Net. Sure, most of Oracle development is traditionally made with Java but the configuration shown here can be very useful to many people.
Even beginners may appreciate the Separation of Concerns design principle implemented in the project, with client and server completely decoupled and communicating through services. The two apps are platform-independent. For example you can run the server component on Windows and the client on Linux. Or the opposite.
In future articles of this series I’ll explain how also development of this apps can be platform-agnostic. So stay tuned and thank you for reading!
References
[XE] - https://www.oracle.com/database/technologies/appdev/xe.html
[Quickstart] - https://www.oracle.com/it/database/technologies/appdev/xe/quickstart.html
[SQLDeveloper] - https://www.oracle.com/database/technologies/appdev/sql-developer.html
[18cHelpCenter] - https://docs.oracle.com/en/database/oracle/oracle-database/18/index.html
[InstallNode] - https://nodejs.org/en/download/
