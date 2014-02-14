[Tillbaka](/ReadMe.sv.md#221-exempel%C3%B6vning)

[English](/Documents/Dependencies.md)

# Beroenden (dependencies) - exempel/övning

## 1 Inledning
Se till att du har installerat senaste version av **NuGet Package Manager** (Visual Studio extension).

När du skapar VS-solution och VS-project för dessa exempel/övningar så kan du välja .NET Framework version. Använd då någon av följande:
- .NET Framework 3.5
- .NET Framework 4
- .NET Framework 4.5

Använd samma framework för all VS-project. Så om du använder **.NET Framework X** när du skapar din VS-solution använd då också **.NET Framework X** för de VS-project du skapar.

## 2 Skapa VS-solution

### 2.1 Blank VS-solution
1. File -> New -> Project -> Templates -> Other Project Types -> Visual Studio Solutions -> Blank Solution
2. Välj .NET Framework
3. Döp din VS-solution till **Company-Examples**

### 2.2 VS-project
1. Högerklicka din VS-solution i **Solution Explorer** -> Add -> New Project
2. **OBS!** - det kan hända att **Visual Basic** är markerat, se till att markera/expandera **Visual C#** (så det inte blir fel, för vi vill ju inte koda VB)
3. Välj Installed -> Visual C# -> Windows -> Class Library
4. Döp ditt VS-project till **Company.Examples**
5. Ta bort den automat-genererade klassen **Class1.cs**

### 2.3 VS-test-project
1. Högerklicka din VS-solution i **Solution Explorer** -> Add -> New Project
2. **OBS!** - det kan hända att **Visual Basic** är markerat, se till att markera/expandera **Visual C#** (så det inte blir fel, för vi vill ju inte koda VB)
3. Välj Installed -> Visual C# -> Test -> Unit Test Project
4. Döp ditt VS-test-project till **Company.Examples.UnitTests**
5. Ta bort den automat-genererade klassen **UnitTest1.cs**
6. Lägg till referens till **Company.Examples** (ditt Class Library VS-project)

### 2.4 Lägg till kataloger
1. Lägg till katalogen **HardToTest** i **Company.Examples**
2. Lägg till katalogen **Testable** i **Company.Examples**
3. Lägg till katalogen **HardToTest** i **Company.Examples.UnitTests**
4. Lägg till katalogen **Testable** i **Company.Examples.UnitTests**

## 3 Beroenden till klasser med statiska metoder/egenskaper
Det finns ett problem i att enhets-testa klasser som har beroenden till klasser med statiska metoder/egenskaper.

### 3.1 Skapa en klass med en statisk metod ([/Company-Examples/Company.Examples/HardToTest/Dependencies/ClassWithStaticMethod.cs](/Company-Examples/Company.Examples/HardToTest/Dependencies/ClassWithStaticMethod.cs))
1. Lägg till katalogen **Dependencies** som underkatalog till **HardToTest** i **Company.Examples**
1. Skapa en klass som heter **ClassWithStaticMethod** i katalogen **Dependencies**

### 3.2 Skapa en klass med ett beroende till en klass med en statisk metod
1. Skapa en klass som heter **AClassWithStaticDependency** i katalogen **HardToTest** i VS-project **Company.Examples**
2. Skapa en klass som heter **AClassWithStaticDependencyTest** i katalogen **HardToTest** i VS-test-project **Company.Examples.UnitTests**

Vad vill vi testa:
Vi vill verifiera att ClassWithStaticMethod.Method() anropas när vi anropar AClassWithStaticDependency.Method(). Detta går inte att testa för det finns inget enkelt sätt att verifiera att ClassWithStaticMethod.Method() har anropats.

Ett mindre bra sätt att lösa det på:
Detta sätt leder inte till bättre kod-design. Det kräver att man har ramverk som klarar av det:
- [**Microsoft Fakes**](http://msdn.microsoft.com/en-us/library/hh549175.aspx) - kräver Visual Studio Premium/Ultimate 2012/2013 ([Isolating Code Under Test with Microsoft Fakes](http://msdn.microsoft.com/en-us/library/hh549175.aspx))
- [**Typemock Isolator**](http://www.typemock.com/isolator-product-page)
- [**Telerik JustMock**](http://www.telerik.com/products/mocking.aspx)
 



