[English](/ReadMe.md)
# Riktlinjer för programmering

## Innehållsförteckning
- [1 Inledning](/ReadMe.sv.md#1-inledning)
    - [1.1 Ordlista](/ReadMe.sv.md#11-ordlista)
    - [1.2 Projekt-struktur](/ReadMe.sv.md#12-projekt-struktur)
    - [1.3 Namnkonvention](/ReadMe.sv.md#13-namnkonvention)
        - [VS-test-project](/ReadMe.sv.md#131-vs-test-project)
    - [1.4 Övrigt](/ReadMe.sv.md#14-%C3%96vrigt)
- [2 Testbarhet](/ReadMe.sv.md#2-testbarhet)

## 1 Inledning
Det här projektet innehåller riktlinjer för programmering. Riktlinjerna gäller i huvudsak för **.NET**, **C#** och **Visual Studio**. Avsnittet [**2 Testbarhet**](/ReadMe.sv.md#2-testbarhet) kan dock appliceras på andra programmeringsspråk. Jag anser att avsnittet [**2 Testbarhet**](/ReadMe.sv.md#2-testbarhet) är det viktigaste avsnittet och därför har jag valt att lägga det först.

### 1.1 Ordlista
- **VS-project** = **Visual Studio** project
- **VS-solution** = **Visual Studio** solution
- **VS-test-project** = ett **Visual Studio** project för att testa ett annat **Visual Studio** project

### 1.2 Projekt-struktur
Detta projekt består av en **VS-solution** med diverse **VS-project** med exempel kod. Programmeringsspråket som används är **C#**, **.NET Framework 4.5**. Jag vill visa hur jag menar genom exempel. **VS-solution** innehåller flera **VS-project** och därför har jag valt att gruppera/strukturera dem med hjälp av **Solution Folders**.
- **.nuget** - katalogen innehåller filer för **NuGet Package Restore**, dessa filer skapas när man slår på **NuGet Package Restore** ([3.1.1 Enable NuGet Package Restore](/ReadMe.sv.md#311-enable-nuget-package-restore))
- **CodeAnalysis** - globala filer/inställningar för **Code Analysis** ([Code Analysis for Managed Code Overview](http://msdn.microsoft.com/en-us/library/3z0aeatx.aspx)), länkas in av **VS-project**
- **Company-Console** - innehåller ett Windows-console-application projekt + tillhörande **VS-test-project**
- **Company-Examples** - innehåller ett **Class Library** (**VS-project**) med mer allmän exempel-kod vad gäller testbarhet + tillhörande **VS-test-project**
- **Company-Services** - innehåller WCF och WebService (asmx) projekt + tillhörande **VS-test-project**
- **Company-Shared** - innehåller generella funktioner för hela lösningen
- **Company-Web** - innehåller webbapplikationer (MVC, MVP och traditionell Web Forms) + tillhörande **VS-test-project**
- **Company-Windows** - innehåller ett Windows-forms-application projekt + tillhörande **VS-test-project**
- **Data** - innehåller en liten exempel-databas fil
- **Documents** - **ReadMe.md** filerna för detta projekt
- **Properties** - globala **Assembly** inställningar, länkas in av **VS-project**
- **Signing** - global **Strong Name Key** fil, länkas in av **VS-project**

### 1.3 Namnkonvention
Jag inleder alla **VS-project** namn med ansvarigt företags namn (eller organisation). I detta projekt har jag valt att inleda alla **VS-project** namn med **Company**. Resten av namnet bygger på funtions-namn eller produkt-namn.

#### 1.3.1 VS-test-project
Alla **VS-test-project** i den **VS-solution** som detta projekt består av är av typen **Unit Test Project**. Jag använder följande namngivning på **VS-test-project**:
- [VS-project som ska testas].**IntegrationTests** - innehåller integrerade enhetstester där inte allt mockas
- [VS-project som ska testas].**ShimTests** - innehåller enhetstester där typer som behöver mockas inte är mockbara utan [**Shims**](http://msdn.microsoft.com/en-us/library/hh549175.aspx#shims) ([**Microsoft Fakes**](http://msdn.microsoft.com/en-us/library/hh549175.aspx)) används istället ([Using shims to isolate your application from other assemblies for unit testing](http://msdn.microsoft.com/en-us/library/hh549176.aspx))
- [VS-project som ska testas].**UnitTests** - innehåller enhetstester där typer som behöver mockas är mockbara

[**Microsoft Fakes**](http://msdn.microsoft.com/en-us/library/hh549175.aspx) kräver Visual Studio Premium/Ultimate 2012/2013. Om man har Visual Studio 2010 eller Visual Studio Professional 2012/2013 kan inte ett VS-project där [**Microsoft Fakes**](http://msdn.microsoft.com/en-us/library/hh549175.aspx) används laddas. Om jag skulle blanda shim-tests med övriga enhets-tester skulle inga enhets-test projekt gå att ladda med dessa versioner. Det är därför jag gjort denna uppdelning.

### 1.4 Övrigt
Jag är systemutvecklare och utvecklar/programmerar i huvudsak EPiServer-lösningar och andra webbapplikationer. Jag har mindre erfarenhet av .NET WCF, .NET WebServices, .NET Windows Forms, ändå har jag velat ta med exempel inom dessa typer av applikationer. Jag anser mig heller inte expert på att skriva/programmera tester, däremot har jag byggt upp min kunskap på att göra programkod testbar.

## 2 Testbarhet
Det finns olika mål med testning ([SWEBOK - Chapter 5 Software Testing - Objectives of Testing](http://www.computer.org/portal/web/swebok/html/ch5#Ref2.2)). Detta avsnitt behandlar automatiserade/programmerbara funktionella tester, att skriva kod/programmera så att en applikation blir möjlig att automatiskt funktions-testa. 

Mjukvara kan testas på på olika nivåer ([Software testing - Testing levels](http://en.wikipedia.org/wiki/Software_testing#Testing_levels), [SWEBOK - Chapter 5 Software Testing - Test Levels](http://www.computer.org/portal/web/swebok/html/ch5#Ref2))
- **enhetstest** (unit test) – testa en minsta enhet, en metod/egenskap i en klass i ett system
- **integrationstest** (integration test) – testa funktionalitet mellan enheter
- **systemtest** (system test) – testa ett system som en helhet

För mig som programmerare handlar testbarhet mest om programmerbara/automatiska tester. Jag anser att begreppet testbarhet mest hör ihop med enhetstester (unit tests). Bygg dina klasser så att de blir testbara, vilket innebär att klassens beroenden är abstrakta och injicerbara (injectable).

### 2.1 Fördelar
Fördelar med testbarhet:
- **Pluggbara** system – system/mjukvara där det är lätt att byta ut olika delar
- System/mjukvara som kan köras i olika miljöer med olika förutsättningar, produktion, test, utveckling m.m.

Även om man inte skriver/programmerar några tester men skriver sin kod testbar så anser jag att man får en bra mjukvaru-design. Jag anser också att det kommer att resultera i att de klasser man skriver/programmerar hanterar det de ska, vilket resulterar i kod som är lättare att underhålla och man undviker redundant kod (duplicate code). Samtidigt kräver det mer av den som programmerar att se till att klasser underhålls på rätt sätt eftersom det är mycket troligt att flera andra klasser har ett beroende till klassen. Med andra ord, bygger man testbart så bygger man objekt-orienterat.

### 2.2 Beroenden (dependencies)
Klasser har beroenden till andra klasser. Idén med enhetstestning är att testa kod utan att testa dess beroenden. Tanken är att om en klass fungerar som den är designad och dess beroende klasser likaså så borde de fungera tillsammans som tänkt.

### 2.3 Hantera beroenden
För att kunna enhetstesta en metod i en klass som har ett beroende till en annan klass på ett bra sätt så måste man kunna hantera detta beroende. Detta kan hanteras med hjälp av:
- [**Dependency injection**](http://en.wikipedia.org/wiki/Dependency_injection) - ett design mönster
- [**Inversion of control**](http://en.wikipedia.org/wiki/Inversion_of_control) - en programmerings teknik

Mitt sätt att se det: [**Inversion of control**](http://en.wikipedia.org/wiki/Inversion_of_control) är en teknik man kan använda för att hantera [**Dependency injection**](http://en.wikipedia.org/wiki/Dependency_injection).

Kortfattat innebär det att man inte hårdkodar ett beroende till en annan klass utan man gör det möjligt att styra beroendet under körning. Martin Fowler har skrivit en artikel som behandlar detta område, [**Inversion of Control Containers and the Dependency Injection pattern**](http://martinfowler.com/articles/injection.html#ServiceLocatorVsDependencyInjection). Martin Fowler skriver också om fördelar/nackdelar med att använda respektive teknik/metod för att hantera beroenden:
- [Inversion of Control Containers and the Dependency Injection pattern - **Deciding which option to use**](http://martinfowler.com/articles/injection.html#DecidingWhichOptionToUse)
- [Inversion of Control Containers and the Dependency Injection pattern - **Service Locator vs Dependency Injection**](http://martinfowler.com/articles/injection.html#ServiceLocatorVsDependencyInjection)
- [Inversion of Control Containers and the Dependency Injection pattern - **Constructor versus Setter Injection**](http://martinfowler.com/articles/injection.html#ConstructorVersusSetterInjection)

Den teknik/metod som förespråkas mest är **Constructor Injection** vilket innebär att beroenden anges i konstruktorn för en klass. Det går alltså inte att instansiera klassen utan att ange dess beroenden. Detta är anledningen till att denna teknik/metod förespråkas, det blir tydligt för programmerare att en klass har beroenden.

#### [2.3.1 Dependency Injection (DI)](http://en.wikipedia.org/wiki/Dependency_injection)

#### [2.3.2 Inversion of Control (IoC)](http://en.wikipedia.org/wiki/Inversion_of_control)

##### 2.3.2.1 Inversion of Control Containers (IoC Containers)

### 2.4 Mock
Ett vanligt begrepp inom enhets-testning är **Mock** - *För att kunna utföra enhets-tester på en klass så mockar man dess beroenden*. Det finns ramverk att använda vid testning/enhets-testning som innehåller begreppet **Mock**:

- [**EasyMock.NET**](http://sourceforge.net/projects/easymocknet/)
- [**JustMock**](http://www.telerik.com/products/mocking.aspx)
- [**Moq**](http://www.moqthis.com/)
- [**NMock**](http://nmock3.codeplex.com/)
- [**Rhino Mocks**](http://hibernatingrhinos.com/oss/rhino-mocks)
- [**TypeMock Isolator**](http://www.typemock.com/)

Ska man vara korrekt så är **Mock** bara en del i ett vidare begrepp, [**Test Double**](http://en.wikipedia.org/wiki/Test_double).

Gerard Meszaros har definierat begrepp för olika typer av [**Test Double**](http://xunitpatterns.com/Test%20Double.html) som han kallar det:

- **Dummy object** - used when a parameter is needed for the tested method but without actually needing to use the parameter
- **Fake object** - used as a simpler implementation, e.g. using an in-memory database in the tests instead of doing real database access
- **Mock object** - used for verifying "indirect output" of the tested code, by first defining the expectations before the tested code is executed
- **Test spy** - used for verifying "indirect output" of the tested code, by asserting the expectations afterwards, without having defined the expectations before the tested code is executed
- **Test stub** - used for providing the tested code with "indirect input"

Vad det handlar om är att sätta upp egenskaper och förväntningar på beroenden och på så sätt testa olika scenarior för den klass som är under test. Det man använder mock-ramverken till är att skapa upp klasser dynamiskt under körningen av enhets-testet. Det kräver att beroenden är **mock-bara** (mitt begrepp):
- **Interface** - alla medlemmar (egenskaper/metoder) i ett interface är **alltid** mock-bara
- **Abstract** - abstracta medlemmar (egenskaper/metoder) är ofta mock-bara
- **Virtual (C#)** - virtuella medlemmar (egenskaper/metoder) är ofta mock-bara

**Interface** är alltid mock-bara eftersom de inte innehåller någon implementation. Abstrakta klasser, klasser med abstrakta medlemmar eller klasser med virtuella medlemmar är ofta mock-bara, men de behöver inte vara det. T.ex. så kan koden i konstrukorn för en abstrakt klass göra att den inte går att mocka.

Man kan även skriva sina **Test Doubles** själv men det underlättar om man använder ett ramverk.

#### 2.4.1 Mock the unmockable
Det finns **Mock**-ramverk som dock kan mocka det mesta:

- [**Microsoft Fakes**](http://msdn.microsoft.com/en-us/library/hh549175.aspx) - kräver Visual Studio Premium/Ultimate 2012/2013 ([Isolating Code Under Test with Microsoft Fakes](http://msdn.microsoft.com/en-us/library/hh549175.aspx))
- [**Typemock Isolator**](http://www.typemock.com/isolator-product-page)
- [**Telerik JustMock**](http://www.telerik.com/products/mocking.aspx)

Dessa ramverk fungerar genom att de går in och manipulerar vid bygget av en solution. De kan vara bra att använda när man inte har kontroll över beroenden till kod man vill testa. Men om man använder dessa ramverk rakt igenom så leder det inte till någon bättre mjukvaru-arkitektur. De kan t.ex. vara bra att använda något av dessa ramverk om man vill enhets-testa en **Wrapper**.

### 2.5 Design Patterns
För att kunna skriva testbar kod behöver man många gånger använda sig av **Design Patterns**. Vanliga mönster för att hantera dependency injection är:
- [**Adapter**](http://www.blackwasp.co.uk/Adapter.aspx) - koda **Wrappers** för att ge befintliga svår-mockade klasser ett användbart gränssnitt
- [**Factory Method**](http://www.blackwasp.co.uk/FactoryMethod.aspx) - ett mönster som används för att instansiera klasser (om en klass kräver en eller flera beroende-parametrar i sin konstruktor så kan det underlätta genom att ha en fabrik som instansierar objekt)

### 2.6 Exempel
Det vanligaste problemet med att enhets-testa kod är att beroenden i en klass är hårdkodade. Hårdkodade beroenden går inte att styra utifrån vilket man behöver kunna göra om man ska kunna enhets-testa. Om man inte har möjlighet att styra detta kan man använda sig av något av de ramverk som beskrivs under [2.4.1 Mock the unmockable](/ReadMe.sv.md#241-mock-the-unmockable). Men man bör sträva efter att kunna styra beroenden utifrån för att få en bra mjukvaru-arkitektur. Varje exempel som följer har en tillhörande test-class, även om det inte går att testa. I test-klassen står beskrivet vad man skulle vilja testa.

Exempel på kod som är svår att enhets-testa:
- [**ClassWithStaticDependency**](/Company-Examples/Company.Examples/Testability/HardToTest/ClassWithStaticDependency.cs): [ClassWithStaticDependencyTest](/Company-Examples/Company.Examples.UnitTests/Testability/HardToTest/ClassWithStaticDependencyTest.cs)




Om vi skippar tänket på god kod-design så skulle vi kunna testa dessa två scenarier ändå om vi har tillgång till något av följande:



Test för scenario 1, löst med [**Shims**](http://msdn.microsoft.com/en-us/library/hh549175.aspx#shims) ([/Company-Examples/Company.Examples.ShimTests/HardToTest/EmailFormTest.cs](/Company-Examples/Company.Examples.ShimTests/HardToTest/EmailFormTest.cs)):








	












## 3. Visual Studio

### 3.1 NuGet
Använd NuGet för att hantera referenser till external bibliotek. När du lägger till **NuGet** paket så hamnar paketen som standard i katalogen **packages** på samma nivå som din VS-solution-fil. Om du slår på (enable) **NuGet Package Restore** så kan utvecklare bygga din VS-solution direkt efter att de öppnat din VS-solution från **Source Control**. Alla paket som behövs laddas ner automatiskt vid första bygget (kan behöva byggas 2 gånger ibland för att det ska fungera). Det är viktigt att inte checka in eventuella **NuGet** paket, för då ser jag inte så så stor vits med **NuGet**. Om du dessutom korrigerar inställningarna ([3.1.2 Korrigera NuGet.targets](/ReadMe.sv.md#312-korrigera-nugettargets)) så:

- behöver du inte ckecka in/commita **NuGet.exe** heller, det laddas också ner vid första bygget.
- behöver inte andra utvecklare/programmerare som öppnar din VS-solution från **Source Control** ha **NuGet Package Manager** installerat över huvudtaget eller inte konfigurerat på samma sätt som dig för att de ändå ska kunna bygga VS-solution

#### 3.1.1 Enable NuGet Package Restore
- I **Solution Explorer** högerklicka på din **Solution**
- Klicka **Enable NuGet Package Restore**

Följande katalog och filer har nu skapats under rotkatalogen för din VS-solution:

	.nuget
		NuGet.Config
		NuGet.exe
		NuGet.targets

**.nuget** katalogen läggs även till som en **Solution Folder** i din VS-solution så att du kan se den i **Solution Explorer**.

#### 3.1.2 Korrigera NuGet.targets
I början på **NuGet.targets** bör det se ut så här:

	<?xml version="1.0" encoding="utf-8"?>
	<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
		<PropertyGroup>
			...

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
		...
	</Project>

Ändra till följande:

- **RequireRestoreConsent** = false
- **DownloadNuGetExe** = true
- **PackageSource Include** = "https://www.nuget.org/api/v2/"

så att det ser ut så här:

	<?xml version="1.0" encoding="utf-8"?>
	<Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
		<PropertyGroup>
			...

			<!-- Determines if package restore consent is required to restore packages -->
			<RequireRestoreConsent Condition=" '$(RequireRestoreConsent)' != 'false' ">false</RequireRestoreConsent>

			<!-- Download NuGet.exe if it does not already exist -->
			<DownloadNuGetExe Condition=" '$(DownloadNuGetExe)' == '' ">true</DownloadNuGetExe>
		</PropertyGroup>

		<ItemGroup Condition=" '$(PackageSources)' == '' ">
			<!-- Package sources used to restore packages. By default, registered sources under %APPDATA%\NuGet\NuGet.Config will be used -->
			<!-- The official NuGet package source (https://www.nuget.org/api/v2/) will be excluded if package sources are specified and it does not appear in the list -->
			<PackageSource Include="https://www.nuget.org/api/v2/" />
		</ItemGroup>
		...
	</Project>

Det går även att lägga till fler sökvägar till ytterligare **PackageSources**, om ni t.ex. har någon intern sökväg till era egna **NuGet** paket.

#### 3.1.3 Bygg NuGet paket av ett projekt
Om du vill skapa egna **NuGet** paket så kan du göra det direkt när du bygger. I rooten på det VS-project som du vill skapa ett **NuGet** paket av lägg till en xml-fil och döp den till [VS-project namn].nuspec, dvs. kopiera namnet på projekt-filen och byt ut **csproj** mot **nuspec**. Den kan t.ex. se ut så här:

	<?xml version="1.0"?>
	<package>
		<metadata>
			<id>$id$</id>
			<version>$version$</version>
			<title>$title$</title>
			<authors>$author$</authors>
			<owners>$author$</owners>
			<requireLicenseAcceptance>false</requireLicenseAcceptance>
			<description>$description$</description>
		</metadata>
	</package>

Alla värden som börjar och slutar med **$**, t.ex. **$author$**, är så kallade **Replacement Tokens** och kommer ersättas av värden från **AssemblyInfo.cs**.

Lägg till ett **PostBuildEvent** i projektet:

- Högerklicka ditt VS-project i **Solution Explorer**
- Välj fliken **Build Events**
- I fältet **Post-build event command line:** - lägg till följande: **"$(SolutionDir).nuget\NuGet.exe" pack "$(ProjectPath)" -Properties Configuration=$(ConfigurationName) -IncludeReferencedProjects**

När du bygger din VS-solution/ditt VS-project kommer du få en [VS-project namn].[version].nupkg i din **output** katalog för ditt VS-project.

Exempel i detta projekt:

- [**/Company-Shared/Company/Company.nuspec**](/Company-Shared/Company/Company.nuspec)
- [**/Company-Shared/Company/Company.csproj** - leta efter taggen **&lt;PostBuildEvent&gt;**](/Company-Shared/Company/Company.csproj)

Du kan läsa mer om **.nuspec**-filer här:

- [**Nuspec Reference**](http://docs.nuget.org/docs/reference/nuspec-reference)
- [**Replacement Tokens**](http://docs.nuget.org/docs/reference/nuspec-reference#Replacement_Tokens)






















### 3.2 Code Analysis
I have started to use Code Analysis

### 3.3 *.config transformering
*.config/XML file transformation
Web.config transforms are built into Visual Studio. You can transform the Web.config file when publishing/deploying a Visual Studio webapplication.
Web.config Transformation Syntax for Web Project Deployment Using Visual Studio: http://msdn.microsoft.com/en-us/library/dd465326(v=vs.110).aspx
SlowCheetah
Is a Visual Studio extension to handle transformation of any xml-file. And it transforms on build (F5)
SlowCheetah - XML Transforms: http://visualstudiogallery.msdn.microsoft.com/69023d00-a4f9-4a34-a6cd-7e854ba318b5
SlowCheetah on NuGet: http://www.nuget.org/packages/SlowCheetah/
SlowCheetah on GitHub: https://github.com/sayedihashimi/slow-cheetah

### 3.4 ReSharper

### 3.5 Solution Folder
I Visual Studio kan man skapa kataloger på solution-nivå. Om man högerklickar VS-solution i **Solution Explorer** får man upp valet **Add** -> **New Solution Folder**.
När man skapar en **Solution Folder** skapas inte en katalog på disk. Lägger man till en fil, genom att högerklicka på katalogen och väljer **Add** -> **New Item** så kommer den nya filen visuellt att ligga i den **Solution Folder** man valde att lägga till filen in men fysiskt så hamnar filen i rotkatalogen för VS-solution.
Man kan korrigera så att den visuella strukturen stämmer överens med den fysiska strukturen men det kräver manuella åtgärder. Exempel:
1. Lägg till en **Solution Folder**.
2. Lägg till en katalog på disk i rot-katalogen för din VS-solution och ge den samma namn.
3. Lägg till en fil från Visual Studio.
4. Ta bort samma fil från Visual Studio (den kommer bara att tas bort visuellt i Visual Studio, inte fysiskt på disk).
5. Flytta den fysiska filen (som du tog bort i steget innan) från rot-katalogen till den aktuella fysiska katalogen på disk.
6. Välj **Add** -> **Existing Item** genom att högerklicka på katalogen i **Solution Explorer**.
Detta gäller även sub-kataloger. Om man nu lägger till den VS-solution till source-control så kommer katalogstrukturen för all VS-solution kataloger vara den samma både fysiskt och visuellt i VS-solution.


