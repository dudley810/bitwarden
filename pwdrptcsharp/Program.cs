using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

SecureString secureKey = new SecureString();
ConsoleKeyInfo key;
DateTime myChgDate = DateTime.Now;

Console.WriteLine("What is your session key: ");
do
{
    key = Console.ReadKey(true);
    // Ignore any key out of range.
    if (((int)key.Key) >= 65 && ((int)key.Key <= 90))
    {
        // Append the character to the password.
        secureKey.AppendChar(key.KeyChar);
        Console.Write("*");
    }
    // Exit if Enter key is pressed.
} while (key.Key != ConsoleKey.Enter);
Console.WriteLine();

bool dateInvalid = false;
do
{
    Console.WriteLine("What password change date would you like? (Hit Enter for today) (All older change dates will be included in report) : ");
    try
    {
        string mydate = Console.ReadLine();
        if (string.IsNullOrEmpty(mydate))
        {
            dateInvalid = true;
        }
        else
        {
            myChgDate = DateTime.Parse(mydate);
        }
        dateInvalid= true;
    }
    catch(Exception ex) 
    {
        Console.WriteLine(ex.ToString());   
    }
} while (dateInvalid == false);

Process p = new Process();
p.StartInfo.UseShellExecute = false;
p.StartInfo.RedirectStandardOutput = true;
p.StartInfo.Arguments = " list items --session " + secureKey.ToString();
p.StartInfo.FileName = "bw ";
p.Start();

try
{
    List<BitwardenData>? bwdList = JsonSerializer.Deserialize<List<BitwardenData>>(p.StandardOutput.ReadToEnd());
    using StreamWriter file = new("pwdrpt.csv");
    file.WriteLine("Item_Name,Password_Revision_Date");
    if (bwdList != null)
    {
        foreach (var item in bwdList)
        {
            if (item.type == 1)
            {
                try
                {
                    DateTime cd = Convert.ToDateTime(item.creationDate);
                    if (cd.Date <= myChgDate)
                    {
                        if (item.login.passwordRevisionDate == null)
                        {
                            file.WriteLine(item.name + ",change it now");
                        }
                        else
                        {
                            DateTime rd = Convert.ToDateTime(item.login.passwordRevisionDate);
                            if (rd.Date <= myChgDate.Date)
                            {
                                file.WriteLine(item.name + "," + item.login.passwordRevisionDate);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Credential is new since the date so I skipping it:"+ item.name);
                    }
                }
                catch
                {
                    file.WriteLine(item.name + ",change it now");
                }
            }
        }
        file.Close();
    }
    else
    {
        Console.WriteLine("There was an issue with your passwords being null, please try again");
    }
}
catch (Exception ex)
{
    Console.WriteLine("There was an issue with your passwords: " + ex.Message);
}
p.WaitForExit();
