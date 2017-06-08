namespace Eldert.BizTalk.InlineXSLTWithIndexer.Schemas {
    using Microsoft.XLANGs.BaseTypes;
    
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.BizTalk.Schema.Compiler", "3.0.1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [SchemaType(SchemaTypeEnum.Document)]
    [Schema(@"http://Eldert.BizTalk.InlineXSLTWithIndexer.Schemas.RequestOrder/2016/10",@"RequestOrder")]
    [System.SerializableAttribute()]
    [SchemaRoots(new string[] {@"RequestOrder"})]
    public sealed class RequestOrder : Microsoft.BizTalk.TestTools.Schema.TestableSchemaBase {
        
        [System.NonSerializedAttribute()]
        private static object _rawSchema;
        
        [System.NonSerializedAttribute()]
        private const string _strSchema = @"<?xml version=""1.0"" encoding=""utf-16""?>
<xs:schema xmlns=""http://Eldert.BizTalk.InlineXSLTWithIndexer.Schemas.RequestOrder/2016/10"" xmlns:b=""http://schemas.microsoft.com/BizTalk/2003"" targetNamespace=""http://Eldert.BizTalk.InlineXSLTWithIndexer.Schemas.RequestOrder/2016/10"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name=""RequestOrder"">
    <xs:complexType>
      <xs:sequence>
        <xs:element name=""OrderNumber"" type=""xs:string"" />
        <xs:element maxOccurs=""unbounded"" name=""Delivery"">
          <xs:complexType>
            <xs:sequence>
              <xs:element name=""DeliveryDate"" type=""xs:string"" />
              <xs:element maxOccurs=""unbounded"" name=""Product"">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name=""Name"" type=""xs:string"" />
                    <xs:element name=""Amount"" type=""xs:string"" />
                    <xs:element name=""SKU"" type=""xs:string"" />
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
              <xs:element maxOccurs=""unbounded"" name=""Insurance"">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name=""SKU"" type=""xs:string"" />
                    <xs:element name=""PolicyNumber"" type=""xs:string"" />
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>";
        
        public RequestOrder() {
        }
        
        public override string XmlContent {
            get {
                return _strSchema;
            }
        }
        
        public override string[] RootNodes {
            get {
                string[] _RootElements = new string [1];
                _RootElements[0] = "RequestOrder";
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
