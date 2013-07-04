Imports System
Imports System.IO
Imports System.Net
Imports System.Console
Imports System.Threading
Imports System.Collections
Imports System.Text.RegularExpressions

Module WebCrawler
    Dim Website As String = Nothing
    Dim Depth As Integer = 0
    Dim ReadOnly Ver As String = "1.1"
    Dim log As Boolean = False
    Dim logfile as string = "Crawl.log"
    Dim links As Integer = 0
    Dim Agent As String = "qt"
    Dim Delay As Integer = "500"
    Dim DoingWork As Boolean = False
    Dim ShowArt As Boolean = True
    Dim lstEmails As New ArrayList
    Dim lstUrls As New ArrayList
    Dim aList As ArrayList = Nothing
    Dim i as UInt16 = 0
    Dim TimerOn As Boolean = False
    Dim atype As Integer = 1
    Dim defaultFcolor As ConsoleColor = Console.ForegroundColor

    Public Shared Sub Main(Args() As String)
    Try
	i = 0
	Do until i >= Args.Length
                Dim al As String = Args(i).ToLower
                If al = "-l" Or al = "--log" Then
                    log = True
                ElseIf al = "-b" Or al = "--bug-report" Then
                    BugReport()
                ElseIf al = "-h" Or al = "--help" Then
                    Help()
                ElseIf al = "-a" Or al = "--animation" Then
                    i += 1
                    atype = Args(i)
                    If atype > 3 Or atype < 0 Then
                        Console.WriteLine("Animation must be between 0 - 3")
                        Environment.Exit(1)
                    End If
                ElseIf al = "-v" Or al = "--version" Then
                    Console.WriteLine("Version: " & Ver)
                    Environment.Exit(0)
                ElseIf al = "-t" Or al = "--timer" Then
                    TimerOn = True
                ElseIf al = "-n" Or al = "--no-art" Then
                    ShowArt = False
                ElseIf al = "-lf" Or al = "--log-file" Then
                    log = True
                    i += 1
                    logfile = Args(i)
                ElseIf al.StartsWith("http://") = True Or al.StartsWith("https://") = True Then
                    Website = al
                ElseIf al = "-f" Or al = "--file" Then
                    i += 1
                    Website = Args(i)
                ElseIf al = "-w" Or al = "--website" Then
                    i += 1
                    Website = Args(i)
                ElseIf al = "-d" Or al = "--depth" Then
                    i += 1
                    Depth = Args(i)
		ElseIf al = "-dl" or al = "--delay" then
		    i += 1
		    Delay = Args(i)
                ElseIf al = "-u" Or al = "--user-agent" Then
                    i += 1
		    If Args(i) = "googlebot" Then
                        Agent = "googlebot"
                    ElseIf Args(i) = "none" Then
                        Agent = "none"
                    Else
                        Agent = Args(i)
                    End If
                Else
                    Console.WriteLine("Invalid syntax. See --help for help")
                    Environment.Exit(1)
                End If
	  i += 1
	 loop
	 Start()
      'Err.Number = 1
      'ErrorHandler()
    Catch
	  ErrorHandler()
    End Try
    End Sub
    Private Sub Help() 'to be re-done		ALSO this should include error codes!
        Console.WriteLine("========== Help Page ==========")
        Console.WriteLine()
        Console.WriteLine("Syntax:")
        Console.WriteLine("Normal: crawl <Website> <Depth>")
        Console.WriteLine("With Logs: crawl <cmd>")
        Console.WriteLine()
        Console.WriteLine("<Website>    has to be: a valid website on port 80")
        Console.WriteLine("<Depth>      how deep to go in levels of links")
        Console.WriteLine("<cmd>        can be: log list clear email url help none googlebot default qt custom")
        Console.WriteLine()
        Console.WriteLine("<Website>    A valid website on port 80")
        Console.WriteLine("<Switch>     Special conditions in crawl")
        Console.WriteLine("<Depth>      how deep to go in levels of links as integer")
        Console.WriteLine("<cmd>        Special commands in crawl")
        Console.WriteLine()
        Console.WriteLine("========== Description ==========")
        Console.WriteLine()
        Console.WriteLine("Qt-Spider " & Ver)
        Console.WriteLine("Created for searching the web for links and emails or anything")
        Console.WriteLine("else of interest. Original coding designed by a man named Kobe (see credits)")
        Console.WriteLine("and it is improved by me (see dev).")
        Environment.Exit(0)
    End Sub
    
    Private Sub BugReport()
	Console.WriteLine("How to submit a bug:" & VbNewLine)
	Console.WriteLine("You can send an email to bug@quitetiny.com with the subject line Crawl <bug>")
	Console.WriteLine("You can submit an issue on our GitHub" & VbNewLine)
	Console.WriteLine("Notes:")
	Console.WriteLine("MAKE SURE YOU HAVE THE MOST CURRENT VERSION")
	Console.WriteLine("You can check the version by running crawl with the switch -v or --version")
	Console.WriteLine("Make sure you explain what you were doing and take a picture if it helps")
	Console.WriteLine("Press ENTER to go to our GitHub page...")
	Console.ReadLine()
	System.Diagnostics.Process.Start("http://example.com") 'GitHub Goes here 'You need the https://
	Environment.Exit(0)
    End Sub
    
    Private Sub ErrorHandler()
	Console.ForegroundColor = ConsoleColor.Red
	'if statements should go here for custom error handeling
	If Err.Number = 0 then
	    Console.WriteLine("Error (0) Internal program error! Please fill out a bug report!")
	    Console.ForegroundColor = ConsoleColor.White
	    Console.WriteLine("To submit a bug, run crawl with the switch -b or --bug-report")
	    Environment.Exit(2)
	ElseIf Err.Number = 1 then
	    Console.WriteLine("Error (1) No job specified") 'Crawl finished the loop without having a job to do like crawl a website
	    Environment.Exit(1)
	Else
	    Console.WriteLine("Error (" & Err.Number & ") " & Err.Description)
	    Environment.Exit(3)
	End If
    End Sub
    
    Private Sub Art()
	'art
	ShowArt = False
	Start()
    End Sub

    Private Sub Start()
	 If Website = Nothing then
	    Console.WriteLine("No Website specified!")
	    Environment.Exit(1)
	 ElseIf Agent = "qt" andalso delay < 500 then
	    Console.WriteLine("Delay can not be shorter than 500 if the user-agent is QT")
	    Console.WriteLine("The reason is that people will ban this bot and it will be un-fair to others.")
	    Environment.Exit(1)
	 End If
	 If ShowArt = True then
	    Art()
	 End If
         If Depth >= 2 Then
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.WriteLine("Warning: A depth of two or larger will take a VERY long time to complete!")
            Console.Beep()
	    Console.ForeGroundColor = defaultFcolor
	    Console.WriteLine("Press ANY key to continue...")
	    Console.ReadKey()
         End If
	 If TimerOn = True then
	 		Dim TimerThread As New Threading.Thread(AddressOf Timer)
       		TimerThread.Start()
     End If
	 Dim workthread As New Threading.Thread(AddressOf Work)
     workthread.Start()
     DoingWork = True
	 If atype = 1 then
         Do Until DoingWork = False
	    	Console.Clear()
	    	Console.WriteLine("Searching '" & Website & "'.")
	    	Thread.Sleep(500)
	    	Console.Clear()
	    	Console.WriteLine("Searching '" & Website & "'..")
	    	Thread.Sleep(500)
	    	Console.Clear()
	    	Console.WriteLine("Searching '" & Website & "'...")
	    	Thread.Sleep(500)
	 	 Loop
	 ElseIf atype = 0
            Do Until DoingWork = False
                Thread.Sleep(1000)
            Loop
	 ElseIf atype = 2 then
	    StarLoad()
	 ElseIf atype = 3 then
	    DotLoad()
	 End If
    End Sub
    
    Private Sub CDone()
	 Console.Clear()
	 i = 0
	 If log = true then
	    LogRes()
	 End If
	 Console.WriteLine("Listing results:")
	 Do Until i >= aList.Count
	    If aList(i).StartsWith("(!) ") = True then
	 	Console.ForeGroundColor = ConsoleColor.Red
	  	Console.WriteLine(aList(i))
	    	Console.ForeGroundColor = defaultFcolor
	    Else
	    	Console.WriteLine(aList(i))
	    End If
	    i += 1
	    Thread.Sleep(100)
	 Loop
	 TimerOn = False
	 Thread.Sleep(600)
	 Environment.Exit(0)
    End Sub
    
    Private Sub LogRes()
	Try
	  Do Until i >= aList.Count
	    File.AppendAllText(logfile, aList(i) & VbNewLine)
	    i += 1
	 Loop
	 Console.WriteLine("Done!")
	 TimerOn = False
	 Thread.Sleep(600)
	 Environment.Exit(0)
	Catch
	 ErrorHandler()
	End Try
    End Sub

    Private Sub Work()
        aList = Spider(Website, Depth)
        DoingWork = False
    End Sub

    Private Function Spider(ByVal url As String, ByVal depth As Integer) As ArrayList
        'aReturn is used to hold the list of urls
        Dim aReturn As New ArrayList
        'aStart is used to hold the new urls to be checked
        Dim aStart As ArrayList = GrabUrls(url)
        'temp array to hold data being passed to new arrays
        Dim aTemp As ArrayList
        'aNew is used to hold new urls before being passed to aStart
        Dim aNew As New ArrayList
        'add the first batch of urls
        aReturn.AddRange(aStart)
        'if depth is 0 then only return 1 page
        If depth < 1 Then Return aReturn
        'loops through the levels of urls
        For i = 1 To depth
            'grabs the urls from each url in aStart
            For Each tUrl As String In aStart
                'grabs the urls and returns non-duplicates
                aTemp = GrabUrls(tUrl, aReturn, aNew)
                'add the urls to be check to aNew
                aNew.AddRange(aTemp)
            Next
            'swap urls to aStart to be checked
            aStart = aNew
            'add the urls to the main list
            aReturn.AddRange(aNew)
            'clear the temp array
            aNew = New ArrayList
        Next
        Return aReturn
    End Function
    Private Overloads Function GrabUrls(ByVal url As String) As ArrayList
        Dim strSource As String
        'will hold the urls to be returned
        Dim aReturn As New ArrayList
        Try
            'regex string used: thanks google
            Dim strRegex As String = "<a.*?href=""(.*?)"".*?>(.*?)</a>"
            'Dim strRegex2 As String = "*.html*"
            'i used a webclient to get the source
            'web requests might be faster
            Dim wc As New WebClient
            If Agent = "qt" Then
                wc.Headers(HttpRequestHeader.UserAgent) = "Mozilla/5.0 (compatible; Qt-Spider/" & Ver & "; +http://www.quitetiny.com/bot.html)"
                wc.Headers(HttpRequestHeader.Accept) = "*/*"
            ElseIf Agent = "googlebot" Then
                wc.Headers(HttpRequestHeader.UserAgent) = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)"
                wc.Headers(HttpRequestHeader.Accept) = "*/*"
            ElseIf Agent = "none" Then
                wc.Headers(HttpRequestHeader.UserAgent) = Nothing
            Else 'Custom
                wc.Headers(HttpRequestHeader.UserAgent) = Agent
                wc.Headers(HttpRequestHeader.Accept) = "*/*"
            End If
            Threading.Thread.Sleep(Delay)
            'put the source into a string
            strSource = wc.DownloadString(url)
            Dim HrefRegex As New Regex(strRegex, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
            'parse the urls from the source
            Dim HrefMatch As Match = HrefRegex.Match(strSource)
            'used later to get the base domain without subdirectories or pages
            Dim BaseUrl As New Uri(url)
            'while there are urls
            While HrefMatch.Success = True
                'loop through the matches
                Dim sUrl As String = HrefMatch.Groups(1).Value
                'if it's a page or sub directory with no base url (domain)
                If Not sUrl.Contains("http://") AndAlso Not sUrl.Contains("www") Then
                    'add the domain plus the page
                    Dim tURi As New Uri(BaseUrl, sUrl)
                    sUrl = tURi.ToString
                End If
                'if it's not already in the list then add it
                If Not aReturn.Contains(sUrl) Then aReturn.Add(sUrl)
                'go to the next url
                HrefMatch = HrefMatch.NextMatch
            End While
        Catch ex As Exception
            If ex.Message.Contains("The remote name could not be resolved: ") = True Then
                aReturn.Add("(!) Webserver Not Found [" & url & "]")
            Else
                aReturn.Add("(!) " & ex.Message & " [" & url & "]")
            End If
        End Try
        Return aReturn
        links += 1
    End Function
    Private Overloads Function GrabUrls(ByVal url As String, ByRef aReturn As ArrayList, ByRef aNew As ArrayList) As ArrayList
        'overloads function to check duplicates in aNew and aReturn
        'temp url arraylist
        Dim tUrls As ArrayList = GrabUrls(url)
        'used to return the list
        Dim tReturn As New ArrayList
        'check each item to see if it exists, so not to grab the urls again
        For Each item As String In tUrls
            If Not aReturn.Contains(item) AndAlso Not aNew.Contains(item) Then
                tReturn.Add(item)
            End If
        Next
        Return tReturn
    End Function

    Private Sub Timer()
	Dim n as UInt64 = 0
	Do Until TimerOn = False
		Thread.Sleep(1000)
		n += 1
	Loop
	Console.WriteLine("Crawling took " & n / 60 & " minutes")
    End Sub
    
    Private Sub StarLoad()
		Do until DoingWork = False
			Console.Clear()
			Console.WriteLine("| Crawling |")
			Thread.Sleep(100)
			Console.Clear()
			Console.WriteLine("/ Crawling /")
			Thread.Sleep(100)
			If DoingWork = False then
				Exit Do
			End If
			Console.Clear()
			Console.WriteLine("- Crawling -")
			Thread.Sleep(100)
			Console.Clear()
			Console.WriteLine("\ Crawling \")
			Thread.Sleep(100)
		Loop
	CDone()
    End Sub

    Private Sub DotLoad()
		Do until DoingWork = False
			Console.Write(".")
			Thread.Sleep(150)
		Loop
	CDone()
   End Sub
End Module
