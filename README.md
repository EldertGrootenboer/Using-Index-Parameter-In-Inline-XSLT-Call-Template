# Using Index Parameter In Inline XSLT Call Template
When developing maps with BizTalk, we have several options to map our data. We can of course use the mapper functionality itself, but sometimes we will find this will not give us the required flexibility we need. Luckily the BizTalk mapper gives us the ability to use our own code using things like inline C#, XSLT, XSLT call templates etc. When using inline xslt call templates, we might want to use a XPATH expression to gather the data we need, but there is a small caveat if we want to use an indexer in expression.

## Scenario
To explain this, we will use a scenario where we map our message using an inline xslt call template. The incoming message represents an order with multiple deliveries, each with mulitple products and an optional insurance for the products. Note that this is a simplified scenario which we could also solve in other ways, even just using mapping functionalities, but this is just to show one way of doing this.

![](https://code.msdn.microsoft.com/site/view/file/162473/1/1.PNG)

These should be merged in the outgoing message. We will add the node DeliveryIndex here, to help us debug the map later on.

![](https://code.msdn.microsoft.com/site/view/file/162474/1/2.PNG)

## Our XSLT Template
We will be using the following XSLT template to map our products.

```XML
<xsl:template name="AggregateProducts"> 
    <!-- Parameters --> 
    <xsl:param name="parOrderNumber" /> 
    <!-- This is our indexer --> 
    <xsl:param name="parDeliveryOrderIndex" /> 
 
    <!-- Loop through products --> 
    <!-- Use the index to get products for the correct delivery --> 
    <xsl:for-each select="//*[local-name()='RequestOrder']/*[local-name()='Delivery'][$parDeliveryOrderIndex]/*[local-name()='Product']"> 
 
        <!-- Check if a insurance is specified for this product --> 
        <xsl:variable name="var:SKU" select="*[local-name()='SKU']" /> 
        <xsl:variable name="var:InsuranceNumber" select="/*[local-name()='RequestOrder']/*[local-name()='Delivery'][$parDeliveryOrderIndex]/*[local-name()='Insurance'][*[local-name()='SKU'][text()=$var:SKU]]/*[local-name()='PolicyNumber'][text()]" /> 
 
        <!-- Create package node --> 
        <xsl:element name="Package"> 
            <!-- Create child nodes --> 
            <xsl:element name="OrderNumber"> 
                <xsl:value-of select="$parOrderNumber" /> 
            </xsl:element> 
            <xsl:element name="Product"> 
                <xsl:value-of select="*[local-name()='Name'][text()]" /> 
            </xsl:element> 
            <xsl:element name="Amount"> 
                <xsl:value-of select="*[local-name()='Amount'][text()]" /> 
            </xsl:element> 
 
            <!-- Only map insurance number if it was set --> 
            <xsl:if test="$var:InsuranceNumber"> 
                <xsl:element name="InsurancePolicy"> 
                    <xsl:value-of select="$var:InsuranceNumber" /> 
                </xsl:element> 
            </xsl:if> 
             
            <xsl:element name="DeliveryDate"> 
                <xsl:value-of select="../*[local-name()='DeliveryDate'][text()]" /> 
            </xsl:element> 
 
            <!-- Used to show that the delivery order index is set --> 
            <xsl:element name="DeliveryIndex"> 
                <xsl:value-of select="$parDeliveryOrderIndex" /> 
            </xsl:element> 
        </xsl:element> 
    </xsl:for-each> 
</xsl:template>
```

## Creating the map
Now we will create the map, and add a script functoid in which we add use the template we just created. For the first parameter we will use the ordernumber as input to the functoid. For the second parameter (the indexer) we will use a iteration functoid, which will loop over the delivery nodes. Our output will be sent to the Package node.

![](https://code.msdn.microsoft.com/site/view/file/162475/1/4.PNG)

We will use the following input for our tests.

```XML
<ns0:RequestOrder xmlns:ns0="http://Eldert.BizTalk.InlineXSLTWithIndexer.Schemas.RequestOrder/2016/10"> 
    <OrderNumber>1</OrderNumber>  
    <Delivery> 
        <DeliveryDate>2016-10-25</DeliveryDate>  
        <Product> 
            <Name>First Product</Name>  
            <Amount>10</Amount>  
            <SKU>123456</SKU>  
        </Product> 
        <Product> 
            <Name>Second Product</Name>  
            <Amount>5</Amount>  
            <SKU>654321</SKU>  
        </Product> 
        <Product> 
            <Name>Third Product</Name>  
            <Amount>1</Amount>  
            <SKU>456789</SKU>  
        </Product> 
        <Insurance> 
            <SKU>456789</SKU>  
            <PolicyNumber>123456789</PolicyNumber>  
        </Insurance> 
        <Insurance> 
            <SKU>123456</SKU>  
            <PolicyNumber>987654321</PolicyNumber>  
        </Insurance> 
    </Delivery> 
    <Delivery> 
        <DeliveryDate>2016-10-28</DeliveryDate>  
        <Product> 
            <Name>First Product</Name>  
            <Amount>200</Amount>  
            <SKU>123456</SKU>  
        </Product> 
        <Product> 
            <Name>Third Product</Name>  
            <Amount>50</Amount>  
            <SKU>456789</SKU>  
        </Product> 
        <Insurance> 
            <SKU>456789</SKU>  
            <PolicyNumber>123456789</PolicyNumber>  
        </Insurance> 
    </Delivery> 
</ns0:RequestOrder>
```

Now when we test our map, we would expect 5 packages in total, 3 for the first delivery, and 2 for the second, with the insurance policy node set on 3 of the packages. However, if we look at our output, we have 10 packages. Upon further investigation we will find out that each package has been created for each delivery, so in this example 5 packages times 2 deliveries, because the indexer is not taken into account. The reason our indexer is not taken into account, is because an XPATH expression needs it's indices to be an integer, and currently our parameter is treated as a string. Luckily this can be easily fixed, by calling the number() function on our parameter. Change this in the following 2 lines.

```XML
<xsl:for-each select="//*[local-name()='RequestOrder']/*[local-name()='Delivery'][number($parDeliveryOrderIndex)]/*[local-name()='Product']">
```

```XML
<xsl:variable name="var:InsuranceNumber" select="/*[local-name()='RequestOrder']/*[local-name()='Delivery'][number($parDeliveryOrderIndex)]/*[local-name()='Insurance'][*[local-name()='SKU'][text()=$var:SKU]]/*[local-name()='PolicyNumber'][text()]" />
```

Now if we retest our map, we will get the expected output.

```XML
<ns0:CreateOrder xmlns:ns0="http://Eldert.BizTalk.InlineXSLTWithIndexer.Schemas.CreateOrder/2016/10"> 
    <Package> 
        <OrderNumber>1</OrderNumber>  
        <Product>First Product</Product>  
        <Amount>10</Amount>  
        <InsurancePolicy>987654321</InsurancePolicy>  
        <DeliveryDate>2016-10-25</DeliveryDate>  
        <DeliveryIndex>1</DeliveryIndex>  
    </Package> 
    <Package> 
        <OrderNumber>1</OrderNumber>  
        <Product>Second Product</Product>  
        <Amount>5</Amount>  
        <DeliveryDate>2016-10-25</DeliveryDate>  
        <DeliveryIndex>1</DeliveryIndex>  
    </Package> 
    <Package> 
        <OrderNumber>1</OrderNumber>  
        <Product>Third Product</Product>  
        <Amount>1</Amount>  
        <InsurancePolicy>123456789</InsurancePolicy>  
        <DeliveryDate>2016-10-25</DeliveryDate>  
        <DeliveryIndex>1</DeliveryIndex>  
    </Package> 
    <Package> 
        <OrderNumber>1</OrderNumber>  
        <Product>First Product</Product>  
        <Amount>200</Amount>  
        <DeliveryDate>2016-10-28</DeliveryDate>  
        <DeliveryIndex>2</DeliveryIndex>  
    </Package> 
    <Package> 
        <OrderNumber>1</OrderNumber>  
        <Product>Third Product</Product>  
        <Amount>50</Amount>  
        <InsurancePolicy>123456789</InsurancePolicy>  
        <DeliveryDate>2016-10-28</DeliveryDate>  
        <DeliveryIndex>2</DeliveryIndex>  
    </Package> 
</ns0:CreateOrder>
```

## More Information
For more information, you can read [this article](http://social.technet.microsoft.com/wiki/contents/articles/36043.biztalk-server-using-index-parameter-in-inline-xslt-call-template.aspx) on the Technet Wiki.
