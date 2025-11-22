using System;

public class Reference
{
    private string _book;
    private int _startChapter;
    private int _startVerse;
    private int? _endVerse;

    // Constructor for a single verse (e.g., John 3:16)
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _startChapter = chapter;
        _startVerse = verse;
        _endVerse = null;
    }

    // Constructor for a verse range (e.g., Proverbs 3:5-6)
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _startChapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public override string ToString()
    {
        return _endVerse.HasValue
            ? $"{_book} {_startChapter}:{_startVerse}-{_endVerse}"
            : $"{_book} {_startChapter}:{_startVerse}";
    }
}
