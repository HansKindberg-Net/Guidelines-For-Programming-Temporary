[English](/ReadMe.md)
# Riktlinjer för programmering

## Innehållsförteckning
[1 Inledning](/ReadMe.sv.md#1-inledning)

&nbsp;&nbsp;&nbsp;&nbsp;[1.1 Denna lösnings struktur](/ReadMe.sv.md#11-denna-l%C3%B6snings-struktur)



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
Det här dokumentet innehåller riktlinjer för programmering i huvudsak för **.NET** och **Visual Studio**. Avsnittet **Testbarhet** kan dock appliceras på andra programmeringsspråk. Jag anser att avsnittet **Testbarhet** är det viktigaste avsnittet och därför har jag valt att lägga det först.

###1.1 Denna lösnings struktur
**Riktlinjer för programmering** är byggd som en **Visual Studio Solution**. Jag vill visa hur jag menar genom exempel. Projekten i denna solution är strukturerade i **Solution Folders**:
* **Company-Samples** - innehåller ett projekt (classlibrary) med mer allmän exempel-kod vad gäller testbarhet + tillhörande test-projekt
* **Company-Services** - innehåller WCF och WebService (asmx) projekt + tillhörande test-projekt
* **Company-Shared** - innehåller generella funktioner för hela lösningen
* **Company-Web** - innehåller webbapplikationer (MVC, MVP och traditionell Web Forms) + tillhörande test-projekt
* **Company-Windows** - innehåller ett Windows-forms-application projekt + tillhörande test-projekt

Jag använder följande namngivning på test-projekten:
* [PROJEKTUNDERTEST].**IntegrationTests** - innehåller integrerade enhetstester där inte allt mockas
* [PROJEKTUNDERTEST].**ShimTests** - innehåller enhetstester där typer som behöver mockas inte är mockbara utan **Microsoft Fakes** används istället (se **1.1.1 Shim-tests och Microsoft Fakes**)
* [PROJEKTUNDERTEST].**UnitTests** - innehåller enhetstester där typer som behöver mockas är mockbara

####1.1.1 Shim-tests och Microsoft Fakes
**Microsoft Fakes** kräver Visual Studio Premium/Ultimate 2012/2013. Om man har Visual Studio 2010 eller Visual Studio Professional 2012/2013 kan inte ett projekt där **Microsoft Fakes** används laddas. Om jag skulle blanda shim-tests med övriga enhets-tester skulle inga enhets-test projekt gå att ladda med dessa versioner. Det är därför jag gjort denna uppdelning.

###1.2 Övrigt
Jag är systemutvecklare och utvecklar/programmerar i huvudsak EPiServer-lösningar och andra webbapplikationer. Jag har mindre erfarenhet av .NET WCF, .NET WebServices, .NET Windows Forms, ändå har jag velat ta med exempel inom dessa typer av applikationer.

##2. Testbarhet
Det finns olika mål med testning ([SWEBOK - Chapter 5 Software Testing - Objectives of Testing](http://www.computer.org/portal/web/swebok/html/ch5#Ref2.2)). Detta avsnitt behandlar automatiserade/programmerbara funktionella tester, att skriva kod/programmera så att en applikation blir möjlig att automatiskt funktions-testa. 

Mjukvara kan testas på på olika nivåer ([Software testing - Testing levels](http://en.wikipedia.org/wiki/Software_testing#Testing_levels), [SWEBOK - Chapter 5 Software Testing - Test Levels](http://www.computer.org/portal/web/swebok/html/ch5#Ref2))
* **enhetstest** (unit test) – testa en minsta enhet, en metod/egenskap i en klass i ett system
* **integrationstest** (integration test) – testa funktionalitet mellan enheter
* **systemtest** (system test) – testa ett system som en helhet

För mig som programmerare handlar testbarhet mest om programmerbara/automatiska tester. Jag anser att begreppet testbarhet mest hör ihop med enhetstester (unit tests). Bygg dina klasser så att de blir testbara, vilket innebär att klassens beroenden är abstrakta och injicerbara (injectable).

###2.1 Fördelar
Fördelar med testbarhet:
* **Pluggbara** system – system/mjukvara där det är lätt att byta ut olika delar
* System/mjukvara som kan köras i olika miljöer med olika förutsättningar, produktion, test, utveckling m.m.

Även om man inte skriver/programmerar några tester men skriver sin kod testbar så anser jag att man får en bra mjukvaru-design. Jag anser också att det kommer att resultera i att de klasser man skriver/programmerar hanterar det de ska, vilket resulterar i kod som är lättare att underhålla och man undviker redundant kod (duplicate code). Samtidigt kräver det mer av den som programmerar att se till att klasser underhålls på rätt sätt eftersom det är mycket troligt att flera andra klasser har ett beroende till klassen. Med andra ord, bygger man testbart så bygger man objekt-orienterat.

###2.2 Beroenden (dependencies)
Klasser har beroenden till andra klasser. Idén med enhetstestning är att testa kod utan att testa dess beroenden. Tanken är att om en klass fungerar som den är designad och dess beroende klasser likaså så borde de fungera tillsammans som tänkt.

###2.3 Hantera beroenden
För att kunna enhetstesta en metod i en klass som har ett beroende till en annan klass på ett bra sätt så måste man kunna hantera detta beroende. Detta kan hanteras med hjälp av:
* [**Inversion of control**](http://en.wikipedia.org/wiki/Inversion_of_control) - en programmerings teknik
* [**Dependency injection**](http://en.wikipedia.org/wiki/Dependency_injection) - ett design mönster

Kortfattat innebär det att man inte hårdkodar ett beroende till en annan klass utan man gör det möjligt att styra beroendet under körning.

Följande exempel visar en svårtestad metod ([se hela klassen](Company-Samples/Company.Samples/HardToTest/EmailForm.cs)):
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

Metoden ovan är svår att testa i huvudsak för att den skapar en instans av typen SmtpClient och sedan kallar på metoden Send(MailMessage mailMessage). Det finns bl.a två scenarior vi skulle vilja testa för denna metod:
1. Om ValidateInput() returnerar ett object där IsValid == true, så ska Send(mailMessage) anropas.
2. Om ValidateInput() returnerar ett object där IsValid == false, så ska den kasta ett fel och Send(mailMessage) ska inte anropas.

Om vi skippar tänket på god kod-design så skulle vi kunna testa dessa två scenarior ändå om vi har tillgång till något av följande:
* **Microsoft Fakes** - kräver Visual Studio Premium/Ultimate 2012/2013 ([Isolating Code Under Test with Microsoft Fakes](http://msdn.microsoft.com/en-us/library/hh549175.aspx))
* [**Typemock Isolator**](http://www.typemock.com/isolator-product-page)
* [**Telerik JustMock**](http://www.telerik.com/products/mocking.aspx)

[Exempel med **Microsoft Fakes**](Company-Samples/Company.Samples.ShimTests/HardToTest/EmailFormTest.cs)

##3. Visual Studio

###3.1 NuGet
Använd NuGet för att hantera referenser till external bibliotek. När du lägger till **NuGet** paket så hamnar paketen som standard i katalogen **packages** på samma nivå som din solution-fil. Om du slår på (enable) **NuGet Package Restore** så kan utvecklare bygga din solution direkt efter att de öppnat din solution från **Source Control**. Alla paket som behövs laddas ner automatiskt vid första bygget (kan behöva byggas 2 gånger ibland för att det ska fungera). Det är viktigt att inte checka in eventuella **NuGet** paket, för då ser jag inte så så stor vits med **NuGet**. Om du dessutom korrigerar inställningarna (3.1.2 Korrigera NuGet.targets) så behöver du inte ckecka in **NuGet.exe** heller, det laddas också ner vid första bygget.

####3.1.1 Enable NuGet Package Restore
* I **Solution Explorer** högerklicka på din **Solution**
* Klicka **Enable NuGet Package Restore**

Följande katalog och filer har nu skapats under rotkatalogen för din solution:
<pre>
.nuget
    NuGet.Config
    NuGet.exe
    NuGet.targets
</pre>

**.nuget** katalogen läggs även till som en **Solution Folder** i din solution så att du kan se den i **Solution Explorer**.

####3.1.2 Korrigera NuGet.targets
I början på **NuGet.targets** bör det se ut så här:
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

Ändra till följande:
* **RequireRestoreConsent** = false
* **DownloadNuGetExe** = true
* **PackageSource Include** = "https://www.nuget.org/api/v2/"

så att det ser ut så här:
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

Det går även att lägga till fler sökvägar till ytterligare **PackageSources**, om ni t.ex. har någon intern sökväg till era egna **NuGet** paket.

####3.1.3 Bygg NuGet paket av ett projekt
Om du vill skapa egna **NuGet** paket så kan du göra det direkt när du bygger. I rooten på det projekt som du vill skapa ett **NuGet** paket av lägg till en xml-fil och döp den till [PROJECTNAMN].nuspec, dvs. kopiera namnet på projekt-filen och byt ut **csproj** mot **nuspec**. Den ska innehålla följande:
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

Alla värden som börjar och slutar med **$**, t.ex. **$author$**, är så kallade **Replacement Tokens** och kommer ersättas av värden från **AssemblyInfo.cs**.

Lägg till ett **PostBuildEvent** i projektet:
* Högerklicka ditt projekt i **Solution Explorer**
* Välj fliken **Build Events**
* I fältet **Post-build event command line:** - lägg till följande: **"$(SolutionDir).nuget\NuGet.exe" pack "$(ProjectPath)" -Properties Configuration=$(ConfigurationName) -IncludeReferencedProjects**

När du bygger din solution/ditt projekt kommer du få en [PROJEKTNAMN].[VERSION].nupkg i din **output** katalog för projektet.

Exempel i denna solution:
* [**Company.nuspec**](Company-Shared/Company/Company.nuspec)
* [**Company.csproj** - leta efter taggen **&lt;PostBuildEvent&gt;**](Company-Shared/Company/Company.csproj)

Du kan läsa mer om **.nuspec**-filer här:
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