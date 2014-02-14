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
3. Döp din VS-solution till **Company-Dependencies-Examples**

### 2.2 VS-project
1. Högerklicka din VS-solution i **Solution Explorer** -> Add -> New Project
2. **OBS!** - det kan hända att **Visual Basic** är markerat, se till att markera/expandera **Visual C#** (så det inte blir fel, för vi vill ju inte koda VB)
3. Välj Installed -> Visual C# -> Windows -> Class Library
4. Döp ditt VS-project till **Company.Dependencies.Examples**

### 2.3 VS-test-project
1. Högerklicka din VS-solution i **Solution Explorer** -> Add -> New Project
2. **OBS!** - det kan hända att **Visual Basic** är markerat, se till att markera/expandera **Visual C#** (så det inte blir fel, för vi vill ju inte koda VB)
3. Välj Installed -> Visual C# -> Test -> Unit Test Project
4. Döp ditt VS-test-project till **Company.Dependencies.Examples.UnitTests**