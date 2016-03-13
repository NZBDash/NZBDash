#region Copyright
// /************************************************************************
//   Copyright (c) 2016 Jamie Rees
//   File: StringCipherTests.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   \"Software\"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************/
#endregion
using System.Security.Cryptography;

using NUnit.Framework;

using NZBDash.Common.Helpers;

namespace NZBDash.Common.Tests.Helpers
{
    [TestFixture]
    public class StringCipherTests
    {
        [TestCase("ABCDEF","Passone")]
        [TestCase("<app block>","Passtwo")]
        [TestCase("{\"glossary\": {\"title\": \"example glossary\",\"GlossDiv\": {\"title\": \"S\",\"GlossList\": {\"GlossEntry\": {\"ID\": \"SGML\",\"SortAs\": \"SGML\",\"GlossTerm\": \"Standard Generalized Markup Language\",\"Acronym\": \"SGML\",\"Abbrev\": \"ISO 8879:1986\",\"GlossDef\": {\"para\": \"A meta-markup language, used to create markup languages such as DocBook.\",\"GlossSeeAlso\": [\"GML\", \"XML\"]},\"GlossSee\": \"markup\"}}}}}"
            ,"JSONExample")]
        public void Encrypt(string text, string passPhrase)
        {
            var encrypted = StringCipher.Encrypt(text, passPhrase);

            Assert.That(encrypted, Is.Not.EqualTo(text));
            Assert.That(encrypted, Is.Not.EqualTo(passPhrase));
        }

        [TestCase("ABCDEF", "Passone")]
        [TestCase("<app block>", "Passtwo")]
        [TestCase("{\"glossary\": {\"title\": \"example glossary\",\"GlossDiv\": {\"title\": \"S\",\"GlossList\": {\"GlossEntry\": {\"ID\": \"SGML\",\"SortAs\": \"SGML\",\"GlossTerm\": \"Standard Generalized Markup Language\",\"Acronym\": \"SGML\",\"Abbrev\": \"ISO 8879:1986\",\"GlossDef\": {\"para\": \"A meta-markup language, used to create markup languages such as DocBook.\",\"GlossSeeAlso\": [\"GML\", \"XML\"]},\"GlossSee\": \"markup\"}}}}}"
                , "JSONExample")]
        public void Decrypt(string text, string passPhrase)
        {
            var encrypted = StringCipher.Encrypt(text,passPhrase);

            Assert.That(encrypted, Is.Not.EqualTo(text));
            Assert.That(encrypted, Is.Not.EqualTo(passPhrase));

            var decrypted = StringCipher.Decrypt(encrypted, passPhrase);

            Assert.That(decrypted, Is.EqualTo(text));
            Assert.That(decrypted, Is.Not.EqualTo(passPhrase));
        }

        [TestCase("ABCDE", "Passone")]
        public void InvalidDecryptThrowsCryptographicException(string text, string passPhrase)
        {
            var encrypted = StringCipher.Encrypt(text, passPhrase);

            Assert.Throws<CryptographicException>(
                () =>
                {
                    StringCipher.Decrypt(encrypted, "incorrectPassPhrase");
                });
        }
    }
}