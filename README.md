# LabAssistant App

App to help laboratory specialists in their daily work.


## General Information

The idea of this software came to me during visit at my local laboratory where specialists are performing measurements their machines.
The machines are measuring samples and after finish measurment reports are beeing printed on paper.
The laboratory specialists are analysing all reports and writing down the most important data to create final report which is send out to customers.


## The problem

I noticed that analysys reports from machines and re writting them into final report is quite time absorbing operation.
After talking with lab manager and lab specialist my estimation are that 40% of lab specialist time is rewritting data and creation of final reports.
Copying reports and creating them is tedious and monotous work.
The idea is to prepare tool to automate this work.


## The solution:

Create software to be able to read data from machine's reports and automaticly transfer only needed data to final report.
All lab specialist need to do is to make photo/scan of machine reports.
I devided my work into stages:
Stage 1: 
    *Prepare library to handle with all photo preprocessing activities, reading text of machine report and extracting needed data into basic report
    *Prepare console app to work with library and be able to perform tests to improve my ocr engine
Stage 2:
    *Prepare desktop app to be more user friendly comparing to console app and to be able to test app capabilities with other users.
Stage 3: 
    *Prepare smartphone app to be able to make photo of machine report and process it to final report on smarthone screen


## Technology stack:

* C#, 
* .NET 8.0, 
* tesseract library for OCR
* WPF for desktop app
* MAUI for smartphone app


## Technologies - implementation ToDo:
* C#                : as main programming language
* .Net 8            : as main framework for software
* WPF.Net           : to create desktop frontend
* tesseract         : as main OCR engine
* MAUI.Net          : to create smarthone app               


## Features

* Preprocessing photo with report from measurement machine 
* Read text from machine's report - different preprocessed images will be analysed
* Retrive only needed data from machine's report
* Error correction mechanism. Comparing retrived information from different preprocessed images and correct potential errors.
* Presenting results on screen
* Final report creation


## Screenshots

To be available as soon as desktop app is implemented.


## Creator

Created by Lukas Zlocki  
