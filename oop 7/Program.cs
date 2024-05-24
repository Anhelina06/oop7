using System;

public class Node
{
    public double Data { get; set; }
    public Node Prev { get; set; }
    public Node Next { get; set; }

    public Node(double data)
    {
        Data = data;
        Prev = null;
        Next = null;
    }
}

public class DoublyLinkedList
{
    private Node head;
    private int count;

    public DoublyLinkedList()
    {
        head = null;
        count = 0;
    }

    public void AddFirst(double data)
    {
        InsertAtBeginning(data);
        PrintAverage();  
    }

    public void InsertAtBeginning(double data)
    {
        Node newNode = new Node(data);
        if (head != null)
        {
            newNode.Next = head;
            head.Prev = newNode;
        }
        head = newNode;
        count++;
    }



    public Node FindFirstLessThanAverage()
    {
        if (head == null) return null;

        double sum = 0;
        for (int i = 0; i < count; i++)
        {
            sum += this[i];
        }

        double average = sum / count;
        Console.WriteLine($"Average value: {average}");  

        for (int i = 0; i < count; i++)
        {
            if (this[i] < average)
                return GetNodeAt(i);
        }

        return null;
    }

    public double SumAfterMax()
    {
        if (head == null) return 0;

        Node maxNode = head;
        for (int i = 1; i < count; i++)
        {
            if (this[i] > maxNode.Data)
                maxNode = GetNodeAt(i);
        }

        double sum = 0;
        Node current = maxNode.Next;

        while (current != null)
        {
            sum += current.Data;
            current = current.Next;
        }

        return sum;
    }

    public DoublyLinkedList GetElementsGreaterThan(double value)
    {
        DoublyLinkedList newList = new DoublyLinkedList();

        for (int i = 0; i < count; i++)
        {
            if (this[i] > value)
                newList.InsertAtBeginning(this[i]);
        }

        return newList;
    }

    public void RemoveBeforeMax()
    {
        if (head == null) return;

        Node maxNode = head;
        for (int i = 1; i < count; i++)
        {
            if (this[i] > maxNode.Data)
                maxNode = GetNodeAt(i);
        }

        while (head != maxNode)
        {
            Node temp = head;
            head = head.Next;
            if (head != null)
                head.Prev = null;
            temp.Next = null;
            count--;
        }
    }

    public void PrintList()
    {
        for (int i = 0; i < count; i++)
        {
            Console.Write(this[i] + " ");
        }
        Console.WriteLine();
    }

    public double this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index is out of range.");

            Node current = GetNodeAt(index);
            return current.Data;
        }
        set
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index is out of range.");

            Node current = GetNodeAt(index);
            current.Data = value;
        }
    }

    private Node GetNodeAt(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException("Index is out of range.");

        Node current = head;
        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }

        return current;
    }

    private void PrintAverage()
    {
        if (count == 0)
        {
            Console.WriteLine("The list is empty, no average value.");
            return;
        }

        double sum = 0;
        for (int i = 0; i < count; i++)
        {
            sum += this[i];
        }

    }
}

public class Program
{
    public static void Main()
    {
        DoublyLinkedList list = new DoublyLinkedList();
        list.AddFirst(5);
        list.AddFirst(30);
        list.AddFirst(10);
        list.AddFirst(20);
        list.AddFirst(15);

        Console.WriteLine("Initial list:");
        list.PrintList();

        

        Node firstLessThanAverage = list.FindFirstLessThanAverage();
        Console.WriteLine($"The first occurrence of an element smaller than the average value: {firstLessThanAverage?.Data}");

        double sumAfterMax = list.SumAfterMax();
        Console.WriteLine($"The sum of elements after the maximum: {sumAfterMax}");

        double threshold = 10;
        DoublyLinkedList greaterThanList = list.GetElementsGreaterThan(threshold);
        Console.WriteLine($"A new list of element values greater than {threshold}:");
        greaterThanList.PrintList();

        list.RemoveBeforeMax();
        Console.WriteLine("The list after removing the elements before the maximum:");
        list.PrintList();

        list.AddFirst(55); 
        Console.WriteLine("List after adding 55 to the beginning:");
        list.PrintList(); 
    }
}
