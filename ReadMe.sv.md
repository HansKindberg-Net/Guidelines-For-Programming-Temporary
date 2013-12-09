[English](ReadMe.md)
#Riktlinjer f�r programmering

##1. Inledning
Det h�r dokumentet inneh�ller riktlinjer f�r programmering i huvudsak f�r **.NET** och **Visual Studio**. Avsnittet **Testbarhet** kan dock appliceras p� andra programmeringsspr�k. Jag anser att avsnittet **Testbarhet** �r det viktigaste avsnittet och d�rf�r har jag valt att l�gga det f�rst.

##2. Testbarhet
(Testbarhet inneh�ller riktlinjer f�r att skriva testbar-kod. Leder till kod/mjukvara som blir mer plugbar)

Mjukvara kan testas p� olika s�tt, p� olika niv�er, http://en.wikipedia.org/wiki/Software_testing#Testing_levels.
* enhetstest (unit test) � testa en minsta enhet, en metod/egenskap i en klass i ett system
* integrationstest (integration test) � testa funktionalitet mellan enheter
* systemtest (system test) � testa ett helt system

F�r mig som programmerare handlar testbarhet mest om programmerbara/automatiska tester. Jag anser att begreppet testbarhet mest h�r ihop med enhetstester (unit tests). Bygg dina klasser s� att de blir testbara, vilket inneb�r att klassens beroenden �r abstrakta och injicerbara (injectable).

###2.1 F�rdelar
F�rdelar med testbarhet:
* **Pluggbara** system � system/mjukvara d�r det �r l�tt att byta ut olika delar
* System/mjukvara som kan k�ras i olika milj�er med olika f�ruts�ttningar, produktion, test, utveckling m.m.

�ven om man inte skriver/programmerar n�gra tester men skriver sin kod testbar s� anser jag att man f�r en bra mjukvaru-design. Jag anser ocks� att det kommer att resultera i att de klasser man skriver/programmerar hanterar det de ska, vilket resulterar i kod som �r l�ttare att underh�lla och man undviker redundant kod (duplicate code). Samtidigt kr�ver det mer av den som programmerar att se till att klasser underh�lls p� r�tt s�tt eftersom det �r mycket troligt att flera andra klasser har ett beroende till klassen.

###2.2 Beroenden (dependencies) � dependency injection
Klasser har beroenden till andra klasser. Id�n med enhetstestning �r att testa kod utan att testa dess beroenden. Tanken �r att om en klass fungerar som den �r designad och dess beroende klasser likas� s� borde de fungera tillsammans som t�nkt.

##3. Visual Studio
###3.1 NuGet
Enable NuGet Package Restore
In the Solution Explorer
Right click you solution
Click Enable NuGet Package Restore
F�ljande katalog och filer har nu skapats under rotkatalogen f�r din solution:
.nuget
NuGet.Config
NuGet.exe
NuGet.targets
        <!-- Determines if package restore consent is required to restore packages -->
        <RequireRestoreConsent Condition=" '$(RequireRestoreConsent)' != 'false' ">true</RequireRestoreConsent>
        
        <!-- Download NuGet.exe if it does not already exist -->
        <DownloadNuGetExe Condition=" '$(DownloadNuGetExe)' == '' ">false</DownloadNuGetExe>
    </PropertyGroup>
    
    <ItemGroup Condition=" '$(PackageSources)' == '' ">
        <!-- Package sources used to restore packages. By default, registered sources under %APPDATA%\NuGet\NuGet.Config will be used -->
        <!-- The official NuGet package source (https://www.nuget.org/api/v2/) will be excluded if package sources are specified and it does not appear in the list -->
        <!--
            <PackageSource Include="https://www.nuget.org/api/v2/" />
            <PackageSource Include="https://my-nuget-source/nuget/" />
        -->
    </ItemGroup>
RequireRestoreConsent = false
DownloadNuGetExe = true
PackageSource Include=�https://www.nuget.org/api/v2/
�

Code Analysis
I have started to use Code Analysis
ReSharper
Nuget
*.config/XML file transformation
Web.config transforms are built into Visual Studio. You can transform the Web.config file when publishing/deploying a Visual Studio web-application.
Web.config Transformation Syntax for Web Project Deployment Using Visual Studio: http://msdn.microsoft.com/en-us/library/dd465326(v=vs.110).aspx
SlowCheetah
Is a Visual Studio extension to handle transformation of any xml-file. And it transforms on build (F5)
SlowCheetah - XML Transforms: http://visualstudiogallery.msdn.microsoft.com/69023d00-a4f9-4a34-a6cd-7e854ba318b5
SlowCheetah on NuGet: http://www.nuget.org/packages/SlowCheetah/
SlowCheetah on GitHub: https://github.com/sayedihashimi/slow-cheetah