using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Common.Extensions
{
    public static class UriExtensions
    {
        public static Uri AddRelativeUri(this Uri baseUri, string relativeUri) => new Uri(baseUri, relativeUri);
    }
}