<?xml version="1.0"?>
<xsl:stylesheet
           xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:output method="html"/>
<xsl:template match="StudentDataBase">
<TABLE BORDER="5">
<TR>
<xsl:for-each select="//speciality">
<TR>
<TD>
<TABLE BORDER="1" WIDTH="1200">
<TR>
<th style="width:10%;">
<p align="left">Speciality</p>
</th>
<th style="width:90%;">
<p align="left">
<xsl:value-of select="@SPECIALITY"/>
</p>
</th>
</TR>
</TABLE>
</TD>
<xsl:for-each select="group">
<TR>
<TD>
<TABLE BORDER="5" WIDTH="1200">
<TR>
<th style="width: 10%;">
<p align="left">Group</p>
</th>
<th style="width:90%;">
<p align="left">
<xsl:value-of select="@GROUP"/>
</p>
</th>
</TR>
</TABLE>
</TD>
<xsl:for-each select="student">
<TR>
<TD>
<TABLE BORDER="5" WIDTH="1200">
<TR>
<th style="width: 10%;">
<p align="left">Room</p>
</th>
<th style="width:90%;">
<p align="left">
<xsl:value-of select="@ROOM" />
</p>
</th>
</TR>
<TR>
<th style="width: 10%;">
<p align="left">Surname</p>
</th>
<th style="width:90%;">
<p align="left">
<xsl:value-of select="@SURNAME"/>
</p>
</th>
</TR>
<TR>
<th style="width: 10%;">
<p align="left">Name</p>
</th>
<th style="width:90%;">
<p align="left">
<xsl:value-of select="@NAME"/>
</p>
</th>
</TR>
<TR>
<th style="width: 10%;">
<p align="left">PhoneNumber</p>
</th>
<th style="width:90%;">
<p align="left">
<xsl:value-of select="@PHONENUMBER"/>
</p>
</th>
</TR>



	<TR>
		<th style="width: 10%;">
			<p align="left">Success</p>
		</th>
		<th style="width:90%;">
			<p align="left">
				<xsl:value-of select="@SUCCESS"/>
			</p>
		</th>
	</TR>
	
	
	
	
	
</TABLE>
</TD>
</TR>
</xsl:for-each>
</TR>
</xsl:for-each>
</TR>
</xsl:for-each>
</TR>
</TABLE>
</xsl:template>
</xsl:stylesheet>

