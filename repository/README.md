# AirUFV
practicalWork1OOP
<img src="./hiqw5lig.png"
style="width:5.5625in;height:1.38542in" />

> **UNIVERSIDAD** **FRANCISCO** **DE** **VITORIA**
>
> **ESCUELA** **POLITÉCNICA** **SUPERIOR**
>
> OOP - Practical Work 1 Design Detailed Document
>
> **Group** **2**
>
> María Fernández del Pozo
>
> Ángela Pérez López
>
> Jaime Ordovás Curbera
>
> <img src="./ikc0vsmy.png"
> style="width:0.33333in;height:0.33333in" />Ingeniería Informática,
> Universidad Francisco de Vitoria ©

**INDEX**

Introduction 3 Description 3 Class Diagram 6 Problems 8 Assumptions 8
Conclusion 9

> 2
>
> <img src="./r31uy3mx.png"
> style="width:0.33333in;height:0.33333in" />Ingeniería Informática,
> Universidad Francisco de Vitoria ©

**Introduction**

GitHub repository:
[<u>https://github.com/meripozo/AirUFV</u>](https://github.com/meripozo/AirUFV)

Group members are:

> \- Jaime Ordovás Curbera
>
> \- María Fernández del Pozo Romero - Ángela Pérez López

In this document we will make a description of the design and the
development of the program. We will explain our coding choices and the
difficulties or problems we found while programming, followed by the
solution we found. Then we have a brief conclusion where we mention what
we learned after doing the practical work.

**Description**

Our program is divided into the following sections:

**Program**

> *-* *Main()*

First initiates the simulation and the airport classes. Then, displays
the menu (load flights from file, add a flight manually, starts a
simulation manually and exit) of the program and selects the
corresponding part of the program depending on the option selected. For
this we used a switch case with 4 cases and a default. Case 1 calls
LoadAircraftFromFile() and asks for the file path, Case 2 calls
AddAircraft(), Case 3 calls RunSimulationManu(), Case 4 exits the menu
and default appears when the option entered is invalid.

**Airport**

> *-* *Airport* *()*

We create an array for the runway and a list for the aircraft.

> *-* *AddRunway()*

Initializes the aircraft list and the runway array with 3 empty runways,
updating the status of the runway to free.

> *-* *ShowStatus()*

Class created to show the current status of the runway by calling the
“PrintRunwayInfo” from a foreach. Then with console.writelines we
display the number of aircraft loaded manually using “Aircrafts.Count”.

> 3
>
> <img src="./r4p1sbwn.png"
> style="width:0.33333in;height:0.33333in" />Ingeniería Informática,
> Universidad Francisco de Vitoria ©
>
> *-* *GetNumberOfAircrafts()*

This returns the count of the aircrafts added.

> *-* *AdvanceTick()*

To advance the stimulation by one tick and update the aircraft and the
runway, we use the class “AdvanceTick” and with a foreach, unless the
status is 4, the tick gets updated. To assign waiting aircrafts to free
runways we created a foreach and an if statement for when the status is
2 (waiting) so it can land.

> *-* *LoadAircraftFromFile()*

LoadAircraftFromFile is one of the main classes of our program because
it is the one in charge of opening and reading the file (aricrafts.csv),
followed by validation of the file path and the attributes with try
catches, exceptions and the “IsValid” classes.

> *-* *AddAircraft()*

AddAircraft is one of the other main classes that makes it possible for
the user to manually add aircrafts to the program, first with a do while
you select the type of aircraft and then it displays the attributes for
the user to input the corresponding data depending on the type of
aircraft. We also added the validation for these data inputs. We use a
bool variable to validate if the input is in a correct format or not, in
order to show the Exceptions messages to the user.

**Simulator**

*-* *Simulator()* Initializes the airport.

> *-* *LoadAircraftFromFile()*

Calls LoadAircraftFromFile() and passes the file path. Then using the
received path, airport. LoadAircraftFromFile() indicates if the file has
been loaded correctly with a bool. Then it displays a message saying
that the file has been successfully loaded.

> *-* *AddAircraft()*

Calls Add.Aircraft() from airport.

> *-* *RunSimulationManu()*

First, shows the number of airplanes loaded by calling
GetNumberOfAircraft() from airport. Then it displays the program current
situation, showing the runways being used with the corresponding
aircraft with its information and ticks, with ShowStatus(). When the
user clicks any key it will update the ticks and if the user type “\*”
it will exit.

> 4
>
> <img src="./senakdd4.png"
> style="width:0.33333in;height:0.33333in" />Ingeniería Informática,
> Universidad Francisco de Vitoria ©

**Aircraft**

This is an abstract class that contains the declaration, setters and
getters of all the aircraft attributes that all the types have in common
with the corresponding constructor.

> *-* *UpdateTick()*

It is a virtual class that is in charge of updating the ticks and
modifying the status. This class also calculates the distance covered in
15 minutes (every tick), the fuel consumed depending on the distance
travelled and also controls that if the distance is 0, the status
changes to Waiting.

> *-* *Enums*

Represents each status with a number. InFlight = 1, Waiting = 2, Landing
= 3, OnGround = 4.

> *-* *PrintAircraftInfo*

Prints the current aircraft information with a class.

**CargoAircraft**

It is inherited from the class Aircraft. Contains the setters and
getters of the specific attribute, in this case is the maximum load
(maxLoad) and the constructor with all the attributes.

> *-* *PrintAircraftInfo()*

Also we used polymorphism with an override “PrintAircraftInfo” to print
the specific attribute along with the rest of the attributes.

**CommercialAircraft**

It is inherited from the class Aircraft. Contains the setters and
getters of the specific attribute, in this case is the number of
passengers (numPassengers) and the constructor with all the attributes.

> *-* *PrintAircraftInfo()*

Also we used polymorphism with an override “PrintAircraftInfo” to print
the specific attribute along with the rest of the attributes.

**PrivateAircraft**

It is inherited from the class Aircraft. Contains the setters and
getters of the specific attribute, in this case is the owner of the
aircraft (owner) and the constructor with all the attributes.

> *-* *PrintAircraftInfo()*

Also we used polymorphism with an override “PrintAircraftInfo” to print
the specific attribute along with the rest of the attributes.

> 5
>
> <img src="./tuus0q5v.png"
> style="width:0.33333in;height:0.33333in" />Ingeniería Informática,
> Universidad Francisco de Vitoria ©

**Runway**

Declaration of the attributes and the corresponding constructor. This
includes the runway status, which indicates if a runway is free, the
current aircraft and the ticks renaming, we also declare the ID of the
aircrafts because we will be printing it and the end.

> *-* *UpdateTick()*

Is in charge of updating the runway's occupation time for every tick. It
contains an if statement that compares if the runway is occupied, if it
is occupied it leads to another if statements that if the ticks
remaining are more that 0, it takes away one tick, and if it is already
0, the aircrafts lands and the state gets to 4 (OnGround). Once this
happens, we call ReleaseRunway.

> *-* *ReleaseRunway()*

This is a class that indicates that the runway is free for the next
aircraft to land.

> *-* *PrintRunwayInfo*

Finally we also have this class that is in charge of printing the runway
information. If the runway is free it shows the name of the runway and
if it is occupied it shows what aircraft is occupying the runway and how
many ticks are left.

**Aircrafts.csv**

\- The file that contains the aircrafts data in this order:
ID;State;Distance;Speed;Type;FuelCapacity;FuelConsumption;CurrentFuel;AdditionalData

**Class** **Diagram**

This is the class diagram of our finished practical work:

> 6
>
> <img src="./5ezczxt1.png"
> style="width:0.33333in;height:0.33333in" />Ingeniería Informática,
> Universidad Francisco de Vitoria ©
>
> <img src="./er4kudzs.png"
> style="width:4.14583in;height:9.83333in" />7
>
> <img src="./k1nijd23.png"
> style="width:0.33333in;height:0.33333in" />Ingeniería Informática,
> Universidad Francisco de Vitoria ©

**Problems**

While doing the practical work, we faced different challenges

Enums:

> \- We were confused when we had to decide where to place the enums,
> first we created their own file but then we decided that it was better
> to implement them inside the aircraft file.

Validations:

> \- We had trouble deciding how or what we would use to create the
> validation for the inputs (the file path and the aircraft type
> attributes). First, we made a new class called verifications which
> have individual classes for each data type so that the corresponding
> class can be called when necessary. Finally we decided to do the try
> catch and format exceptions for each of the attributes.
>
> \- We also struggled with the try catch exceptions control, because
> our program was not entering in the catches.

**Assumptions**

> \- We assumed that our aircrafts start flying with a full fuel tank. -
> Also, runways are initially Free.
>
> 8
>
> <img src="./em2pisbq.png"
> style="width:0.33333in;height:0.33333in" />Ingeniería Informática,
> Universidad Francisco de Vitoria ©

**Conclusion**

We all agree that this project is essential to pass the subject as it is
a great tool to practice and implement all that we learned throughout
the course. Apart from improving our programming, this practical work
also made us better at teamwork and organisation because we had to
divide the work and to propose the idea of how we will program together
in a fair way. Overall, this effective work helped us to learn new
lessons and prepared us for future projects, like upcoming individual
practical work.

> 9

