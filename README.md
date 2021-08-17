# Blood-Donation-System

## <u>Build with</u>:
* ASP .NET Core
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

## <u>Hosted on</u>:
* Azure

## <u>Author</u>
* Viktor Nikolov

---
---

## <b><u>Blood-Donation-System</u></b> 
is an online platform. It's purpose is to connect recipients who are looking for blood banks with volunteers blood donors.

---

## How to use Blood-Donation-System
Initially both sides should create account(both recipients and donors). Recipients can register freely, but donors require to be examined by administrator first. Donors can candidate by signing in with temporary information like email, password and phone number. Aditionally donors should answer 5 control questions(these are required).

* ### 1. Account creation and appling blood appointment by recipients.
---
Blood-Donation-System :heart: connects two types of users  :bust_in_silhouette: -- Recipient and Donors.

Both sides should create an account before accessing the functionality of the website.

Recipient | Donors
--- | ---
Recipient can [Create an account](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214472/Recipients/README/Recipient_register_qgayn1.jpg) from registration form or he/shre could sign in through [Facebook or Google login](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214472/Recipients/README/Recipient_login_r4fdiv.jpg) :heavy_check_mark:. Once successfully registered, the recipient can use the website and apply [Blood appointment](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629216481/Recipients/README/Blood_appointment_ijovkp.jpg). He/She also could see all his/her taken appointments [old appointments](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629217304/Recipients/README/RecipientAllApp_uokica.jpg). | Donors on the other hand cannot :x: create an account instantly. They should send an [application](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214472/Recipients/README/Donor_register_yfafmz.jpg) to be part of the platform since they are required to have certain traits in order to become a donor on our platform. 

>The active blood appointments appear with green color and the expierd one with blue.

>Once the blood appointment is aplly the recipient could edit only "Additional info and Sending info" fields. If he/she want to edit something else he/she need to write a requiest to the Admin.

> :warning: **Note**: A donors should be approved by a third party! 

The admin user have acces to [All applicants](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214471/Recipients/README/Admin_panel_candidates_approved_message_zwu3sw.jpg). He could see all [question's answers](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214471/Recipients/README/Admin_panel_candidates_correct_answer_n8jjnn.jpg) and he takes care of [approval](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214471/Recipients/README/Admin_panel_candidates_approved_lrap8p.jpg) :thumbsup: or [rejection](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214471/Recipients/README/Admin_panel_candidates_delete_nqtgak.jpg) :thumbsdown: of applicants).

**Note**: The Donors recieving an email if his/her applicant is approved or rejected.

Once approved by the admin the user is officially a **donor** and can access their profile :heavy_check_mark:.

>:warning: **Note**: A donor should fill their [Profile](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629217823/Recipients/README/DonorProfile_gb7hza.jpg) info after approval :bust_in_silhouette: in order to could take recipient's blood appointments.

>:warning: **Note**: There is a [Remaining messages](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629218016/Recipients/README/DonorRemainingMessage_dxpnmu.jpg) on the home page until his/her profile is not completed.

* ### 2. How donor choose an appointment.
---
Once both sides have completely set up their profiles then the donor can [Choose](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629218356/Recipients/README/DonorChooseApp_bdqrvo.jpg)

> This page shows all of the blood appoinmtens and short information about them such as Recipient name, Start Date, End Date, Count of the blood banks and Blood type needed

When the donor finds a recipient the donor can click on that recipient's appointment card and [See](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629218481/Recipients/README/DonorSeeApp1_ghiw5h.jpg) detailed information about the appointment. 
And if he/she want to [Take](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629218561/Recipients/README/DonorSeeApp2_usbtcc.jpg) it just need to click on the button.

**Note**: The Donor also could Send all blood appointment's information to its email only by clicking the [Send] button or [Printing] it if he/she want to.

>:warning: **Note**: Donors could take only 1 blood appointment every two mounths.
There is a [information on the navigation bar](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629219467/Recipients/README/DonordonationInfo_iihhxm.jpg) with last donataion and ramaining time.

#### How does that work? **Pretty simple!**

- Once the appointment request has been took, to the recipient will be sending a email that the appointment is taken and a little information about the donor.


* ### 3. Admin panel
The admin have an [Admin panel](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214471/Recipients/README/Admin_panel_candidates_approved_message_zwu3sw.jpg).

The admin could:
* Take a look of the donor's control questions and take care of approve or reject the applicants.
* Take a look of every new applyed blood appointment and decide to aprove or reject it.
* Could Edit all blood appointment
* Could write new blog post or edit the old one
* Have access to HangFire statistics


## And more, and more...
> If you want to see all functionality of the website you just could take a look <here> with default users:<br><br>
**Note** You always could create an account or use default one:<br>
Recipient:<br>
-username: recipient1@abv.bg<br>
-password: 123456
<br>
Donor:<br>
-username: donor1@abv.bg<br>
-password: 123456
<br>
Admin:<br>
-username: admin@admin.com<br>
password: 000000

## Future work
---

Right now I am working on this idea and developing it further. It is a good idea for such a community of recipients and donors in my country.