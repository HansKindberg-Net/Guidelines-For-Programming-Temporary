[English](/ReadMe.md)
# Riktlinjer f�r programmering

## Inneh�llsf�rteckning
- [1 Inledning](/ReadMe.sv.md#1-inledning)
	- [1.1 Denna l�snings struktur](/ReadMe.sv.md#11-denna-l%C3%B6snings-struktur)



	this is code by indenting

<code>this is code with code-tag</code>

`this is code with hyphens`

<pre>this is pre</pre>

<ul>
	<li>Hej
		<ul>
			<li>Hej</li>
			<li>Hej
				<ul>
					<li>Hej</li>
					<li>Hej</li>
				</ul>
			</li>
		</ul>
	</li>
	<li>Hej</li>
</ul>

##1 Inledning
Det h�r dokumentet inneh�ller riktlinjer f�r programmering i huvudsak f�r **.NET** och **Visual Studio**. Avsnittet **Testbarhet** kan dock appliceras p� andra programmeringsspr�k. Jag anser att avsnittet **Testbarhet** �r det viktigaste avsnittet och d�rf�r har jag valt att l�gga det f�rst.

###1.1 Denna l�snings struktur
**Riktlinjer f�r programmering** �r byggd som en **Visual Studio Solution**. Jag vill visa hur jag menar genom exempel. Projekten i denna solution �r strukturerade i **Solution Folders**:
* **Company-Samples** - inneh�ller ett projekt (classlibrary) med mer allm�n exempel-kod vad g�ller testbarhet + tillh�rande test-projekt
* **Company-Services** - inneh�ller WCF och WebService (asmx) projekt + tillh�rande test-projekt
* **Company-Shared** - inneh�ller generella funktioner f�r hela l�sningen
* **Company-Web** - inneh�ller webbapplikationer (MVC, MVP och traditionell Web Forms) + tillh�rande test-projekt
* **Company-Windows** - inneh�ller ett Windows-forms-application projekt + tillh�rande test-projekt

Jag anv�nder f�ljande namngivning p� test-projekten:
* [PROJEKTUNDERTEST].**IntegrationTests** - inneh�ller integrerade enhetstester d�r inte allt mockas
* [PROJEKTUNDERTEST].**ShimTests** - inneh�ller enhetstester d�r typer som beh�ver mockas inte �r mockbara utan **Microsoft Fakes** anv�nds ist�llet (se **1.1.1 Shim-tests och Microsoft Fakes**)
* [PROJEKTUNDERTEST].**UnitTests** - inneh�ller enhetstester d�r typer som beh�ver mockas �r mockbara

####1.1.1 Shim-tests och Microsoft Fakes
**Microsoft Fakes** kr�ver Visual Studio Premium/Ultimate 2012/2013. Om man har Visual Studio 2010 eller Visual Studio Professional 2012/2013 kan inte ett projekt d�r **Microsoft Fakes** anv�nds laddas. Om jag skulle blanda shim-tests med �vriga enhets-tester skulle inga enhets-test projekt g� att ladda med dessa versioner. Det �r d�rf�r jag gjort denna uppdelning.

###1.2 �vrigt
Jag �r systemutvecklare och utvecklar/programmerar i huvudsak EPiServer-l�sningar och andra webbapplikationer. Jag har mindre erfarenhet av .NET WCF, .NET WebServices, .NET Windows Forms, �nd� har jag velat ta med exempel inom dessa typer av applikationer.

##2. Testbarhet
Det finns olika m�l med testning ([SWEBOK - Chapter 5 Software Testing - Objectives of Testing](http://www.computer.org/portal/web/swebok/html/ch5#Ref2.2)). Detta avsnitt behandlar automatiserade/programmerbara funktionella tester, att skriva kod/programmera s� att en applikation blir m�jlig att automatiskt funktions-testa. 

Mjukvara kan testas p� p� olika niv�er ([Software testing - Testing levels](http://en.wikipedia.org/wiki/Software_testing#Testing_levels), [SWEBOK - Chapter 5 Software Testing - Test Levels](http://www.computer.org/portal/web/swebok/html/ch5#Ref2))
* **enhetstest** (unit test) � testa en minsta enhet, en metod/egenskap i en klass i ett system
* **integrationstest** (integration test) � testa funktionalitet mellan enheter
* **systemtest** (system test) � testa ett system som en helhet

F�r mig som programmerare handlar testbarhet mest om programmerbara/automatiska tester. Jag anser att begreppet testbarhet mest h�r ihop med enhetstester (unit tests). Bygg dina klasser s� att de blir testbara, vilket inneb�r att klassens beroenden �r abstrakta och injicerbara (injectable).

###2.1 F�rdelar
F�rdelar med testbarhet:
* **Pluggbara** system � system/mjukvara d�r det �r l�tt att byta ut olika delar
* System/mjukvara som kan k�ras i olika milj�er med olika f�ruts�ttningar, produktion, test, utveckling m.m.

�ven om man inte skriver/programmerar n�gra tester men skriver sin kod testbar s� anser jag att man f�r en bra mjukvaru-design. Jag anser ocks� att det kommer att resultera i att de klasser man skriver/programmerar hanterar det de ska, vilket resulterar i kod som �r l�ttare att underh�lla och man undviker redundant kod (duplicate code). Samtidigt kr�ver det mer av den som programmerar att se till att klasser underh�lls p� r�tt s�tt eftersom det �r mycket troligt att flera andra klasser har ett beroende till klassen. Med andra ord, bygger man testbart s� bygger man objekt-orienterat.

###2.2 Beroenden (dependencies)
Klasser har beroenden till andra klasser. Id�n med enhetstestning �r att testa kod utan att testa dess beroenden. Tanken �r att om en klass fungerar som den �r designad och dess beroende klasser likas� s� borde de fungera tillsammans som t�nkt.

###2.3 Hantera beroenden
F�r att kunna enhetstesta en metod i en klass som har ett beroende till en annan klass p� ett bra s�tt s� m�ste man kunna hantera detta beroende. Detta kan hanteras med hj�lp av:
* [**Inversion of control**](http://en.wikipedia.org/wiki/Inversion_of_control) - en programmerings teknik
* [**Dependency injection**](http://en.wikipedia.org/wiki/Dependency_injection) - ett design m�nster

Kortfattat inneb�r det att man inte h�rdkodar ett beroende till en annan klass utan man g�r det m�jligt att styra beroendet under k�rning.

F�ljande exempel visar en sv�rtestad metod ([se hela klassen](Company-Samples/Company.Samples/HardToTest/EmailForm.cs)):
<pre>
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
</pre>

Metoden ovan �r sv�r att testa i huvudsak f�r att den skapar en instans av typen SmtpClient och sedan kallar p� metoden Send(MailMessage mailMessage). Det finns bl.a tv� scenarior vi skulle vilja testa f�r denna metod:
1. Om ValidateInput() returnerar ett object d�r IsValid == true, s� ska Send(mailMessage) anropas.
2. Om ValidateInput() returnerar ett object d�r IsValid == false, s� ska den kasta ett fel och Send(mailMessage) ska inte anropas.

Om vi skippar t�nket p� god kod-design s� skulle vi kunna testa dessa tv� scenarior �nd� om vi har tillg�ng till n�got av f�ljande:
* **Microsoft Fakes** - kr�ver Visual Studio Premium/Ultimate 2012/2013 ([Isolating Code Under Test with Microsoft Fakes](http://msdn.microsoft.com/en-us/library/hh549175.aspx))
* [**Typemock Isolator**](http://www.typemock.com/isolator-product-page)
* [**Telerik JustMock**](http://www.telerik.com/products/mocking.aspx)

[Exempel med **Microsoft Fakes**](Company-Samples/Company.Samples.ShimTests/HardToTest/EmailFormTest.cs)

##3. Visual Studio

###3.1 NuGet
Anv�nd NuGet f�r att hantera referenser till external bibliotek. N�r du l�gger till **NuGet** paket s� hamnar paketen som standard i katalogen **packages** p� samma niv� som din solution-fil. Om du sl�r p� (enable) **NuGet Package Restore** s� kan utvecklare bygga din solution direkt efter att de �ppnat din solution fr�n **Source Control**. Alla paket som beh�vs laddas ner automatiskt vid f�rsta bygget (kan beh�va byggas 2 g�nger ibland f�r att det ska fungera). Det �r viktigt att inte checka in eventuella **NuGet** paket, f�r d� ser jag inte s� s� stor vits med **NuGet**. Om du dessutom korrigerar inst�llningarna (3.1.2 Korrigera NuGet.targets) s� beh�ver du inte ckecka in **NuGet.exe** heller, det laddas ocks� ner vid f�rsta bygget.

####3.1.1 Enable NuGet Package Restore
* I **Solution Explorer** h�gerklicka p� din **Solution**
* Klicka **Enable NuGet Package Restore**

F�ljande katalog och filer har nu skapats under rotkatalogen f�r din solution:
<pre>
.nuget
    NuGet.Config
    NuGet.exe
    NuGet.targets
</pre>

**.nuget** katalogen l�ggs �ven till som en **Solution Folder** i din solution s� att du kan se den i **Solution Explorer**.

####3.1.2 Korrigera NuGet.targets
I b�rjan p� **NuGet.targets** b�r det se ut s� h�r:
<pre>
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003"&gt;
    &lt;PropertyGroup&gt;
        ...

        &lt;!-- Determines if package restore consent is required to restore packages --&gt;
        &lt;RequireRestoreConsent Condition=" '$(RequireRestoreConsent)' != 'false' "&gt;true&lt;/RequireRestoreConsent&gt;

        &lt;!-- Download NuGet.exe if it does not already exist --&gt;
        &lt;DownloadNuGetExe Condition=" '$(DownloadNuGetExe)' == '' "&gt;false&lt;/DownloadNuGetExe&gt;
    &lt;/PropertyGroup&gt;

    &lt;ItemGroup Condition=" '$(PackageSources)' == '' "&gt;
        &lt;!-- Package sources used to restore packages. By default, registered sources under %APPDATA%\NuGet\NuGet.Config will be used --&gt;
        &lt;!-- The official NuGet package source (https://www.nuget.org/api/v2/) will be excluded if package sources are specified and it does not appear in the list --&gt;
        &lt;!--
            &lt;PackageSource Include="https://www.nuget.org/api/v2/" /&gt;
            &lt;PackageSource Include="https://my-nuget-source/nuget/" /&gt;
        --&gt;
    &lt;/ItemGroup&gt;
    ...
&lt;/Project&gt;
</pre>

�ndra till f�ljande:
* **RequireRestoreConsent** = false
* **DownloadNuGetExe** = true
* **PackageSource Include** = "https://www.nuget.org/api/v2/"

s� att det ser ut s� h�r:
<pre>
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;Project ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003"&gt;
    &lt;PropertyGroup&gt;
        ...

        &lt;!-- Determines if package restore consent is required to restore packages --&gt;
        &lt;RequireRestoreConsent Condition=" '$(RequireRestoreConsent)' != 'false' "&gt;false&lt;/RequireRestoreConsent&gt;

        &lt;!-- Download NuGet.exe if it does not already exist --&gt;
        &lt;DownloadNuGetExe Condition=" '$(DownloadNuGetExe)' == '' "&gt;true&lt;/DownloadNuGetExe&gt;
    &lt;/PropertyGroup&gt;

    &lt;ItemGroup Condition=" '$(PackageSources)' == '' "&gt;
        &lt;!-- Package sources used to restore packages. By default, registered sources under %APPDATA%\NuGet\NuGet.Config will be used --&gt;
        &lt;!-- The official NuGet package source (https://www.nuget.org/api/v2/) will be excluded if package sources are specified and it does not appear in the list --&gt;
        &lt;PackageSource Include="https://www.nuget.org/api/v2/" /&gt;
    &lt;/ItemGroup&gt;
    ...
&lt;/Project&gt;
</pre>

Det g�r �ven att l�gga till fler s�kv�gar till ytterligare **PackageSources**, om ni t.ex. har n�gon intern s�kv�g till era egna **NuGet** paket.

####3.1.3 Bygg NuGet paket av ett projekt
Om du vill skapa egna **NuGet** paket s� kan du g�ra det direkt n�r du bygger. I rooten p� det projekt som du vill skapa ett **NuGet** paket av l�gg till en xml-fil och d�p den till [PROJECTNAMN].nuspec, dvs. kopiera namnet p� projekt-filen och byt ut **csproj** mot **nuspec**. Den ska inneh�lla f�ljande:
<pre>
&lt;?xml version="1.0"?&gt;
&lt;package&gt;
    &lt;metadata&gt;
        &lt;id&gt;$id$&lt;/id&gt;
        &lt;version&gt;$version$&lt;/version&gt;
        &lt;title&gt;$title$&lt;/title&gt;
        &lt;authors&gt;$author$&lt;/authors&gt;
        &lt;owners&gt;$author$&lt;/owners&gt;
        &lt;requireLicenseAcceptance&gt;false&lt;/requireLicenseAcceptance&gt;
        &lt;description&gt;$description$&lt;/description&gt;
    &lt;/metadata&gt;
&lt;/package&gt;
</pre>

Alla v�rden som b�rjar och slutar med **$**, t.ex. **$author$**, �r s� kallade **Replacement Tokens** och kommer ers�ttas av v�rden fr�n **AssemblyInfo.cs**.

L�gg till ett **PostBuildEvent** i projektet:
* H�gerklicka ditt projekt i **Solution Explorer**
* V�lj fliken **Build Events**
* I f�ltet **Post-build event command line:** - l�gg till f�ljande: **"$(SolutionDir).nuget\NuGet.exe" pack "$(ProjectPath)" -Properties Configuration=$(ConfigurationName) -IncludeReferencedProjects**

N�r du bygger din solution/ditt projekt kommer du f� en [PROJEKTNAMN].[VERSION].nupkg i din **output** katalog f�r projektet.

Exempel i denna solution:
* [**Company.nuspec**](Company-Shared/Company/Company.nuspec)
* [**Company.csproj** - leta efter taggen **&lt;PostBuildEvent&gt;**](Company-Shared/Company/Company.csproj)

Du kan l�sa mer om **.nuspec**-filer h�r:
* [**Nuspec Reference**](http://docs.nuget.org/docs/reference/nuspec-reference)
* [**Replacement Tokens**](http://docs.nuget.org/docs/reference/nuspec-reference#Replacement_Tokens)






















###3.2 Code Analysis
I have started to use Code Analysis

###3.3 *.config transformering
*.config/XML file transformation
Web.config transforms are built into Visual Studio. You can transform the Web.config file when publishing/deploying a Visual Studio web-application.
Web.config Transformation Syntax for Web Project Deployment Using Visual Studio: http://msdn.microsoft.com/en-us/library/dd465326(v=vs.110).aspx
SlowCheetah
Is a Visual Studio extension to handle transformation of any xml-file. And it transforms on build (F5)
SlowCheetah - XML Transforms: http://visualstudiogallery.msdn.microsoft.com/69023d00-a4f9-4a34-a6cd-7e854ba318b5
SlowCheetah on NuGet: http://www.nuget.org/packages/SlowCheetah/
SlowCheetah on GitHub: https://github.com/sayedihashimi/slow-cheetah

###3.4 ReSharper