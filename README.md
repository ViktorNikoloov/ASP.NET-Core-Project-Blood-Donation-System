# Blood-Donation-System

## <u>Build with</u>:
* [ASP .NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0)
* [Entity Framework Core](https://www.pluralsight.com/paths/entity-framework-core?aid=7010a000002LUv2AAG&promo=&utm_source=non_branded&utm_medium=digital_paid_search_google&utm_campaign=XYZ_EMEA_Dynamic&utm_content=&cq_cmp=1576650371&gclid=CjwKCAjw-sqKBhBjEiwAVaQ9azcCpMYBTFR4qZIA1O7B34zN7f3q6vIEoZl5rL_maNlkxBR85eXjohoCzowQAvD_BwE)
* [MSSQL Server](https://skyvia.com/connectors/sql-server?gclid=CjwKCAjw-sqKBhBjEiwAVaQ9a4cFKf8u5hYvSd0GvE_sk7b4m0lGla61iAmwCS4sMySXVjvRb7Q9DhoCOiYQAvD_BwE)
* [AutoMapper](https://docs.automapper.org/en/stable/Queryable-Extensions.html)
* [Bootstrap](https://getbootstrap.com/)
* [reCaptcha](https://www.google.com/recaptcha/about/)
* [SendGrid](https://sendgrid.com/)
* [Cloudinary](https://cloudinary.com/)
* [Tinymce](https://www.tiny.cloud/)
* [HtmlSanitizer](https://csharp.hotexamples.com/examples/Ganss.XSS/HtmlSanitizer/-/php-htmlsanitizer-class-examples.html)
* [Facebook and Google login](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/?view=aspnetcore-5.0&tabs=visual-studio)
* [HangFire](https://www.hangfire.io/) for Administrator role olny

## <u>Hosted on</u>:
* [Azure](https://portal.azure.com/) - https://blooddonationbg.azurewebsites.net/

## <u>Author</u>
* [Viktor Nikolov](https://github.com/ViktorNikoloov)

---
---

## <b><u>Blood-Donation-System</u></b> 
is an online platform. Its purpose is to connect recipients who are looking for blood banks with volunteers blood donors.

---

## How to use Blood-Donation-System
Initially both sides should create account(both recipients and donors). Recipients can register freely, but donors require to be examined by administrator first. Donors can candidate by signing in with temporary information like email, password and phone number. Aditionally donors should answer 5 control questions(these are required).

* ### 1. Account creation and appling blood appointment by recipients.
---
Blood-Donation-System :heart: connects two types of users  :bust_in_silhouette: -- Recipient and Donors.

Both sides should create an account before accessing the functionality of the website.

Recipient | Donors
--- | ---
Recipient can [Create an account](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214472/Recipients/README/Recipient_register_qgayn1.jpg) from registration form or he/shre could sign in through [Facebook or Google login](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214472/Recipients/README/Recipient_login_r4fdiv.jpg) :heavy_check_mark:. Once successfully registered, the recipient can use the website and apply [Blood appointment](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629216481/Recipients/README/Blood_appointment_ijovkp.jpg). He/She also could see all his/her [taken appointments](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629217304/Recipients/README/RecipientAllApp_uokica.jpg). | Donors on the other hand cannot :x: create an account instantly. They should send an [application](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214472/Recipients/README/Donor_register_yfafmz.jpg) to be part of the platform since they are required to have certain traits in order to become a donor on our platform. 

>The active blood appointments appear with green color and the expired one with blue.

>Once the blood appointment is aplly the recipient could edit only "Additional info and Sending info" fields. If he/she want to edit something else he/she needs to write a request to the Admin.

> :warning: **Note**: Donors should be approved by a third party! 
> :warning: **Note**: Donors receive an [email](https://res.cloudinary.com/dvvbab0fs/image/upload/v1632393759/Recipients/README/DonorEmail_gbumm8.jpg) when his/her applicant is approved or rejected
> :warning: **Note**: Donor will receive an [approval](https://res.cloudinary.com/dvvbab0fs/image/upload/v1632858581/Recipients/README/DonorRecieveEmail_mwtwki.png) or rejected email.
> :warning: **Note**: Once approved by the admin the user is officially a **donor** and can access their profile :heavy_check_mark:.

>:warning: **Note**: A donor should fill their [Profile](https://res.cloudinary.com/dvvbab0fs/image/upload/v1632859092/Recipients/README/DonorSetUpProfil_xuw8yx.jpg) info after approval :bust_in_silhouette: in order to could take recipient's blood appointments.

>:warning: **Note**: There is a [Remaining messages](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629218016/Recipients/README/DonorRemainingMessage_dxpnmu.jpg) on the home page until his/her profile is not completed.

* ### 2. How donor choose an appointment.
---
Once both sides have completely set up their profiles then the donor have a choice [Choose](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629218356/Recipients/README/DonorChooseApp_bdqrvo.jpg)

> The page shows all of the blood appoinmtens and short information about them such as Recipient name, Start Date, End Date, Count of the blood banks and Blood type needed.

When the donor finds a recipient he/she could click on that recipient's appointment card and [See](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629218481/Recipients/README/DonorSeeApp1_ghiw5h.jpg) detailed information about the appointment. 
And if he/she wants to [Take](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629218561/Recipients/README/DonorSeeApp2_usbtcc.jpg) it just need to click on the button.

**Note**: The Donor also could Send all blood appointment's information to its email only by clicking the *Send* button or *Printing* it if he/she needs to save the information about the recipient.

>:warning: **Note**: Donors could take only 1 blood appointment every two mounths.
There is an [information](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629219467/Recipients/README/DonordonationInfo_iihhxm.jpg) with last donataion and ramaining time to the next one.

#### How does that work? **Pretty simple!**

- Once the appointment request has been taken, to the recipient will be sending an email with simple information about the donor.

* ### 4. Blog articles
* Create an [articles](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629315304/Recipients/README/table_a4qfq6.jpg).
* All blog's articles [articles](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629318198/Recipients/README/AllArticle_sc76vj.jpg).
* Everyone could choose and read the [article](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629317891/Recipients/README/Article_imibcd.jpg).
* Create or read [comments](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629318021/Recipients/README/Comments_cflspn.jpg) on the article's page.
* A few articles [in home page](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629317317/Recipients/README/Homearticles_abfevm.jpg).

* ### 3. Admin panel
The admin have an [Admin panel](https://res.cloudinary.com/dvvbab0fs/image/upload/v1632391834/Recipients/README/AdminPanel_c5iyrh.jpg).

The admin could:
* see all donor [applications](https://res.cloudinary.com/dvvbab0fs/image/upload/v1632392096/Recipients/README/AllApllications_ixs4sq.jpg).
* take a look of the donor's control [questions](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214471/Recipients/README/Admin_panel_candidates_correct_answer_n8jjnn.jpg) and take care of [approve](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214471/Recipients/README/Admin_panel_candidates_approved_lrap8p.jpg) or [reject](https://res.cloudinary.com/dvvbab0fs/image/upload/v1629214471/Recipients/README/Admin_panel_candidates_delete_nqtgak.jpg) the applicants.
* take a look of [all not approved](https://res.cloudinary.com/dvvbab0fs/image/upload/v1632392903/Recipients/README/AllNotApprovedBloodApp_r0rz3h.jpg) and [see all info](https://res.cloudinary.com/dvvbab0fs/image/upload/v1632392973/Recipients/README/BloodApplication_ajvtmi.jpg) And decide to [aprove](https://res.cloudinary.com/dvvbab0fs/image/upload/v1632393128/Recipients/README/BloodApplicationApprove_bp8idj.jpg) or [reject](https://res.cloudinary.com/dvvbab0fs/image/upload/v1632393128/Recipients/README/BloodApplicationApprove_bp8idj.jpg) it.
* could [Edit](https://res.cloudinary.com/dvvbab0fs/image/upload/v1632393258/Recipients/README/BloodApplicationEdit_jjirkn.jpg) all blood appointment
* Receive an [email](https://res.cloudinary.com/dvvbab0fs/image/upload/v1632393445/Recipients/README/AdminEmail_juvubx.jpg) when a new blood application is applyed.
* could write new blog articles or edit the old ones
* have access to HangFire statistics


## And more, and more...
 If you want to see all functionality of the website you just could take a look <here> with default users:<br><br>
**Note** You could create new account if you want to or use one of these:<br>
>Recipient:<br>
-username: recipient1@abv.bg<br>
-password: 123456<br>
>Donor:<br>
-username: donor1@abv.bg<br>
-password: 123456<br>
>Admin:<br>
-username: admin@admin.com<br>
password: 000000

## Future work
---
The site and the idea are not completed. I thnik to keep developing it  in further. It is a good idea for such a volunteer community of recipients and donors which help each other in my country.

