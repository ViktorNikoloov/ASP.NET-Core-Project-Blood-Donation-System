# Blood-Donation-System

## Build with

* ASP.NET Core
* EntityFrameworkCore
* MSSQL Server
* AutoMapper
* Bootstrap
* reCaptcha
* SendGrid 
* Cloudinary -> Save Images at the Cloud service
* Facebook and Google login
* HtmlSanitizer -> Clear Incoming HTML from the chat for XSS
* HangFire for Administrator

## Hosted on
* Azure

## Authors
* Viktor Nikolov


## Blood-Donation-System is an online platform. It's purpose is to connect recipients who looking for blood bank with volunteer blood donors.
---
## How to use Blood-Donation-System
Initially both sides should create account(both recipients and donors). Recipients can register freely, but donors require to be examined by administrator first. Donors can candidate by signing in with temporary information like email, password and phone number. Aditionally donors should answer 5 control questions(these are required).

### 1. Account creation
---
Blood-Donation-System :feet: connects two types of users  :bust_in_silhouette: -- Recipient and Donors.

Both sides should create an account before accessing the functionality of the website.

Recipient | Donors
--- | ---
Recipient can [create an account](img) right off the bat :heavy_check_mark:. Once successfully registered, the recipient can use the website and apply blood appointments. | Donors on the other hand cannot :x: create an account instantly. They should send an [application](img) to be part of the platform since they are required to have certain traits in order to become a donors on our platform. 

> :warning: **Note**: A donors should be approved by a third party (in this case the admin user) takes care of [approval](img) :thumbsup: or [rejection](img) :thumbsdown: of applicants)  

Once approved by the admin the user is officially a **donor** and can access their profile :heavy_check_mark:.

>:warning: **Note**: A donor should fill their profile info after approval :bust_in_silhouette: in order to could take recipient's blood appointments.


### 2. Appling blood appointment by recipients and helping to the recpients by taking an appointment by donors
---
Once both sides have completely set up their profiles then the donor can choose and take a blood appointment :arrow_upper_right: sending by recipiets. 


#### How does that work? **Pretty simple!**
---
Donor have a [tab](img) on their navigation bar where they can access the list of available blood appointments :heavy_check_mark:! When opened, the page will contain a list of blood appointments which the owner can choose of.

![enter image description here](img)

> This page shows all of the appoinmtens and short information about them such as Recipient name, Start Date, End Date, Count of the blood banks and Blood type needed

When the donor finds a recipient the donor can [click](img) on that recipient's appointment card and see detailed information about the appointment. In addition there is a button to send [Email] to the donor with all given information in it or [Print] it if he want to. There is also a button to take the appointment[appointment request](img)

>:warning: **Note**: Donors could take only 1 blood appointment every two mounths.

- Once the appointment request has been took, on the recipient will be sending a email with donor's information.

- Only the recipient can initiate an appointment by [clicking on the appointment that he/she button](img)
>:warning: **Note**: an appointment cannot be started unless the day of the appointment matches the current day 

## Future work
---

Right now I am working on this idea and developing it further. It is a good idea for such a community of recipients and donors in my country.