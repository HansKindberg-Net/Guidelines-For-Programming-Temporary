#Riktlinjer för programmering





Abstract types from EPiServer CMS 7 to be used in EPiServer 4, 5 and 6 libraries.

Important
---------
### EPiServer.Licensing NuGet package
To be able to build this solution you have to create a NuGet-package, **EPiServer.Licensing** (6.2.267.1), and add it to a folder with the following path, **C:\Data\NuGet-packages**. This is because some unit-tests (shim-tests) uses **Fakes** to test wrappers.

Use **NuGet Package Explorer GUI** to create the package
* [Download **NuGet Package Explorer GUI**](http://nuget.codeplex.com/downloads/get/clickOnce/NuGetPackageExplorer.application?releaseId=59864&ProjectName=nuget)
* [Information about **NuGet Package Explorer GUI**](http://docs.nuget.org/docs/creating-packages/using-a-gui-to-build-packages)

The **EPiServer.Licensing.nuspec** should look like this:
<pre>
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;package xmlns="http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd"&gt;
    &lt;metadata&gt;
        &lt;id&gt;EPiServer.Licensing&lt;/id&gt;
        &lt;version&gt6.2.267.1&lt;/version&gt;
        &lt;title&gtEPiServer.Licensing&lt/title&gt;
        &lt;authors&gt;EPiServer AB&lt;/authors&gt;
        &lt;owners&gt;EPiServer AB&lt;/owners&gt;
        &lt;licenseUrl&gt;http://world.episerver.com/PageFiles/99654/EPiServer EULA.txt&lt;/licenseUrl&gt;
        &lt;requireLicenseAcceptance&gt;true&lt;/requireLicenseAcceptance&gt;
        &lt;description&gt;EPiServer license handler.&lt;/description&gt;
        &lt;copyright&gt;\x00a9 2003-2010 by EPiServer AB. All rights reserved&lt;/copyright&gt;
    &lt;/metadata&gt;
&lt;/package&gt;
</pre>

and the content like this:
<pre>
lib
    net20
        EPiServer.Licensing.dll
</pre>

To get the **EPiServer.Licensing.dll**, version **6.2.267.1**, copy it from an existing **EPiServer 6.1.379.0** site, or install a **EPiServer 6.1.379.0** site and copy it.