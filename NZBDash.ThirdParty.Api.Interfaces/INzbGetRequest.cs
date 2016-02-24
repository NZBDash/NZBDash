#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: INzbGetRequest.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************/
#endregion
using NZBDash.DataAccess;
using NZBDash.ThirdParty.Api.Models.Api;

namespace NZBDash.ThirdParty.Api.Interfaces
{
    public interface INzbGetRequest
    {
        NzbGetList GetNzbGetList(string url, string username, string password);
        NzbGetStatus GetStatus(string url, string username, string password);
        NzbGetHistory GetHistory(string url, string username, string password);
        NzbGetLogs GetLogs(string url, string username, string password);
        bool SetDownloadStatus(string url, string username, string password, bool pause);
        bool SetDownloadLimit(string url, string username, string password, int kbLimit);
        bool Restart(string url, string username, string password);
        bool WriteLog(string url, string username, string password, NzbLogType kind, string message);

    }
}
