# TheGallop_Resort.Models

Gruppuppgift inom utbildningen **Systemutveckling .NET YH (200p)**.

Projektets syfte är att utveckla ett REST API för ett hotellbokningssystem samt att tillämpa testning och validering enligt moderna utvecklingsprinciper.

---

# Projektbeskrivning

Projektet innehåller endpoints som simulerar ett hotellbokningssystem där användaren kan:

* Skapa gäster
* Skapa bokningar
* Hantera reservationer
* Uppdatera bokningar
* Ta bort bokningar
* Visa historiska bokningar
* Visa framtida reservationer
* Hämta specifika bokningar och reservationer

API:et är uppbyggt med fokus på:

* Tydlig struktur
* Säker datahantering
* Validering
* Testbarhet
* Separation av ansvar

---

# Domänmodeller

Projektet innehåller följande domänmodeller:

* `Guest`
* `Booking`
* `Room`
* `RoomReservation`
* `RoomCategory`

---

# DTO:er

Flera olika DTO:er används för att:

* Skapa ett säkrare system
* Minska exponering av intern data
* Undvika kodduplicering
* Strukturera API-responser och requests

---

# Relationslogik

Projektet använder följande relationer:

* En `Guest` kan ha flera `Bookings`
* En `Booking` kan ha flera `RoomReservations`
* En `RoomReservation` kopplas till ett `Room`
* Ett `Room` tillhör en `RoomCategory`

---

# Valideringar

Projektet använder flera typer av validering för att säkerställa korrekt datahantering:

* FluentValidation
* DataAnnotations
* Datum- och intervallkontroller

Exempel på valideringar:

* Obligatoriska fält
* Ogiltiga datumintervall
* Tomma strängar
* Felaktiga ID:n

---

# Teststrategi

## Syfte

Syftet med testningen är att säkerställa att systemet fungerar korrekt, hanterar fel på ett säkert sätt och uppfyller projektets krav.

Testningen fokuserar på:

* Funktionalitet
* Affärslogik
* Valideringar
* API-responser
* Felhantering

---

## Testområden

Följande delar av systemet har testats:

* Controllers
* Services
* Valideringar

---

## Typer av tester

### Enhetstester (Unit Tests)

Enhetstester används för att testa enskilda komponenter isolerat.

Tester verifierar bland annat att:

* Metoder returnerar korrekt data
* Affärslogiken fungerar korrekt
* Fel hanteras på rätt sätt
* Valideringar fungerar enligt krav

---

### Controller-tester

Controller-tester används för att säkerställa att API-endpoints:

* Returnerar rätt HTTP-statuskoder
* Hanterar requests korrekt
* Returnerar korrekt responsdata

Exempel på testade statuskoder:

* `200 OK`
* `201 Created`
* `400 Bad Request`
* `404 Not Found`

---

## Testmetodik

### Happy Path

Tester där användaren skickar korrekt data och systemet förväntas fungera utan fel.

Exempel:

* Skapa en giltig bokning
* Hämta en existerande gäst
* Uppdatera en reservation korrekt

---

### Sad Path

Tester där användaren skickar ogiltig eller ofullständig data för att säkerställa att systemet hanterar fel korrekt.

Exempel:

* Skapa bokning med ogiltiga datum
* Hämta resurser som inte existerar
* Skicka tomma eller felaktiga värden

---

## Mockning och testverktyg

Följande verktyg och ramverk används i testningen:

* Ms Test
* FakeItEasy
* FluentAssertions

FakeItEasy används för mockning av beroenden för att kunna testa systemets logik isolerat.

---

# API Endpoints

## Booking Endpoints

### GET – Hämta alla bokningar

`/api/Booking/getAllBookings`

### GET – Hämta bokning via ID

`/api/Booking/getBookingsById/{bookingId}`

### POST – Skapa bokning

`/api/Booking/CreateBooking`

### PUT – Uppdatera gäst på bokning

`/api/Booking/updateGuestOnBooking`

### PUT – Uppdatera status på bokning

`/api/Booking/updateStatusOnBooking`

### GET – Bokningar för nästa månad

`/api/Booking/getBookingsForNextMonth`

### GET – Bokningar för specifikt datum

`/api/Booking/GetBookingsForSpecifikDate`

### GET – Bokningar mellan två datum

`/api/Booking/GetBookingsBetweenDates`

### DELETE – Ta bort bokning

`/api/Booking/DeleteBookingById`

---

## Guest Endpoints

### GET – Gästens bokningshistorik

`/api/Guest/{guestId}/GuestBookingHistory`

### GET – Gästens framtida reservationer

`/api/Guest/{guestId}/GuestFutureReservation`

### GET – Hämta alla gäster

`/api/Guest`

### POST – Skapa gäst

`/api/Guest`

### GET – Hämta gäst via ID

`/api/Guest/{guestId}`

### PUT – Uppdatera gäst

`/api/Guest/{guestId}`

### DELETE – Ta bort gäst

`/api/Guest/{guestId}`

### POST – Skapa gäst och reservation

`/api/Guest/CreateReservationAndGuest`

---

## Room Endpoints

### GET – Hämta alla rum

`/api/Room/getAllRooms`

---

## RoomCategory Endpoints

### POST – Skapa rumskategori

`/api/RoomCategory`

### GET – Hämta alla rumskategorier

`/api/RoomCategory`

### GET – Hämta rumskategori via ID

`/api/RoomCategory/{roomCategoryId}`

### PUT – Uppdatera rumskategori

`/api/RoomCategory/{roomCategoryId}`

### DELETE – Ta bort rumskategori

`/api/RoomCategory/{roomCategoryId}`

---

## RoomReservation Endpoints

### POST – Skapa rumsreservation

`/api/RoomReservation/CreateRoomReservation`

---
