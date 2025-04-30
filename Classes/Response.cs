using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace ReflectorKG.Classes;

internal class Response
{
    private readonly XslCompiledTransform xslCompiled = new();

    public Response()
    {
        var xml = new XmlDocument
        {
            PreserveWhitespace = true
        };
        xml.LoadXml(
            "<?xml version='1.0' encoding='utf-8'?>\r<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform' xmlns:msxsl='urn:schemas-microsoft-com:xslt' exclude-result-prefixes='msxsl'>\r  <xsl:output method='xml' indent='no' omit-xml-declaration='yes'/>\r  <xsl:param name='edition'/>\r  <xsl:param name='version'/>\r  <xsl:param name='userspurchased'/>\r  \r  <xsl:template match='activationrequest'>\r    <data>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates/>\r      <edition><xsl:value-of select='$edition'/></edition>\r      <xsl:text>\r</xsl:text>\r      <version><xsl:value-of select='$version'/></version>\r      <xsl:text>\r</xsl:text>\r      <userspurchased><xsl:value-of select='$userspurchased'/></userspurchased>\r      <xsl:text>\r</xsl:text>\r    </data>\r  </xsl:template>\r\r  <xsl:template match='machinehash'>\r    <machinehash>\r      <xsl:value-of select='text( )'/>\r    </machinehash>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='productcode|majorversion|minorversion|serialnumber|session|edition|productname'>\r    <xsl:copy>\r      <xsl:value-of select='text( )'/>\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='productcodes'>\r    <xsl:copy>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates />\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>    \r  </xsl:template>\r\r  <xsl:template match='product'>\r    <xsl:copy>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates />\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='text( )'/>\r</xsl:stylesheet>\r\n");

        xslCompiled.Load(xml);
    }

    public string Generate(string request, string? users, string? edition)
    {
        var document = new XmlDocument();
        document.LoadXml(request);

        var xmlWriterSettings = xslCompiled.OutputSettings.Clone();
        xmlWriterSettings.NewLineChars    = Environment.NewLine;
        xmlWriterSettings.NewLineHandling = NewLineHandling.Replace;

        var v_def_users   = Environment.GetEnvironmentVariable("users_min_default");
        var v_def_version = Environment.GetEnvironmentVariable("version_default");
        var v_def_edition = Environment.GetEnvironmentVariable("edition_default");
        var getUsers      = users   ?? v_def_users;
        var getEdition    = edition ?? v_def_edition;

        Debug.WriteLine(getUsers);

        var xsltArgumentList = new XsltArgumentList();
        xsltArgumentList.AddParam("version",        "", v_def_version);
        xsltArgumentList.AddParam("edition",        "", getEdition);
        xsltArgumentList.AddParam("userspurchased", "", getUsers);

        var sb     = new StringBuilder();
        var writer = XmlWriter.Create(sb, xmlWriterSettings);

        xslCompiled.Transform(document, xsltArgumentList, writer);
        var data = sb.ToString();

        return AddSignature(data);
    }


    private static string GetResourceTextFile(string filename)
    {
        var       asm    = Assembly.GetExecutingAssembly();
        using var stream = asm.GetManifestResourceStream($"{asm.GetName().Name}.Properties.{filename}");
        using var sr     = new StreamReader(stream!);
        return sr.ReadToEnd();
    }


    private static string AddSignature(string data)
    {
        var rsaCryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
        {
            Flags = CspProviderFlags.UseMachineKeyStore
        });
        var xml = GetResourceTextFile("RSAKeyValue.xml");
        rsaCryptoServiceProvider.FromXmlString(xml);
        var dataBytes  = Encoding.UTF8.GetBytes(data);
        var dataBase64 = Convert.ToBase64String(rsaCryptoServiceProvider.SignData(dataBytes, new SHA1CryptoServiceProvider()));

        return $"<activationresponse>\r\n{data}\r\n<signature>\r\n{dataBase64}\r\n</signature>\r\n</activationresponse>\r\n";
    }
}