﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Winterdom.Viasfora.Contracts;

namespace Winterdom.Viasfora.Options {
  [Guid(Guids.SqlOptions)]
  public class SqlOptionsPage : DialogPage {
    private ILanguage language = SettingsContext.GetLanguage(Constants.Sql);

    public override void SaveSettingsToStorage() {
      base.SaveSettingsToStorage();
      language.ControlFlow = ControlFlowKeywords.ToArray();
      language.Visibility = VisibilityKeywords.ToArray();
      language.Linq = LinqKeywords.ToArray();
      language.Enabled = Enabled;
      var settings = SettingsContext.GetSettings();
      settings.Save();
    }
    public override void LoadSettingsFromStorage() {
      base.LoadSettingsFromStorage();
      ControlFlowKeywords = language.ControlFlow.ToList();
      VisibilityKeywords = language.Visibility.ToList();
      LinqKeywords = language.Linq.ToList();
      Enabled = language.Enabled;
    }

    [LocDisplayName("Enabled")]
    [Description("Enabled or disables all Viasfora features for this language")]
    public bool Enabled { get; set; }

    [LocDisplayName("Control Flow")]
    [Description("Control Flow keywords to highlight")]
    [Category("SQL")]
    [Editor(Constants.STRING_COLLECTION_EDITOR, typeof(UITypeEditor))]
    [TypeConverter(typeof(Design.StringListConverter))]
    public List<String> ControlFlowKeywords { get; set; }

    [LocDisplayName("Visibility")]
    [Description("Visibility keywords to highlight")]
    [Category("SQL")]
    [Editor(Constants.STRING_COLLECTION_EDITOR, typeof(UITypeEditor))]
    [TypeConverter(typeof(Design.StringListConverter))]
    public List<String> VisibilityKeywords { get; set; }

    [LocDisplayName("Statement")]
    [Description("Statement keywords to highlight")]
    [Category("SQL")]
    [Editor(Constants.STRING_COLLECTION_EDITOR, typeof(UITypeEditor))]
    [TypeConverter(typeof(Design.StringListConverter))]
    public List<String> LinqKeywords { get; set; }
  }
}
