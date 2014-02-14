[Tillbaka](/ReadMe.sv.md#221-exempel%C3%B6vning)

[English](/Documents/Dependencies.md)

# Beroenden (dependencies) - exempel/�vning

## 1 Inledning
Se till att du har installerat senaste version av **NuGet Package Manager** (Visual Studio extension).

N�r du skapar VS-solution och VS-project f�r dessa exempel/�vningar s� kan du v�lja .NET Framework version. Anv�nd d� n�gon av f�ljande:
- .NET Framework 3.5
- .NET Framework 4
- .NET Framework 4.5

Anv�nd samma framework f�r all VS-project. S� om du anv�nder **.NET Framework X** n�r du skapar din VS-solution anv�nd d� ocks� **.NET Framework X** f�r de VS-project du skapar.

## 2 Skapa VS-solution

### 2.1 Blank VS-solution
1. File -> New -> Project -> Templates -> Other Project Types -> Visual Studio Solutions -> Blank Solution
2. V�lj .NET Framework
3. D�p din VS-solution till **Company-Examples**

### 2.2 VS-project
1. H�gerklicka din VS-solution i **Solution Explorer** -> Add -> New Project
2. **OBS!** - det kan h�nda att **Visual Basic** �r markerat, se till att markera/expandera **Visual C#** (s� det inte blir fel, f�r vi vill ju inte koda VB)
3. V�lj Installed -> Visual C# -> Windows -> Class Library
4. D�p ditt VS-project till **Company.Examples**
5. Ta bort den automat-genererade klassen **Class1.cs**

### 2.3 VS-test-project
1. H�gerklicka din VS-solution i **Solution Explorer** -> Add -> New Project
2. **OBS!** - det kan h�nda att **Visual Basic** �r markerat, se till att markera/expandera **Visual C#** (s� det inte blir fel, f�r vi vill ju inte koda VB)
3. V�lj Installed -> Visual C# -> Test -> Unit Test Project
4. D�p ditt VS-test-project till **Company.Examples.UnitTests**
5. Ta bort den automat-genererade klassen **UnitTest1.cs**
6. L�gg till referens till **Company.Examples** (ditt Class Library VS-project)

### 2.4 L�gg till kataloger
1. L�gg till katalogen **HardToTest** i **Company.Examples**
2. L�gg till katalogen **Testable** i **Company.Examples**
3. L�gg till katalogen **HardToTest** i **Company.Examples.UnitTests**
4. L�gg till katalogen **Testable** i **Company.Examples.UnitTests**

## 3 Beroenden till klasser med statiska metoder/egenskaper
Det finns ett problem i att enhets-testa klasser som har beroenden till klasser med statiska metoder/egenskaper.

### 3.1 Skapa en klass med en statisk metod ([/Company-Examples/Company.Examples/HardToTest/Dependencies/ClassWithStaticMethod.cs](/Company-Examples/Company.Examples/HardToTest/Dependencies/ClassWithStaticMethod.cs))
1. L�gg till katalogen **Dependencies** som underkatalog till **HardToTest** i **Company.Examples**
1. Skapa en klass som heter **ClassWithStaticMethod** i katalogen **Dependencies**

### 3.2 Skapa en klass med ett beroende till en klass med en statisk metod
1. Skapa en klass som heter **AClassWithStaticDependency** i katalogen **HardToTest** i VS-project **Company.Examples**
2. Skapa en klass som heter **AClassWithStaticDependencyTest** i katalogen **HardToTest** i VS-test-project **Company.Examples.UnitTests**

Vad vill vi testa:
Vi vill verifiera att ClassWithStaticMethod.Method() anropas n�r vi anropar AClassWithStaticDependency.Method(). Detta g�r inte att testa f�r det finns inget enkelt s�tt att verifiera att ClassWithStaticMethod.Method() har anropats.

Ett mindre bra s�tt att l�sa det p�:
Detta s�tt leder inte till b�ttre kod-design. Det kr�ver att man har ramverk som klarar av det:
- [**Microsoft Fakes**](http://msdn.microsoft.com/en-us/library/hh549175.aspx) - kr�ver Visual Studio Premium/Ultimate 2012/2013 ([Isolating Code Under Test with Microsoft Fakes](http://msdn.microsoft.com/en-us/library/hh549175.aspx))
- [**Typemock Isolator**](http://www.typemock.com/isolator-product-page)
- [**Telerik JustMock**](http://www.telerik.com/products/mocking.aspx)
 



