[English](ReadMe.md)
#Riktlinjer för programmering

##1. Inledning
Det här dokumentet innehåller riktlinjer för programmering i huvudsak för **.NET** och **Visual Studio**. Avsnittet **Testbarhet** kan dock appliceras på andra programmeringsspråk. Jag anser att avsnittet **Testbarhet** är det viktigaste avsnittet och därför har jag valt att lägga det först.

##2. Testbarhet
(Testbarhet innehåller riktlinjer för att skriva testbar-kod. Leder till kod/mjukvara som blir mer plugbar)

Mjukvara kan testas på olika sätt, på olika nivåer, http://en.wikipedia.org/wiki/Software_testing#Testing_levels.
* enhetstest (unit test) – testa en minsta enhet, en metod/egenskap i en klass i ett system
* integrationstest (integration test) – testa funktionalitet mellan enheter
* systemtest (system test) – testa ett helt system
För mig som programmerare handlar testbarhet mest om programmerbara/automatiska tester. Jag anser att begreppet testbarhet mest hör ihop med enhetstester (unit tests). Bygg dina klasser så att de blir testbara, vilket innebär att klassens beroenden är abstrakta och injicerbara (injectable).

###2.1 Fördelar
Fördelar med testbarhet:
* **Pluggbara** system – system/mjukvara där det är lätt att byta ut olika delar
* System/mjukvara som kan köras i olika miljöer med olika förutsättningar, produktion, test, utveckling m.m.

Även om man inte skriver/programmerar några tester men skriver sin kod testbar så anser jag att man får en bra mjukvaru-design. Jag anser också att det kommer att resultera i att de klasser man skriver/programmerar hanterar det de ska, vilket resulterar i kod som är lättare att underhålla och man undviker redundant kod (duplicate code). Samtidigt kräver det mer av den som programmerar att se till att klasser underhålls på rätt sätt eftersom det är mycket troligt att flera andra klasser har ett beroende till klassen.

###2.2 Beroenden (dependencies) – dependency injection
Klasser har beroenden till andra klasser. Idén med enhetstestning är att testa kod utan att testa dess beroenden. Tanken är att om en klass fungerar som den är designad och dess beroende klasser likaså så borde de fungera tillsammans som tänkt.

##3. Visual Studio
###3.1 NuGet
Enable NuGet Package Restore
In the Solution Explorer
Right click you solution
Click Enable NuGet Package Restore
Följande katalog och filer har nu skapats under rotkatalogen för din solution:
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
PackageSource Include=”https://www.nuget.org/api/v2/
”

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