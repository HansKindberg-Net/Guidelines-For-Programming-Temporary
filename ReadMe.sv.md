[English](/ReadMe.md)
# Riktlinjer f�r programmering

## Inneh�llsf�rteckning
- [1 Inledning](/ReadMe.sv.md#1-inledning)
    - [1.1 Ordlista](/ReadMe.sv.md#11-ordlista)
    - [1.2 Projekt-struktur](/ReadMe.sv.md#12-projekt-struktur)
    - [1.3 Namnkonvention](/ReadMe.sv.md#13-namnkonvention)
        - [VS-test-project](/ReadMe.sv.md#131-vs-test-project)
    - [1.4 �vrigt](/ReadMe.sv.md#14-%C3%96vrigt)
- [2 Testbarhet](/ReadMe.sv.md#2-testbarhet)

## 1 Inledning
Det h�r projektet inneh�ller riktlinjer f�r programmering. Riktlinjerna g�ller i huvudsak f�r **.NET**, **C#** och **Visual Studio**. Avsnittet [**2 Testbarhet**](/ReadMe.sv.md#2-testbarhet) kan dock appliceras p� andra programmeringsspr�k. Jag anser att avsnittet [**2 Testbarhet**](/ReadMe.sv.md#2-testbarhet) �r det viktigaste avsnittet och d�rf�r har jag valt att l�gga det f�rst.

### 1.1 Ordlista
- **VS-project** = **Visual Studio** project
- **VS-solution** = **Visual Studio** solution
- **VS-test-project** = ett **Visual Studio** project f�r att testa ett annat **Visual Studio** project

### 1.2 Projekt-struktur
Detta projekt best�r av en **VS-solution** med diverse **VS-project** med exempel kod. Programmeringsspr�ket som anv�nds �r **C#**. Jag vill visa hur jag menar genom exempel. **VS-solution** inneh�ller flera **VS-project** och d�rf�r har jag valt att gruppera/strukturera dem med hj�lp av **Solution Folders**.
- **.nuget** - katalogen inneh�ller filer f�r **NuGet Package Restore**, dessa filer skapas n�r man sl�r p� **NuGet Package Restore** ([3.1.1 Enable NuGet Package Restore](/ReadMe.sv.md#311-enable-nuget-package-restore))
- **CodeAnalysis** - globala filer/inst�llningar f�r **Code Analysis** ([Code Analysis for Managed Code Overview](http://msdn.microsoft.com/en-us/library/3z0aeatx.aspx)), l�nkas in av **VS-project**
- **Company-Examples** - inneh�ller ett **Class Library** (**VS-project**) med mer allm�n exempel-kod vad g�ller testbarhet + tillh�rande **VS-test-project**
- **Company-Services** - inneh�ller WCF och WebService (asmx) projekt + tillh�rande **VS-test-project**
- **Company-Shared** - inneh�ller generella funktioner f�r hela l�sningen
- **Company-Web** - inneh�ller webbapplikationer (MVC, MVP och traditionell Web Forms) + tillh�rande **VS-test-project**
- **Company-Windows** - inneh�ller ett Windows-forms-application projekt + tillh�rande **VS-test-project**
- **Documents** - **ReadMe.md** filerna f�r detta projekt
- **Properties** - globala **Assembly** inst�llningar, l�nkas in av **VS-project**
- **Signing** - global **Strong Name Key** fil, l�nkas in av **VS-project**

### 1.3 Namnkonvention
Jag inleder alla **VS-project** namn med ansvarigt f�retags namn (eller organisation). I detta projekt har jag valt att inleda alla **VS-project** namn med **Company**. Resten av namnet bygger p� funtions-namn eller produkt-namn.

#### 1.3.1 VS-test-project
Jag anv�nder f�ljande namngivning p� **VS-test-project**:
- [VS-project som ska testas].**IntegrationTests** - inneh�ller integrerade enhetstester d�r inte allt mockas
- [VS-project som ska testas].**ShimTests** - inneh�ller enhetstester d�r typer som beh�ver mockas inte �r mockbara utan [**Shims**](http://msdn.microsoft.com/en-us/library/hh549175.aspx#shims) ([**Microsoft Fakes**](http://msdn.microsoft.com/en-us/library/hh549175.aspx)) anv�nds ist�llet ([Using shims to isolate your application from other assemblies for unit testing](http://msdn.microsoft.com/en-us/library/hh549176.aspx))
- [VS-project som ska testas].**UnitTests** - inneh�ller enhetstester d�r typer som beh�ver mockas �r mockbara

[**Microsoft Fakes**](http://msdn.microsoft.com/en-us/library/hh549175.aspx) kr�ver Visual Studio Premium/Ultimate 2012/2013. Om man har Visual Studio 2010 eller Visual Studio Professional 2012/2013 kan inte ett VS-project d�r [**Microsoft Fakes**](http://msdn.microsoft.com/en-us/library/hh549175.aspx) anv�nds laddas. Om jag skulle blanda shim-tests med �vriga enhets-tester skulle inga enhets-test projekt g� att ladda med dessa versioner. Det �r d�rf�r jag gjort denna uppdelning.

### 1.4 �vrigt
Jag �r systemutvecklare och utvecklar/programmerar i huvudsak EPiServer-l�sningar och andra webbapplikationer. Jag har mindre erfarenhet av .NET WCF, .NET WebServices, .NET Windows Forms, �nd� har jag velat ta med exempel inom dessa typer av applikationer. Jag anser mig heller inte expert p� att skriva/programmera tester, d�remot har jag byggt upp min kunskap p� att g�ra programkod testbar.

## 2 Testbarhet
Det finns olika m�l med testning ([SWEBOK - Chapter 5 Software Testing - Objectives of Testing](http://www.computer.org/portal/web/swebok/html/ch5#Ref2.2)). Detta avsnitt behandlar automatiserade/programmerbara funktionella tester, att skriva kod/programmera s� att en applikation blir m�jlig att automatiskt funktions-testa. 

Mjukvara kan testas p� p� olika niv�er ([Software testing - Testing levels](http://en.wikipedia.org/wiki/Software_testing#Testing_levels), [SWEBOK - Chapter 5 Software Testing - Test Levels](http://www.computer.org/portal/web/swebok/html/ch5#Ref2))
- **enhetstest** (unit test) � testa en minsta enhet, en metod/egenskap i en klass i ett system
- **integrationstest** (integration test) � testa funktionalitet mellan enheter
- **systemtest** (system test) � testa ett system som en helhet

F�r mig som programmerare handlar testbarhet mest om programmerbara/automatiska tester. Jag anser att begreppet testbarhet mest h�r ihop med enhetstester (unit tests). Bygg dina klasser s� att de blir testbara, vilket inneb�r att klassens beroenden �r abstrakta och injicerbara (injectable).

###2.1 F�rdelar
F�rdelar med testbarhet:
- **Pluggbara** system � system/mjukvara d�r det �r l�tt att byta ut olika delar
- System/mjukvara som kan k�ras i olika milj�er med olika f�ruts�ttningar, produktion, test, utveckling m.m.

�ven om man inte skriver/programmerar n�gra tester men skriver sin kod testbar s� anser jag att man f�r en bra mjukvaru-design. Jag anser ocks� att det kommer att resultera i att de klasser man skriver/programmerar hanterar det de ska, vilket resulterar i kod som �r l�ttare att underh�lla och man undviker redundant kod (duplicate code). Samtidigt kr�ver det mer av den som programmerar att se till att klasser underh�lls p� r�tt s�tt eftersom det �r mycket troligt att flera andra klasser har ett beroende till klassen. Med andra ord, bygger man testbart s� bygger man objekt-orienterat.

###2.2 Beroenden (dependencies)
Klasser har beroenden till andra klasser. Id�n med enhetstestning �r att testa kod utan att testa dess beroenden. Tanken �r att om en klass fungerar som den �r designad och dess beroende klasser likas� s� borde de fungera tillsammans som t�nkt.

###2.3 Hantera beroenden
F�r att kunna enhetstesta en metod i en klass som har ett beroende till en annan klass p� ett bra s�tt s� m�ste man kunna hantera detta beroende. Detta kan hanteras med hj�lp av:
- [**Inversion of control**](http://en.wikipedia.org/wiki/Inversion_of_control) - en programmerings teknik
- [**Dependency injection**](http://en.wikipedia.org/wiki/Dependency_injection) - ett design m�nster

Kortfattat inneb�r det att man inte h�rdkodar ett beroende till en annan klass utan man g�r det m�jligt att styra beroendet under k�rning.

F�ljande exempel visar en sv�rtestad metod ([/Company-Examples/Company.Examples/HardToTest/EmailForm.cs](/Company-Examples/Company.Examples/HardToTest/EmailForm.cs)):

	public void Send()
	{
		IValidationResult validationResult = this.ValidateInput();

		if(!validationResult.IsValid)
			throw validationResult.Exceptions.First();

		using(MailMessage mailMessage = new MailMessage("noreply@company.net", this.To))
		{
			mailMessage.Body = this.Message;
			mailMessage.Subject = this.Subject;

			using(SmtpClient smtpClient = new SmtpClient())
			{
				smtpClient.Send(mailMessage);
			}
		}
	}

Metoden ovan �r sv�r att testa i huvudsak f�r att den skapar en instans av typen SmtpClient och sedan kallar p� metoden Send(MailMessage mailMessage). Det finns bl.a tv� scenarier vi skulle vilja testa f�r denna metod:

1. Om ValidateInput() returnerar ett object d�r IsValid == true, s� ska Send(mailMessage) anropas.
2. Om ValidateInput() returnerar ett object d�r IsValid == false, s� ska den kasta ett fel och Send(mailMessage) ska inte anropas.

Om vi skippar t�nket p� god kod-design s� skulle vi kunna testa dessa tv� scenarier �nd� om vi har tillg�ng till n�got av f�ljande:

- [**Microsoft Fakes**](http://msdn.microsoft.com/en-us/library/hh549175.aspx) - kr�ver Visual Studio Premium/Ultimate 2012/2013 ([Isolating Code Under Test with Microsoft Fakes](http://msdn.microsoft.com/en-us/library/hh549175.aspx))
- [**Typemock Isolator**](http://www.typemock.com/isolator-product-page)
- [**Telerik JustMock**](http://www.telerik.com/products/mocking.aspx)

Test f�r scenario 1, l�st med [**Shims**](http://msdn.microsoft.com/en-us/library/hh549175.aspx#shims) ([/Company-Examples/Company.Examples.ShimTests/HardToTest/EmailFormTest.cs](/Company-Examples/Company.Examples.ShimTests/HardToTest/EmailFormTest.cs)):

	[TestMethod]
	public void Send_IfTheInputIsValid_ShouldCallSmtpClientSend()
	{
		using(ShimsContext.Create())
		{
			bool sendIsCalled = false;
			MailMessage sentMailMessage = null;

			ShimSmtpClient.AllInstances.SendMailMessage = delegate(SmtpClient client, MailMessage mailMessage)
			{
				sentMailMessage = mailMessage;
				sendIsCalled = true;
			};

			Assert.IsFalse(sendIsCalled);
			Assert.IsNull(sentMailMessage);

			EmailForm emailForm = new EmailForm
				{
					Message = _testMessage,
					Subject = _testSubject,
					To = _testReceiver
				};

			emailForm.Send();

			Assert.IsTrue(sendIsCalled);
			Assert.IsNotNull(sentMailMessage);
			Assert.AreEqual(_testMessage, sentMailMessage.Body);
			Assert.AreEqual(_testReceiver, sentMailMessage.To.First().Address);
			Assert.AreEqual(_testSubject, sentMailMessage.Subject);
		}
	}

Test f�r scenario 2, l�st med [**Shims**](http://msdn.microsoft.com/en-us/library/hh549175.aspx#shims) ([/Company-Examples/Company.Examples.ShimTests/HardToTest/EmailFormTest.cs](/Company-Examples/Company.Examples.ShimTests/HardToTest/EmailFormTest.cs)) :

	[TestMethod]
	[ExpectedException(typeof(Exception))]
	public void Send_IfTheInputIsNotValid_ShouldNotCallSmtpClientSendAndShouldThrowAnException()
	{
		using(ShimsContext.Create())
		{
			bool sendIsCalled = false;
			MailMessage sentMailMessage = null;

			ShimSmtpClient.AllInstances.SendMailMessage = delegate(SmtpClient client, MailMessage mailMessage)
			{
				sentMailMessage = mailMessage;
				sendIsCalled = true;
			};

			Assert.IsFalse(sendIsCalled);
			Assert.IsNull(sentMailMessage);

			EmailForm emailForm = new EmailForm
				{
					Message = _testMessage,
					Subject = _testSubject,
					To = null
				};

			Exception expectedException = null;

			try
			{
				emailForm.Send();
			}
			catch(Exception exception)
			{
				expectedException = exception;
			}

			Assert.IsFalse(sendIsCalled);
			Assert.IsNull(sentMailMessage);

			if(expectedException != null)
				throw new Exception();
		}
	}

## 3. Visual Studio

### 3.1 NuGet
Anv�nd NuGet f�r att hantera referenser till external bibliotek. N�r du l�gger till **NuGet** paket s� hamnar paketen som standard i katalogen **packages** p� samma niv� som din VS-solution-fil. Om du sl�r p� (enable) **NuGet Package Restore** s� kan utvecklare bygga din VS-solution direkt efter att de �ppnat din VS-solution fr�n **Source Control**. Alla paket som beh�vs laddas ner automatiskt vid f�rsta bygget (kan beh�va byggas 2 g�nger ibland f�r att det ska fungera). Det �r viktigt att inte checka in eventuella **NuGet** paket, f�r d� ser jag inte s� s� stor vits med **NuGet**. Om du dessutom korrigerar inst�llningarna ([3.1.2 Korrigera NuGet.targets](/ReadMe.sv.md#312-korrigera-nugettargets)) s�:

- beh�ver du inte ckecka in/commita **NuGet.exe** heller, det laddas ocks� ner vid f�rsta bygget.
- beh�ver inte andra utvecklare/programmerare som �ppnar din VS-solution fr�n **Source Control** ha **NuGet Package Manager** installerat �ver huvudtaget eller inte konfigurerat p� samma s�tt som dig f�r att de �nd� ska kunna bygga VS-solution

#### 3.1.1 Enable NuGet Package Restore
- I **Solution Explorer** h�gerklicka p� din **Solution**
- Klicka **Enable NuGet Package Restore**

F�ljande katalog och filer har nu skapats under rotkatalogen f�r din VS-solution:

	.nuget
		NuGet.Config
		NuGet.exe
		NuGet.targets

**.nuget** katalogen l�ggs �ven till som en **Solution Folder** i din VS-solution s� att du kan se den i **Solution Explorer**.

#### 3.1.2 Korrigera NuGet.targets
I b�rjan p� **NuGet.targets** b�r det se ut s� h�r:

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

�ndra till f�ljande:

- **RequireRestoreConsent** = false
- **DownloadNuGetExe** = true
- **PackageSource Include** = "https://www.nuget.org/api/v2/"

s� att det ser ut s� h�r:

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

Det g�r �ven att l�gga till fler s�kv�gar till ytterligare **PackageSources**, om ni t.ex. har n�gon intern s�kv�g till era egna **NuGet** paket.

#### 3.1.3 Bygg NuGet paket av ett projekt
Om du vill skapa egna **NuGet** paket s� kan du g�ra det direkt n�r du bygger. I rooten p� det VS-project som du vill skapa ett **NuGet** paket av l�gg till en xml-fil och d�p den till [VS-project namn].nuspec, dvs. kopiera namnet p� projekt-filen och byt ut **csproj** mot **nuspec**. Den kan t.ex. se ut s� h�r:

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

Alla v�rden som b�rjar och slutar med **$**, t.ex. **$author$**, �r s� kallade **Replacement Tokens** och kommer ers�ttas av v�rden fr�n **AssemblyInfo.cs**.

L�gg till ett **PostBuildEvent** i projektet:

- H�gerklicka ditt VS-project i **Solution Explorer**
- V�lj fliken **Build Events**
- I f�ltet **Post-build event command line:** - l�gg till f�ljande: **"$(SolutionDir).nuget\NuGet.exe" pack "$(ProjectPath)" -Properties Configuration=$(ConfigurationName) -IncludeReferencedProjects**

N�r du bygger din VS-solution/ditt VS-project kommer du f� en [VS-project namn].[version].nupkg i din **output** katalog f�r ditt VS-project.

Exempel i detta projekt:

- [**/Company-Shared/Company/Company.nuspec**](/Company-Shared/Company/Company.nuspec)
- [**/Company-Shared/Company/Company.csproj** - leta efter taggen **&lt;PostBuildEvent&gt;**](/Company-Shared/Company/Company.csproj)

Du kan l�sa mer om **.nuspec**-filer h�r:

- [**Nuspec Reference**](http://docs.nuget.org/docs/reference/nuspec-reference)
- [**Replacement Tokens**](http://docs.nuget.org/docs/reference/nuspec-reference#Replacement_Tokens)






















### 3.2 Code Analysis
I have started to use Code Analysis

### 3.3 *.config transformering
*.config/XML file transformation
Web.config transforms are built into Visual Studio. You can transform the Web.config file when publishing/deploying a Visual Studio web-application.
Web.config Transformation Syntax for Web Project Deployment Using Visual Studio: http://msdn.microsoft.com/en-us/library/dd465326(v=vs.110).aspx
SlowCheetah
Is a Visual Studio extension to handle transformation of any xml-file. And it transforms on build (F5)
SlowCheetah - XML Transforms: http://visualstudiogallery.msdn.microsoft.com/69023d00-a4f9-4a34-a6cd-7e854ba318b5
SlowCheetah on NuGet: http://www.nuget.org/packages/SlowCheetah/
SlowCheetah on GitHub: https://github.com/sayedihashimi/slow-cheetah

### 3.4 ReSharper