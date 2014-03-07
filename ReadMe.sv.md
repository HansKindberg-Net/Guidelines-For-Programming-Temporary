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
Detta projekt best�r av en **VS-solution** med diverse **VS-project** med exempel kod. Programmeringsspr�ket som anv�nds �r **C#**, **.NET Framework 4.5**. Jag vill visa hur jag menar genom exempel. **VS-solution** inneh�ller flera **VS-project** och d�rf�r har jag valt att gruppera/strukturera dem med hj�lp av **Solution Folders**.
- **.nuget** - katalogen inneh�ller filer f�r **NuGet Package Restore**, dessa filer skapas n�r man sl�r p� **NuGet Package Restore** ([3.1.1 Enable NuGet Package Restore](/ReadMe.sv.md#311-enable-nuget-package-restore))
- **CodeAnalysis** - globala filer/inst�llningar f�r **Code Analysis** ([Code Analysis for Managed Code Overview](http://msdn.microsoft.com/en-us/library/3z0aeatx.aspx)), l�nkas in av **VS-project**
- **Company-Console** - inneh�ller ett Windows-console-application projekt + tillh�rande **VS-test-project**
- **Company-Examples** - inneh�ller ett **Class Library** (**VS-project**) med mer allm�n exempel-kod vad g�ller testbarhet + tillh�rande **VS-test-project**
- **Company-Services** - inneh�ller WCF och WebService (asmx) projekt + tillh�rande **VS-test-project**
- **Company-Shared** - inneh�ller generella funktioner f�r hela l�sningen
- **Company-Web** - inneh�ller webbapplikationer (MVC, MVP och traditionell Web Forms) + tillh�rande **VS-test-project**
- **Company-Windows** - inneh�ller ett Windows-forms-application projekt + tillh�rande **VS-test-project**
- **Data** - inneh�ller en liten exempel-databas fil
- **Documents** - **ReadMe.md** filerna f�r detta projekt
- **Properties** - globala **Assembly** inst�llningar, l�nkas in av **VS-project**
- **Signing** - global **Strong Name Key** fil, l�nkas in av **VS-project**

### 1.3 Namnkonvention
Jag inleder alla **VS-project** namn med ansvarigt f�retags namn (eller organisation). I detta projekt har jag valt att inleda alla **VS-project** namn med **Company**. Resten av namnet bygger p� funtions-namn eller produkt-namn.

#### 1.3.1 VS-test-project
Alla **VS-test-project** i den **VS-solution** som detta projekt best�r av �r av typen **Unit Test Project**. Jag anv�nder f�ljande namngivning p� **VS-test-project**:
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

### 2.1 F�rdelar
F�rdelar med testbarhet:
- **Pluggbara** system � system/mjukvara d�r det �r l�tt att byta ut olika delar
- System/mjukvara som kan k�ras i olika milj�er med olika f�ruts�ttningar, produktion, test, utveckling m.m.

�ven om man inte skriver/programmerar n�gra tester men skriver sin kod testbar s� anser jag att man f�r en bra mjukvaru-design. Jag anser ocks� att det kommer att resultera i att de klasser man skriver/programmerar hanterar det de ska, vilket resulterar i kod som �r l�ttare att underh�lla och man undviker redundant kod (duplicate code). Samtidigt kr�ver det mer av den som programmerar att se till att klasser underh�lls p� r�tt s�tt eftersom det �r mycket troligt att flera andra klasser har ett beroende till klassen. Med andra ord, bygger man testbart s� bygger man objekt-orienterat.

### 2.2 Beroenden (dependencies)
Klasser har beroenden till andra klasser. Id�n med enhetstestning �r att testa kod utan att testa dess beroenden. Tanken �r att om en klass fungerar som den �r designad och dess beroende klasser likas� s� borde de fungera tillsammans som t�nkt.

### 2.3 Hantera beroenden
F�r att kunna enhetstesta en metod i en klass som har ett beroende till en annan klass p� ett bra s�tt s� m�ste man kunna hantera detta beroende. Detta kan hanteras med hj�lp av:
- [**Dependency injection**](http://en.wikipedia.org/wiki/Dependency_injection) - ett design m�nster
- [**Inversion of control**](http://en.wikipedia.org/wiki/Inversion_of_control) - en programmerings teknik

Mitt s�tt att se det: [**Inversion of control**](http://en.wikipedia.org/wiki/Inversion_of_control) �r en teknik man kan anv�nda f�r att hantera [**Dependency injection**](http://en.wikipedia.org/wiki/Dependency_injection).

Kortfattat inneb�r det att man inte h�rdkodar ett beroende till en annan klass utan man g�r det m�jligt att styra beroendet under k�rning. Martin Fowler har skrivit en artikel som behandlar detta omr�de, [**Inversion of Control Containers and the Dependency Injection pattern**](http://martinfowler.com/articles/injection.html#ServiceLocatorVsDependencyInjection). Martin Fowler skriver ocks� om f�rdelar/nackdelar med att anv�nda respektive teknik/metod f�r att hantera beroenden:
- [Inversion of Control Containers and the Dependency Injection pattern - **Deciding which option to use**](http://martinfowler.com/articles/injection.html#DecidingWhichOptionToUse)
- [Inversion of Control Containers and the Dependency Injection pattern - **Service Locator vs Dependency Injection**](http://martinfowler.com/articles/injection.html#ServiceLocatorVsDependencyInjection)
- [Inversion of Control Containers and the Dependency Injection pattern - **Constructor versus Setter Injection**](http://martinfowler.com/articles/injection.html#ConstructorVersusSetterInjection)

Den teknik/metod som f�respr�kas mest �r **Constructor Injection** vilket inneb�r att beroenden anges i konstruktorn f�r en klass. Det g�r allts� inte att instansiera klassen utan att ange dess beroenden. Detta �r anledningen till att denna teknik/metod f�respr�kas, det blir tydligt f�r programmerare att en klass har beroenden.

#### [2.3.1 Dependency Injection (DI)](http://en.wikipedia.org/wiki/Dependency_injection)

#### [2.3.2 Inversion of Control (IoC)](http://en.wikipedia.org/wiki/Inversion_of_control)

##### 2.3.2.1 Inversion of Control Containers (IoC Containers)

### 2.4 Mock
Ett vanligt begrepp inom enhets-testning �r **Mock** - *F�r att kunna utf�ra enhets-tester p� en klass s� mockar man dess beroenden*. Det finns ramverk att anv�nda vid testning/enhets-testning som inneh�ller begreppet **Mock**:

- [**EasyMock.NET**](http://sourceforge.net/projects/easymocknet/)
- [**JustMock**](http://www.telerik.com/products/mocking.aspx)
- [**Moq**](http://www.moqthis.com/)
- [**NMock**](http://nmock3.codeplex.com/)
- [**Rhino Mocks**](http://hibernatingrhinos.com/oss/rhino-mocks)
- [**TypeMock Isolator**](http://www.typemock.com/)

Ska man vara korrekt s� �r **Mock** bara en del i ett vidare begrepp, [**Test Double**](http://en.wikipedia.org/wiki/Test_double).

Gerard Meszaros har definierat begrepp f�r olika typer av [**Test Double**](http://xunitpatterns.com/Test%20Double.html) som han kallar det:

- **Dummy object** - used when a parameter is needed for the tested method but without actually needing to use the parameter
- **Fake object** - used as a simpler implementation, e.g. using an in-memory database in the tests instead of doing real database access
- **Mock object** - used for verifying "indirect output" of the tested code, by first defining the expectations before the tested code is executed
- **Test spy** - used for verifying "indirect output" of the tested code, by asserting the expectations afterwards, without having defined the expectations before the tested code is executed
- **Test stub** - used for providing the tested code with "indirect input"

Vad det handlar om �r att s�tta upp egenskaper och f�rv�ntningar p� beroenden och p� s� s�tt testa olika scenarior f�r den klass som �r under test. Det man anv�nder mock-ramverken till �r att skapa upp klasser dynamiskt under k�rningen av enhets-testet. Det kr�ver att beroenden �r **mock-bara** (mitt begrepp):
- **Interface** - alla medlemmar (egenskaper/metoder) i ett interface �r **alltid** mock-bara
- **Abstract** - abstracta medlemmar (egenskaper/metoder) �r ofta mock-bara
- **Virtual (C#)** - virtuella medlemmar (egenskaper/metoder) �r ofta mock-bara

**Interface** �r alltid mock-bara eftersom de inte inneh�ller n�gon implementation. Abstrakta klasser, klasser med abstrakta medlemmar eller klasser med virtuella medlemmar �r ofta mock-bara, men de beh�ver inte vara det. T.ex. s� kan koden i konstrukorn f�r en abstrakt klass g�ra att den inte g�r att mocka.

Man kan �ven skriva sina **Test Doubles** sj�lv men det underl�ttar om man anv�nder ett ramverk.

#### 2.4.1 Mock the unmockable
Det finns **Mock**-ramverk som dock kan mocka det mesta:

- [**Microsoft Fakes**](http://msdn.microsoft.com/en-us/library/hh549175.aspx) - kr�ver Visual Studio Premium/Ultimate 2012/2013 ([Isolating Code Under Test with Microsoft Fakes](http://msdn.microsoft.com/en-us/library/hh549175.aspx))
- [**Typemock Isolator**](http://www.typemock.com/isolator-product-page)
- [**Telerik JustMock**](http://www.telerik.com/products/mocking.aspx)

Dessa ramverk fungerar genom att de g�r in och manipulerar vid bygget av en solution. De kan vara bra att anv�nda n�r man inte har kontroll �ver beroenden till kod man vill testa. Men om man anv�nder dessa ramverk rakt igenom s� leder det inte till n�gon b�ttre mjukvaru-arkitektur. De kan t.ex. vara bra att anv�nda n�got av dessa ramverk om man vill enhets-testa en **Wrapper**.

### 2.5 Design Patterns
F�r att kunna skriva testbar kod beh�ver man m�nga g�nger anv�nda sig av **Design Patterns**. Vanliga m�nster f�r att hantera dependency injection �r:
- [**Adapter**](http://www.blackwasp.co.uk/Adapter.aspx) - koda **Wrappers** f�r att ge befintliga sv�r-mockade klasser ett anv�ndbart gr�nssnitt
- [**Factory Method**](http://www.blackwasp.co.uk/FactoryMethod.aspx) - ett m�nster som anv�nds f�r att instansiera klasser (om en klass kr�ver en eller flera beroende-parametrar i sin konstruktor s� kan det underl�tta genom att ha en fabrik som instansierar objekt)

### 2.6 Exempel
Det vanligaste problemet med att enhets-testa kod �r att beroenden i en klass �r h�rdkodade. H�rdkodade beroenden g�r inte att styra utifr�n vilket man beh�ver kunna g�ra om man ska kunna enhets-testa. Om man inte har m�jlighet att styra detta kan man anv�nda sig av n�got av de ramverk som beskrivs under [2.4.1 Mock the unmockable](/ReadMe.sv.md#241-mock-the-unmockable). Men man b�r str�va efter att kunna styra beroenden utifr�n f�r att f� en bra mjukvaru-arkitektur.

Nedan f�ljer exempel p� kod som �r sv�r att enhets-testa och hur man kan korrigera f�r att g�ra koden testbar. Varje exempel som f�ljer har en tillh�rande test-class, �ven om det inte g�r att testa. I vissa exempel finns �ven exempel p� hur man kan l�sa det med [**Shims**](http://msdn.microsoft.com/en-us/library/hh549175.aspx#shims)/**Shim**-tester. Att l�sa det med [**Shims**](http://msdn.microsoft.com/en-us/library/hh549175.aspx#shims) �r inte att rekommendera om man har kontroll �ver koden, det �r med som ett exempel.

#### 2.6.1 Klass med beroende till en klass med en statisk metod

Det vi vill testa �r att n�r ClassWithStaticDependency.Method() anropas s� ska ClassWithStaticMethod.Method() anropas.

- [**ClassWithStaticDependency**](/Company-Examples/Company.Examples/Testability/HardToTest/ClassWithStaticDependency.cs)
- [**ClassWithStaticDependencyTest**](/Company-Examples/Company.Examples.UnitTests/Testability/HardToTest/ClassWithStaticDependencyTest.cs)

L�sningar:

- [**Shim-test**](/Company-Examples/Company.Examples.ShimTests/Testability/HardToTest/ClassWithStaticDependencyTest.cs)
- [**ClassWithStaticDependencyMadeTestable** - Constructor injection](/Company-Examples/Company.Examples/Testability/Testable/ClassWithStaticDependencyMadeTestable.cs)
	- L�sningen hanteras med hj�lp av [**IClassWithStaticMethod**](/Company-Examples/Company.Examples/Testability/Mockable/IClassWithStaticMethod.cs) & [**ClassWithStaticMethodWrapper**](/Company-Examples/Company.Examples/Testability/Wrappers/ClassWithStaticMethodWrapper.cs)
- [**ClassWithStaticDependencyMadeTestableTest**](/Company-Examples/Company.Examples.UnitTests/Testability/Testable/ClassWithStaticDependencyMadeTestableTest.cs)

#### 2.6.2 Klass med beroende till en klass med en sealed (sluten) metod

Det vi vill testa �r att n�r ClassWithSealedDependency.Method() anropas s� ska ClassWithSealedMethod.Method() anropas.

- [**ClassWithSealedDependency**](/Company-Examples/Company.Examples/Testability/HardToTest/ClassWithSealedDependency.cs)
- [**ClassWithSealedDependencyTest**](/Company-Examples/Company.Examples.UnitTests/Testability/HardToTest/ClassWithSealedDependencyTest.cs)

F�ljande l�sningar �r exempel p� generella l�sningar, l�sningar som generellt g�ller f�r att l�sa alla typer av problem med sv�rtestad kod.

L�sningar:

- [**ClassWithConstructorInjectableInterfaceDependency** - Constructor injection](/Company-Examples/Company.Examples/Testability/Testable/ClassWithConstructorInjectableInterfaceDependency.cs)
	- L�sningen hanteras med hj�lp av [**IDependency**](/Company-Examples/Company.Examples/Testability/Mockable/IDependency.cs) (vid en riktig/konkret implementering kr�vs en klass som implementerar [**IDependency**](/Company-Examples/Company.Examples/Testability/Mockable/IDependency.cs))
- [**ClassWithConstructorInjectableInterfaceDependencyTest**](/Company-Examples/Company.Examples.UnitTests/Testability/Testable/ClassWithConstructorInjectableInterfaceDependencyTest.cs)
- [**ClassWithSetterInjectableInterfaceDependency** - Setter injection](/Company-Examples/Company.Examples/Testability/Testable/ClassWithSetterInjectableInterfaceDependency.cs)
	- L�sningen hanteras med hj�lp av [**IDependency**](/Company-Examples/Company.Examples/Testability/Mockable/IDependency.cs) (vid en riktig/konkret implementering kr�vs en klass som implementerar [**IDependency**](/Company-Examples/Company.Examples/Testability/Mockable/IDependency.cs))
- [**ClassWithSetterInjectableInterfaceDependencyTest**](/Company-Examples/Company.Examples.UnitTests/Testability/Testable/ClassWithSetterInjectableInterfaceDependencyTest.cs)

#### 2.6.3 Klass med beroende till en klass med en virtual metod

Det vi vill testa �r att n�r ClassWithVirtualDependency.Method() anropas s� ska ClassWithVirtualMethod.Method() anropas.

- [**ClassWithVirtualDependency**](/Company-Examples/Company.Examples/Testability/HardToTest/ClassWithVirtualDependency.cs)
- [**ClassWithVirtualDependencyTest**](/Company-Examples/Company.Examples.UnitTests/Testability/HardToTest/ClassWithVirtualDependencyTest.cs)

L�sningar:

- [**ClassWithVirtualDependencyMadeTestable** - Constructor injection](/Company-Examples/Company.Examples/Testability/Testable/ClassWithVirtualDependencyMadeTestable.cs)
- [**ClassWithVirtualDependencyMadeTestableTest**](/Company-Examples/Company.Examples.UnitTests/Testability/Testable/ClassWithVirtualDependencyMadeTestableTest.cs)

#### 2.6.4 Klass med beroende till DateTime.Now

Det vi vill testa �r att n�r ClassWithDateTimeNowDependency.GetCurrentDateTime() anropas s� ska DateTime.Now anropas och returnera v�rdet.

- [**ClassWithDateTimeNowDependency**](/Company-Examples/Company.Examples/Testability/HardToTest/ClassWithDateTimeNowDependency.cs)
- [**ClassWithDateTimeNowDependencyTest**](/Company-Examples/Company.Examples.UnitTests/Testability/HardToTest/ClassWithDateTimeNowDependencyTest.cs)

L�sningar:

- [**Shim-test**](/Company-Examples/Company.Examples.ShimTests/Testability/HardToTest/ClassWithDateTimeNowDependencyTest.cs)
- [**ClassWithDateTimeNowDependencyMadeTestable** - Constructor injection](/Company-Examples/Company.Examples/Testability/Testable/ClassWithDateTimeNowDependencyMadeTestable.cs)
	- L�sningen hanteras med hj�lp av [**IDateTimeContext**](/Company-Shared/Company/IDateTimeContext.cs) ([**DateTimeContext**](/Company-Shared/Company/DateTimeContext.cs) �r ett exempel p� en klass som implementerar [**IDateTimeContext**](/Company-Shared/Company/IDateTimeContext.cs) och som kan anv�ndas n�r ett system k�rs p� "riktigt")
- [**ClassWithDateTimeNowDependencyMadeTestableTest**](/Company-Examples/Company.Examples.UnitTests/Testability/Testable/ClassWithDateTimeNowDependencyMadeTestableTest.cs)

#### 2.6.5 Klass med beroende till SmtpClient (System.Net.Mail)

Det vi vill testa �r att n�r ClassWithSmtpClientDependency.Send(string to, string subject, string message) anropas s� ska SmtpClient.Send(...) anropas med korrekta v�rden.

- [**ClassWithSmtpClientDependency**](/Company-Examples/Company.Examples/Testability/HardToTest/ClassWithSmtpClientDependency.cs)
- [**ClassWithSmtpClientDependencyTest**](/Company-Examples/Company.Examples.UnitTests/Testability/HardToTest/ClassWithSmtpClientDependencyTest.cs)

L�sningar:

- [**Shim-test**](/Company-Examples/Company.Examples.ShimTests/Testability/HardToTest/ClassWithSmtpClientDependencyTest.cs)
- [**ClassWithSmtpClientDependencyMadeTestableFirstAlternative** - Constructor injection](/Company-Examples/Company.Examples/Testability/Testable/ClassWithSmtpClientDependencyMadeTestableFirstAlternative.cs)
	- L�sningen hanteras med hj�lp av [**ISmtpClient**](/Company-Shared/Company/Net/Mail/ISmtpClient.cs) ([**SmtpClientWrapper**](/Company-Shared/Company/Net/Mail/SmtpClientWrapper.cs) �r ett exempel p� en klass som implementerar [**ISmtpClient**](/Company-Shared/Company/Net/Mail/ISmtpClient.cs) och som kan anv�ndas n�r ett system k�rs p� "riktigt")
- [**ClassWithSmtpClientDependencyMadeTestableFirstAlternativeTest**](/Company-Examples/Company.Examples.UnitTests/Testability/Testable/ClassWithSmtpClientDependencyMadeTestableFirstAlternativeTest.cs)
- [**ClassWithSmtpClientDependencyMadeTestableSecondAlternative** - Constructor injection](/Company-Examples/Company.Examples/Testability/Testable/ClassWithSmtpClientDependencyMadeTestableSecondAlternative.cs)
	- L�sningen hanteras med hj�lp av [**ISmtpClientFactory**](/Company-Shared/Company/Net/Mail/ISmtpClientFactory.cs) ([**DefaultSmtpClientFactory**](/Company-Shared/Company/Net/Mail/DefaultSmtpClientFactory.cs) �r ett exempel p� en klass som implementerar [**ISmtpClientFactory**](/Company-Shared/Company/Net/Mail/ISmtpClientFactory.cs) och som kan anv�ndas n�r ett system k�rs p� "riktigt")
- [**ClassWithSmtpClientDependencyMadeTestableSecondAlternativeTest**](/Company-Examples/Company.Examples.UnitTests/Testability/Testable/ClassWithSmtpClientDependencyMadeTestableSecondAlternativeTest.cs)





#### 2.6.6 Klass med beroende till DirectoryEntry (System.DirectoryServices)

Nu blir det mer komplicerat. Denna klass kanske till och med ska designas p� ett annat s�tt som g�r den l�ttare att testa. Men detta �r ett exemple s�...

Ett scenario vi vill testa �r att n�r ClassWithDirectoryEntryDependency.GetLdapRootPropertyNames() anropas s� ska den anropa DirectoryEntry.Properties.PropertyNames och returnera en samling namn.

- [**ClassWithDirectoryEntryDependency**](/Company-Examples/Company.Examples/Testability/HardToTest/ClassWithDirectoryEntryDependency.cs)
- [**ClassWithDirectoryEntryDependencyTest**](/Company-Examples/Company.Examples.UnitTests/Testability/HardToTest/ClassWithDirectoryEntryDependencyTest.cs)

L�sningar:

- [**Shim-test**](/Company-Examples/Company.Examples.ShimTests/Testability/HardToTest/ClassWithSmtpClientDependencyTest.cs)
- [**ClassWithSmtpClientDependencyMadeTestableFirstAlternative** - Constructor injection](/Company-Examples/Company.Examples/Testability/Testable/ClassWithSmtpClientDependencyMadeTestableFirstAlternative.cs)
	- L�sningen hanteras med hj�lp av [**ISmtpClient**](/Company-Shared/Company/Net/Mail/ISmtpClient.cs) ([**SmtpClientWrapper**](/Company-Shared/Company/Net/Mail/SmtpClientWrapper.cs) �r ett exempel p� en klass som implementerar [**ISmtpClient**](/Company-Shared/Company/Net/Mail/ISmtpClient.cs) och som kan anv�ndas n�r ett system k�rs p� "riktigt")
- [**ClassWithSmtpClientDependencyMadeTestableFirstAlternativeTest**](/Company-Examples/Company.Examples.UnitTests/Testability/Testable/ClassWithSmtpClientDependencyMadeTestableFirstAlternativeTest.cs)
- [**ClassWithSmtpClientDependencyMadeTestableSecondAlternative** - Constructor injection](/Company-Examples/Company.Examples/Testability/Testable/ClassWithSmtpClientDependencyMadeTestableSecondAlternative.cs)
	- L�sningen hanteras med hj�lp av [**ISmtpClientFactory**](/Company-Shared/Company/Net/Mail/ISmtpClientFactory.cs) ([**DefaultSmtpClientFactory**](/Company-Shared/Company/Net/Mail/DefaultSmtpClientFactory.cs) �r ett exempel p� en klass som implementerar [**ISmtpClientFactory**](/Company-Shared/Company/Net/Mail/ISmtpClientFactory.cs) och som kan anv�ndas n�r ett system k�rs p� "riktigt")
- [**ClassWithSmtpClientDependencyMadeTestableSecondAlternativeTest**](/Company-Examples/Company.Examples.UnitTests/Testability/Testable/ClassWithSmtpClientDependencyMadeTestableSecondAlternativeTest.cs)



## 3. Visual Studio

### 3.1 NuGet
Anv�nd NuGet f�r att hantera referenser till external bibliotek. N�r du l�gger till **NuGet** paket s� hamnar paketen som standard i katalogen **packages** p� samma niv� som din VS-solution-fil. Om du sl�r p� (enable) **NuGet Package Restore** s� kan utvecklare bygga din VS-solution direkt efter att de �ppnat din VS-solution fr�n **Source Control**. Alla paket som beh�vs laddas ner automatiskt vid f�rsta bygget (kan beh�va byggas 2 g�nger ibland f�r att det ska fungera). Det �r viktigt att inte checka in eventuella **NuGet** paket, f�r d� ser jag inte s� s� stor vits med **NuGet**. Om du dessutom korrigerar inst�llningarna ([3.1.2 Korrigera NuGet.targets](/ReadMe.sv.md#312-korrigera-nugettargets)) s�:

- beh�ver du inte ckecka in/commita **NuGet.exe** heller, det laddas ocks� ner vid f�rsta bygget
- beh�ver inte andra utvecklare/programmerare som �ppnar din VS-solution fr�n **Source Control** ha **NuGet Package Manager** installerat �ver huvudtaget eller inte konfigurerat p� samma s�tt som du f�r att de �nd� ska kunna bygga VS-solution

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
Web.config transforms are built into Visual Studio. You can transform the Web.config file when publishing/deploying a Visual Studio webapplication.
Web.config Transformation Syntax for Web Project Deployment Using Visual Studio: http://msdn.microsoft.com/en-us/library/dd465326(v=vs.110).aspx
SlowCheetah
Is a Visual Studio extension to handle transformation of any xml-file. And it transforms on build (F5)
SlowCheetah - XML Transforms: http://visualstudiogallery.msdn.microsoft.com/69023d00-a4f9-4a34-a6cd-7e854ba318b5
SlowCheetah on NuGet: http://www.nuget.org/packages/SlowCheetah/
SlowCheetah on GitHub: https://github.com/sayedihashimi/slow-cheetah

### 3.4 ReSharper

### 3.5 Solution Folder
I Visual Studio kan man skapa kataloger p� solution-niv�. Om man h�gerklickar VS-solution i **Solution Explorer** f�r man upp valet **Add** -> **New Solution Folder**.
N�r man skapar en **Solution Folder** skapas inte en katalog p� disk. L�gger man till en fil, genom att h�gerklicka p� katalogen och v�ljer **Add** -> **New Item** s� kommer den nya filen visuellt att ligga i den **Solution Folder** man valde att l�gga till filen in men fysiskt s� hamnar filen i rotkatalogen f�r VS-solution.
Man kan korrigera s� att den visuella strukturen st�mmer �verens med den fysiska strukturen men det kr�ver manuella �tg�rder. Exempel:
1. L�gg till en **Solution Folder**.
2. L�gg till en katalog p� disk i rot-katalogen f�r din VS-solution och ge den samma namn.
3. L�gg till en fil fr�n Visual Studio.
4. Ta bort samma fil fr�n Visual Studio (den kommer bara att tas bort visuellt i Visual Studio, inte fysiskt p� disk).
5. Flytta den fysiska filen (som du tog bort i steget innan) fr�n rot-katalogen till den aktuella fysiska katalogen p� disk.
6. V�lj **Add** -> **Existing Item** genom att h�gerklicka p� katalogen i **Solution Explorer**.
Detta g�ller �ven sub-kataloger. Om man nu l�gger till den VS-solution till source-control s� kommer katalogstrukturen f�r all VS-solution kataloger vara den samma b�de fysiskt och visuellt i VS-solution.


