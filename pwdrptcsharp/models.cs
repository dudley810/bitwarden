using System;

public class Rootobject
{
    public BitwardenData[] myPasswords { get; set; }
}

public class BitwardenData
{
    public string _object { get; set; }
    public string id { get; set; }
    public string organizationId { get; set; }
    public object folderId { get; set; }
    public int type { get; set; }
    public int reprompt { get; set; }
    public string name { get; set; }
    public string notes { get; set; }
    public bool favorite { get; set; }
    public Login login { get; set; }
    public string[] collectionIds { get; set; }
    public DateTime revisionDate { get; set; }
    public DateTime creationDate { get; set; }
    public object deletedDate { get; set; }
    public Passwordhistory[] passwordHistory { get; set; }
    public Card card { get; set; }
    public Securenote secureNote { get; set; }
    public Identity identity { get; set; }
    public Field[] fields { get; set; }
}

public class Login
{
    public Uri[] uris { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string totp { get; set; }
    public DateTime? passwordRevisionDate { get; set; }
}

public class Uri
{
    public object match { get; set; }
    public string uri { get; set; }
}

public class Card
{
    public string cardholderName { get; set; }
    public string brand { get; set; }
    public string number { get; set; }
    public string expMonth { get; set; }
    public string expYear { get; set; }
    public string code { get; set; }
}

public class Securenote
{
    public int type { get; set; }
}

public class Identity
{
    public string title { get; set; }
    public string firstName { get; set; }
    public string middleName { get; set; }
    public string lastName { get; set; }
    public string address1 { get; set; }
    public object address2 { get; set; }
    public object address3 { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string postalCode { get; set; }
    public string country { get; set; }
    public object company { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public object ssn { get; set; }
    public string username { get; set; }
    public object passportNumber { get; set; }
    public object licenseNumber { get; set; }
}

public class Passwordhistory
{
    public DateTime lastUsedDate { get; set; }
    public string password { get; set; }
}

public class Field
{
    public string name { get; set; }
    public string value { get; set; }
    public int type { get; set; }
    public object linkedId { get; set; }
}

