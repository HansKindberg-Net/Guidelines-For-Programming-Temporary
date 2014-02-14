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
3. D�p din VS-solution till **Company-Dependencies-Examples**

### 2.2 VS-project
1. H�gerklicka din VS-solution i **Solution Explorer** -> Add -> New Project
2. **OBS!** - det kan h�nda att **Visual Basic** �r markerat, se till att markera/expandera **Visual C#** (s� det inte blir fel, f�r vi vill ju inte koda VB)
3. V�lj Installed -> Visual C# -> Windows -> Class Library
4. D�p ditt VS-project till **Company.Dependencies.Examples**

### 2.3 VS-test-project
1. H�gerklicka din VS-solution i **Solution Explorer** -> Add -> New Project
2. **OBS!** - det kan h�nda att **Visual Basic** �r markerat, se till att markera/expandera **Visual C#** (s� det inte blir fel, f�r vi vill ju inte koda VB)
3. V�lj Installed -> Visual C# -> Test -> Unit Test Project
4. D�p ditt VS-test-project till **Company.Dependencies.Examples.UnitTests**