namespace Eldert.BizTalk.InlineXSLTWithIndexer.Schemas {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://Eldert.BizTalk.InlineXSLTWithIndexer.Schemas.CreateOrder/2016/10",@"CreateOrder")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"CreateOrder"})]
    public sealed class CreateOrder : Microsoft.BizTalk.TestTools.Schema.TestableSchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://Eldert.BizTalk.InlineXSLTWithIndexer.Schemas.CreateOrder/2016/10"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" targetNamespace=""http://Eldert.BizTalk.InlineXSLTWithIndexer.Schemas.CreateOrder/2016/10"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""CreateOrder"">
    <xs:complexType>
      <xs:sequence>
        <xs:element maxOccurs=""unbounded"" name=""Package"">
          <xs:complexType>
            <xs:sequence>
              <xs:element name=""OrderNumber"" type=""xs:string"" />
              <xs:element name=""Product"" type=""xs:string"" />
              <xs:element name=""Amount"" type=""xs:string"" />
              <xs:element minOccurs=""0"" name=""InsurancePolicy"" type=""xs:string"" />
              <xs:element name=""DeliveryDate"" type=""xs:string"" />
              <xs:element name=""DeliveryIndex"" type=""xs:string"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public CreateOrder() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "CreateOrder";
                return _RootElements;
            }
        }
        
        protected override object RawSchema {
            get {
                return _rawSchema;
            }
            set {
                _rawSchema = value;
            }
        }
    }
}
