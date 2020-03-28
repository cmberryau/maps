using System.Reflection;
using System.Runtime.InteropServices;
using log4net.Config;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Maps.Tests")]
[assembly: AssemblyDescription("Tests for Maps")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Pty Ltd")]
[assembly: AssemblyProduct("Maps.Tests")]
[assembly: AssemblyCopyright("Copyright ©  2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("1240af73-59ff-4cb8-aaea-9647e10010ad")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
//[assembly: AssemblyFileVersion("0.0.0.1")]
[assembly: AssemblyVersion("0.1.*")]
[assembly: XmlConfigurator(ConfigFile = "log4net.xml")]
