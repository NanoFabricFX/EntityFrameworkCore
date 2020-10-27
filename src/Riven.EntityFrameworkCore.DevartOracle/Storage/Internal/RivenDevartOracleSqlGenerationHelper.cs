using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore.Utilities;

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<挂起>")]
    public class RivenDevartOracleSqlGenerationHelper : RelationalSqlGenerationHelper
    {
        protected const string GenerateParameterNameTemplate = "\"{0}\"";
        public static string BatchTerminatorStr { get; protected set; } = Environment.NewLine + "/" + Environment.NewLine + Environment.NewLine;

        public RivenDevartOracleSqlGenerationHelper([NotNull] RelationalSqlGenerationHelperDependencies dependencies)
            : base(dependencies)
        {

        }

        public override string BatchTerminator => BatchTerminatorStr;

        public override string EscapeIdentifier(string identifier)
        {
            return base.EscapeIdentifier(
                identifier.ToUpperInvariant()
                );
        }

        public override void EscapeIdentifier(StringBuilder builder, string identifier)
        {
            base.EscapeIdentifier(
                builder,
                identifier.ToUpperInvariant()
                );
        }


        public override string GenerateParameterName(string name)
        {
            while (name.StartsWith("_", StringComparison.Ordinal)
                   || Regex.IsMatch(name, @"^\d"))
            {
                name = name.Substring(1);
            }
            return $":{name}";
        }

        public override void GenerateParameterName(StringBuilder builder, string name)
        {
            builder.Append(this.GenerateParameterName(name));
        }



        public override string DelimitIdentifier(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException(identifier);
            }

            // Interpolation okay; strings
            return string.Format(
                GenerateParameterNameTemplate,
                this.EscapeIdentifier(identifier)
                );
        }

        public override void DelimitIdentifier(StringBuilder builder, string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException(identifier);
            }

            builder.Append(
                this.DelimitIdentifier(identifier)
                );
        }
    }
}
