using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class StringLengthRangeAttribute : ValidationAttribute
{
    public int Minimum { get; set; }
    public int Maximum { get; set; }

    public StringLengthRangeAttribute()
    {
        this.Minimum = 0;
        this.Maximum = int.MaxValue;
    }

    public override bool IsValid(object value)
    {
        string strValue = value as string;
        strValue = Regex.Match(strValue, @"\d+").Value.Trim();
        if (!string.IsNullOrEmpty(strValue))
        {
            int len = strValue.Length;
            return len >= this.Minimum && len <= this.Maximum;
        }
        return true;
    }
}