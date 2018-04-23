using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.XtraSpellChecker;
using DevExpress.Web.ASPxSpellChecker;
using System.Globalization;
using System.IO;

public partial class _Default : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
    }

    protected void btn_Click(object sender, EventArgs e) {
        String correctedText = CheckText(memo.Text);
        memo.Text = correctedText;
    }

    String CheckText(String text) {
        SpellCheckerBase checker = new SpellCheckerBase();

        checker.NotInDictionaryWordFound += new NotInDictionaryWordFoundEventHandler(checker_NotInDictionaryWordFound);

        SpellCheckerISpellDictionary dict = new SpellCheckerISpellDictionary(Server.MapPath("~/Dictionaries/american.xlg"),
            Server.MapPath("~/Dictionaries/english.aff"),
            new CultureInfo("en-us"));
        dict.AlphabetPath = Server.MapPath("~/Dictionaries/EnglishAlphabet.txt");
        dict.CacheKey = "ispellDic";
        dict.Load();

        checker.Dictionaries.Add(dict);

        checker.LevenshteinDistance = 4;

        String result = checker.Check(text);

        return result;
    }

    void checker_NotInDictionaryWordFound(object sender, NotInDictionaryWordFoundEventArgs e) {
        e.Result = SpellCheckOperation.ChangeAll;
        e.Handled = true;
    }
}
