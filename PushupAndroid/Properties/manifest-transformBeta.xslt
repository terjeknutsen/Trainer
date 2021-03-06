﻿<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output indent="yes" />
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()" />
    </xsl:copy>
  </xsl:template>
  <xsl:template match="/manifest/@package">
    <xsl:attribute name="package">
      <xsl:value-of select="'no.trainer.personal.beta'" />
    </xsl:attribute>
  </xsl:template>
</xsl:stylesheet>