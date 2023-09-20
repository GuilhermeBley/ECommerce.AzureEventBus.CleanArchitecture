namespace Ecommerce.Identity.Core.Validations;

internal static class StringUtils
{
    /// <summary>
    /// A-Z
    /// </summary>
    public const string CHAR_AZ_TOUPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    /// <summary>
    /// a-z
    /// </summary>
    public const string CHAR_AZ_TOLOWER = "abcdefghijklmnopqrstuvwxyz";

    /// <summary>
    /// A-Z a-z
    /// </summary>
    public const string CHAR_AZ = CHAR_AZ_TOUPPER + CHAR_AZ_TOLOWER;

    /// <summary>
    /// 0-9
    /// </summary>
    public const string CHAR_NUMBER = "0123456789";

    /// <summary>
    /// A-Z a-z 0-9
    /// </summary>
    public const string CHAR_AZ_NUMBER = CHAR_AZ + CHAR_NUMBER;

    /// <summary>
    /// A-Z a-z 0-9 [ ]
    /// </summary>
    public const string CHAR_AZ_NUMBER_SPACE = CHAR_AZ_NUMBER + " ";

    /// <summary>
    /// A-Z a-z 0-9 [ àèìòùÀÈÌÒÙáéíóúýÁÉÍÓÚÝâêîôûÂÊÎÔÛãñõÃÑÕäëïöüÿÄËÏÖÜŸ]
    /// </summary>
    public const string CHAR_AZ_NUMBER_SPACE_ACCENT = CHAR_AZ_NUMBER_SPACE + "àèìòùÀÈÌÒÙáéíóúýÁÉÍÓÚÝâêîôûÂÊÎÔÛãñõÃÑÕäëïöüÿÄËÏÖÜŸ";

}
