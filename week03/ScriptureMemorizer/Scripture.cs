using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        foreach (var w in text.Split(' '))
        {
            _words.Add(new Word(w));
        }
    }

    public void Display()
    {
        Console.WriteLine(_reference.ToString());
        foreach (var word in _words)
        {
            Console.Write(word.GetDisplayText() + " ");
        }
        Console.WriteLine("\n");
    }

    // Hide only words that are currently visible
    public void HideRandomWords(int count)
    {
        Random rand = new Random();
        List<Word> visibleWords = _words.FindAll(w => !w.IsHidden);

        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            int index = rand.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index); // Remove so we don't hide same word twice
        }
    }

    public bool AllWordsHidden()
    {
        foreach (var word in _words)
            if (!word.IsHidden)
                return false;
        return true;
    }
}
