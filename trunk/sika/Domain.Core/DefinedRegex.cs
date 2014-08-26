using System.Text.RegularExpressions;

namespace Domain.Core
{
    public class DefinedRegexEvaluation
    {
        private const string CStrInFunction = @"\[IN\](.*)\[\/IN\]";

        private const string CStrFields = @"\[FIELDS\](.*)\[\/FIELDS\]";

        private const string CStrNotInFunction = @"\[NOTIN\](.*)\[\/NOTIN\]";

        private const string CStrWhiteSpace = @"\s+";

        private const string CstrStoreProcedure = @"\[SP\](.*)\[\/SP\]";

        private const string CstrList = @"\[LIST\](.*)\[\/LIST\]";

        private const string CstrLeftMatches = @"\[LEFT\]([a-zA-Z;\d]*)\[\/LEFT\]";

        private const string CstrLeft = @"\[LEFT\](.*)\[\/LEFT\]";

        private const string CstrContains = @"\[CONTAINS\](.*)\[\/CONTAINS\]";

        private const string CstrSignatureFunction = @"\b[^()]+\((.*)\)$";

        private const string CstrParamsFunction = @"([^,]+\(.+?\))|([^,]+)";

        private const string CStrFunction = @"\[FN\](.*)\[\/FN\]";


        public static readonly Regex Function = new Regex(
         CStrFunction,
         RegexOptions.Compiled | RegexOptions.IgnoreCase
     );

        public static readonly Regex FielsExpression = new Regex(
         CStrFields,
         RegexOptions.Compiled | RegexOptions.IgnoreCase
     );

        public static readonly Regex ParamsFunction = new Regex(
         CstrParamsFunction,
         RegexOptions.Compiled | RegexOptions.IgnoreCase
     );


        public static readonly Regex SignatureFunction = new Regex(
         CstrSignatureFunction,
         RegexOptions.Compiled | RegexOptions.IgnoreCase
     );


        public static readonly Regex Contains = new Regex(
         CstrContains,
         RegexOptions.Compiled | RegexOptions.IgnoreCase
     );


        public static Regex Left = new Regex(
         CstrLeft,
         RegexOptions.Compiled | RegexOptions.IgnoreCase
     );

        public static readonly Regex LeftMatches = new Regex(
         CstrLeftMatches,
         RegexOptions.Compiled | RegexOptions.IgnoreCase
     );


        public static readonly Regex List = new Regex(
         CstrList,
         RegexOptions.Compiled | RegexOptions.IgnoreCase
     );


        public static readonly Regex InFunction = new Regex(
          CStrInFunction,
          RegexOptions.Compiled | RegexOptions.IgnoreCase
      );

        public static readonly Regex NotInFunction = new Regex(
         CStrNotInFunction,
         RegexOptions.Compiled | RegexOptions.IgnoreCase
     );

        public static Regex WhiteSpace = new Regex(
            CStrWhiteSpace,
            RegexOptions.Compiled
        );

        public static readonly Regex StoredProcedure = new Regex(
            CstrStoreProcedure,
            RegexOptions.Compiled
        );


        public static Regex Parenthesis = new Regex(
           @"\(",
           RegexOptions.Compiled
       );

        public static readonly Regex DosPuntos = new Regex(
          @"\:",
          RegexOptions.Compiled
      );
    }
}