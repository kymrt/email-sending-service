# Email Sending Service using SendGrid

It is an MVC Core API project prepared to send a dynamic template to one or more mail addresses using SendGrid API services.

Direct sending is provided with the /Email/SendEmail method. If there is a problem while sending and the mail is not received, CronJob will try to send mail every 5 minutes.


## Tech Stack

**Framework:** C#, MVC Web API Core 3.1

**NuGet Packages:** SendGrid, Hangfire, EntityFrameworkCore, NUnit

**Database:** MSSQL 


## Preparation

Change SendGrid API Key, Sender Mail Address and Template ID under the **Properties\launchSettings.json**

```bash
...
  "SENDGRID_KEY": "SENDGRID_KEY",
  "FROM_EMAIL_ADDRESS": "FROM_EMAIL_ADDRESS",
  "TEMPLATE_ID": "TEMPLATE_ID",
...
```
Change ConnectionStrings for database under the **appsettings.json**

```bash
...
  "ConnectionStrings": {
    "EmailDataContext": "ConnectionStrings"
  }
...
```
## API Reference

#### Send an email template to one or more mail address

```http
  POST /Email/SendEmail
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `emailAddressList` | `List<string>` | **Required**. Email Addresses |

#### Add one or more mail address to database

```http
  POST /Email/AddEmailAddressToDb
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `emailAddressList`      | `List<string>` | **Required**. Email Addresses |



