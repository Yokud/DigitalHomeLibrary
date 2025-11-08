# DigitalHomeLibrary

A lightweight project for digital accounting of books from my home library

## Motivation

A user-friendly system for tracking books, both physical and digital, is needed. It should allow for convenient searching with filters and compiling reading statistics over a specific period.

## Architecture

Digital home library is based on a microservice architecture.

List of microservices:

* Gateway service (based on Ocelot)
* Authentification service
* Tracking books service
  * CRUD operations for books info
  * add score and review for book
  * managing tags of books by user
  * statuses for book: "Not read", "Reading" and "Read" (the difference with tags is status is subject to the bussines rules unlike tags)
  * filtered seaching books by name, author, year of release, publisher, ISBN, genre, book language
* Digital books service
  * CRUD operations for digital books files (pdf and etc.)
* Statistics service
  * getting count of read books  over a specific period
  * average time of reading book
  * distribution of read books by genre
  * dynamic of reading books
